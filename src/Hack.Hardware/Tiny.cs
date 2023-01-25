namespace Hack.Hardware;

internal enum CommandType
{
    None,
    Address,
    Computation,
}

public class Tiny
{
    private const ushort ComputeMask = 0b1110_0000_0000_0000;

    private static readonly IDictionary<ushort, Computation> computations =
        new Dictionary<ushort, Computation>
        {
            [0b000_0101010_000_000] = Computation.Zero,
            [0b000_0111111_000_000] = Computation.One,
            [0b000_0111010_000_000] = Computation.MinusOne,
            [0b000_0001100_000_000] = Computation.Destination,
            [0b000_0110000_000_000] = Computation.Address,
            [0b000_0001101_000_000] = Computation.NotDestination,
            [0b000_0110001_000_000] = Computation.NotAddress,
            [0b000_0001111_000_000] = Computation.MinusDestination,
            [0b000_0110011_000_000] = Computation.MinusAddress,
            [0b000_0011111_000_000] = Computation.DestinationPlusOne,
            [0b000_0110111_000_000] = Computation.AddressPlusOne,
            [0b000_0001110_000_000] = Computation.DestinationMinusOne,
            [0b000_0110010_000_000] = Computation.AddressMinusOne,
            [0b000_0000010_000_000] = Computation.DestinationPlusAddress,
            [0b000_0010011_000_000] = Computation.DestinationMinusAddress,
            [0b000_0000111_000_000] = Computation.AddressMinusDestination,
            [0b000_0000000_000_000] = Computation.DestinationPlusAddress,
            [0b000_0000000_000_000] = Computation.DestinationAndAddress,
            [0b000_0010101_000_000] = Computation.DestinationOrAddress,
            [0b000_1110000_000_000] = Computation.Memory,
            [0b000_1110001_000_000] = Computation.NotMemory,
            [0b000_1110011_000_000] = Computation.MinusMemory,
            [0b000_1110111_000_000] = Computation.MemoryPlusOne,
            [0b000_1110010_000_000] = Computation.MemoryMinusOne,
            [0b000_1000010_000_000] = Computation.DestinationPlusMemory,
            [0b000_1010011_000_000] = Computation.DestinationMinusMemory,
            [0b000_1000111_000_000] = Computation.MemoryMinusDestination,
            [0b000_1000000_000_000] = Computation.DestinationAndMemory,
            [0b000_1010101_000_000] = Computation.DestinationOrMemory,
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

    private readonly IDictionary<string, ushort> symbols =
        new Dictionary<string, ushort>
        {
            ["SP"] = 0,
            ["LCL"] = 1,
            ["ARG"] = 2,
            ["THIS"] = 3,
            ["THAT"] = 4,
            ["R0"] = 0,
            ["R1"] = 1,
            ["R2"] = 2,
            ["R3"] = 3,
            ["R4"] = 4,
            ["R5"] = 5,
            ["R6"] = 6,
            ["R7"] = 7,
            ["R8"] = 8,
            ["R9"] = 9,
            ["R10"] = 10,
            ["R11"] = 11,
            ["R12"] = 12,
            ["R13"] = 13,
            ["R14"] = 14,
            ["R15"] = 15,
        };

    private int symbolCount = 16;

    private int ip = 0;

    public ushort D { get; private set; }

    public ushort A { get; private set; }

    public void Run(ushort[] instructions)
    {
        Array.Copy(instructions, this.instructions, instructions.Length);
    }

    private void ExecuteAddress(ushort ins)
    {

    }

    private void ExecuteCompute(ushort ins)
    {

    }

    private bool IsComputeInstruction(ushort ins) =>
        (ins & ComputeMask) == ComputeMask;

    private CommandType GetCommandType(ushort ins) =>
        IsComputeInstruction(ins)
            ? CommandType.Computation
            : CommandType.Address;
}