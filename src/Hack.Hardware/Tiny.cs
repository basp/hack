namespace Hack.Hardware;

internal enum CommandType
{
    None,
    Address,
    Computation,
}

public class Tiny
{
    private const ushort CInstructionMask = 0b1110_0000_0000_0000;
    private const ushort CompMask = 0b0001_111111_000_000;
    private const ushort DestinationMask = 0b0000_000000_111_000;
    private const ushort JumpMask = 0b0000_000000_000_111;

    private static readonly IDictionary<ushort, Computation> computations =
        new Dictionary<ushort, Computation>
        {
            [0b0000_101010_000_000] = Computation.Zero,
            [0b0000_111111_000_000] = Computation.One,
            [0b0000_111010_000_000] = Computation.MinusOne,
            [0b0000_001100_000_000] = Computation.Destination,
            [0b0000_110000_000_000] = Computation.Address,
            [0b0000_001101_000_000] = Computation.NotDestination,
            [0b0000_110001_000_000] = Computation.NotAddress,
            [0b0000_001111_000_000] = Computation.MinusDestination,
            [0b0000_110011_000_000] = Computation.MinusAddress,
            [0b0000_011111_000_000] = Computation.DestinationPlusOne,
            [0b0000_110111_000_000] = Computation.AddressPlusOne,
            [0b0000_001110_000_000] = Computation.DestinationMinusOne,
            [0b0000_110010_000_000] = Computation.AddressMinusOne,
            [0b0000_000010_000_000] = Computation.DestinationPlusAddress,
            [0b0000_010011_000_000] = Computation.DestinationMinusAddress,
            [0b0000_000111_000_000] = Computation.AddressMinusDestination,
            [0b0000_000000_000_000] = Computation.DestinationPlusAddress,
            [0b0000_000000_000_000] = Computation.DestinationAndAddress,
            [0b0000_010101_000_000] = Computation.DestinationOrAddress,
            [0b0001_110000_000_000] = Computation.Memory,
            [0b0001_110001_000_000] = Computation.NotMemory,
            [0b0001_110011_000_000] = Computation.MinusMemory,
            [0b0001_110111_000_000] = Computation.MemoryPlusOne,
            [0b0001_110010_000_000] = Computation.MemoryMinusOne,
            [0b0001_000010_000_000] = Computation.DestinationPlusMemory,
            [0b0001_010011_000_000] = Computation.DestinationMinusMemory,
            [0b0001_000111_000_000] = Computation.MemoryMinusDestination,
            [0b0001_000000_000_000] = Computation.DestinationAndMemory,
            [0b0001_010101_000_000] = Computation.DestinationOrMemory,
        };

    private static readonly IDictionary<ushort, Destination> destinations =
        new Dictionary<ushort, Destination>
        {
            [0b000_0000000_000_000] =
                Destination.None,
            [0b000_0000000_001_000] =
                Destination.Memory,
            [0b000_0000000_010_000] =
                Destination.Destination,
            [0b000_0000000_011_000] =
                Destination.Memory &
                Destination.Destination,
            [0b000_0000000_100_000] =
                Destination.Address,
            [0b000_0000000_101_000] =
                Destination.Address &
                Destination.Memory,
            [0b000_0000000_110_000] =
                Destination.Address &
                Destination.Destination,
            [0b000_0000000_111_000] =
                Destination.Address &
                Destination.Memory &
                Destination.Destination,
        };

    private static readonly IDictionary<ushort, Jump> jumps =
        new Dictionary<ushort, Jump>
        {
            [0b000_0000000_000_000] = Jump.None,
            [0b000_0000000_000_001] = Jump.GreaterThan,
            [0b000_0000000_000_010] = Jump.Equal,
            [0b000_0000000_000_011] = Jump.GreaterThanOrEqual,
            [0b000_0000000_000_100] = Jump.LessThan,
            [0b000_0000000_000_101] = Jump.NotEqual,
            [0b000_0000000_000_110] = Jump.LessThanOrEqual,
            [0b000_0000000_000_111] = Jump.Unconditionally,
        };

    private readonly ushort[] instructions = new ushort[short.MaxValue];

    private readonly ushort[] data = new ushort[short.MaxValue];

    private ushort ip = 0;

    public ushort D { get; private set; }

    public ushort A { get; private set; }

    public ushort Out { get; private set; }

    public void Run(ushort[] code)
    {
        Array.Copy(code, this.instructions, code.Length);

        while (true)
        {
            var ins = this.instructions[ip];
            var type = GetCommandType(ins);

            // TODO: Check for halt

            switch (type)
            {
                case CommandType.Address:
                    ip = ExecuteAddress(ins);
                    continue;
                case CommandType.Computation:
                    ip = ExecuteCompute(ins);
                    continue;
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    private ushort ExecuteAddress(ushort ins)
    {
        this.A = ins;

        // debug
        Console.WriteLine($"A <- {ins}");

        return (ushort)(this.ip + 1);
    }

    private ushort ExecuteCompute(ushort ins)
    {
        // This is a value pointint into the *next* instruction index.
        ushort NextInstruction() => (ushort)(this.ip + 1);

        // By default we just go into infinite loop here. The
        // value of `next` is the current instruction pointer.
        // So if we don't do anything in this method we'll be F'ed.
        // In other words: make sure to change the instruction pointer!
        var next = this.ip;

        // Get some more friendly representations
        // of our instruction set.
        var comp = computations[(ushort)(ins & CompMask)];
        var dest = destinations[(ushort)(ins & DestinationMask)];
        var jump = jumps[(ushort)(ins & JumpMask)];

        // debug
        Console.WriteLine($"{dest}={comp};{jump}");

        // First, we need to deal with the current
        // computation. There's only so much things this ALU
        // can do so the switch should be straightforward.
        switch (comp)
        {
            case Computation.Zero:
                this.Out = 0;
                break;
            case Computation.One:
                this.Out = 1;
                break;
            case Computation.MinusOne:
                // TODO: This needs a better solution
                Console.WriteLine("warning: unchecked code");
                this.Out = unchecked((ushort)-1);
                break;
            case Computation.Destination:
                this.Out = this.D;
                break;
            case Computation.Address:
                this.Out = this.A;
                break;
            case Computation.NotDestination:
                this.Out = (ushort)~this.D;
                break;
            case Computation.NotAddress:
                this.Out = (ushort)~this.A;
                break;
            case Computation.MinusDestination:
                this.Out = (ushort)-this.D;
                break;
            case Computation.MinusAddress:
                this.Out = (ushort)-this.A;
                break;
            case Computation.Memory:
                this.Out = this.data[this.A];
                break;
            default:
                var msg = $"{comp}";
                throw new InvalidOperationException(msg);
        }
        
        // We need to have a way to direct the output of our
        // most recent computation and this takes care of that.
        // The three basic registers are A(ddress), D(ata) 
        // and M(emory). There's a number of destination options
        // that allow to write to multiple registers/locations in
        // memory.
        switch (dest)
        {
            case Destination.None:
                // Just drop it.                
                break;
            case Destination.Destination:
                Console.WriteLine($"D <-");
                this.D = this.Out;
                break;
            case Destination.Memory:
                Console.WriteLine($"M[{this.A}] <-");
                this.data[this.A] = this.Out;
                break;
            case Destination.DM:
                Console.WriteLine($"D M[{this.A}] <-");
                this.data[this.A] = this.Out;
                this.D = this.Out;
                break;
            case Destination.Address:
                Console.WriteLine($"A <-");
                this.A = this.Out;
                break;
            case Destination.AM:
                Console.WriteLine($"A M[{this.A}] <-");
                this.data[this.A] = this.Out;
                this.A = this.Out;
                break;
            case Destination.AD:
                Console.WriteLine($"A D <-");
                this.A = this.Out;
                this.D = this.Out;
                break;
            case Destination.AMD:
                Console.WriteLine($"A D M[{this.A}] <-");
                this.data[this.A] = this.Out;
                this.A = this.Out;
                this.D = this.Out;
                break;
            default:
                throw new InvalidOperationException();
        }

        // The `jump` part of the C-instruction specifies
        // where we need to go to next. It will always compare
        // to zero (0). We need to return the instruction
        // pointer (ip) value accordingly. If there's no 
        // jump component in the C-instruction then we will
        // just resume with the next one `this.ip + 1` and
        // otherwise we'll firgure out how to jump using
        // the `jump` field in the C-instruction.
        switch (jump)
        {
            case Jump.None:
                next = (ushort)(this.ip + 1);
                break;
            case Jump.Equal:
                next = this.Out == 0 
                    ? this.A 
                    : NextInstruction();
                break;
            case Jump.NotEqual:
                next = this.Out != 0
                    ? this.A 
                    : NextInstruction();
                break;
            case Jump.GreaterThan:
                next = this.Out > 0
                    ? this.A
                    : NextInstruction();
                break;
            case Jump.GreaterThanOrEqual:
                next = this.Out >= 0
                    ? this.A
                    : NextInstruction();
                break;
            case Jump.LessThan:
                next = this.Out < 0
                    ? this.A
                    : NextInstruction();
                break;
            case Jump.LessThanOrEqual:
                next = this.Out <= 0
                    ? this.A
                    : NextInstruction();
                break;
            case Jump.Unconditionally:
                next = this.A;
                break;
        }

        return next;
    }

    private bool IsComputeInstruction(ushort ins) =>
        (ins & CInstructionMask) == CInstructionMask;

    private CommandType GetCommandType(ushort ins) =>
        IsComputeInstruction(ins)
            ? CommandType.Computation
            : CommandType.Address;
}