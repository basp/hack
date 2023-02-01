namespace Hack;

public class CPU
{
    private static readonly IDictionary<short, Func<CPU, bool>> jumps =
        new Dictionary<short, Func<CPU, bool>>
        {
            [(short)Jump.Never] = cpu => false,
            [(short)Jump.GT] = cpu => !cpu.ALU.NG && !cpu.ALU.ZR,
            [(short)Jump.EQ] = cpu => cpu.ALU.ZR,
            [(short)Jump.GE] = cpu => cpu.ALU.ZR || !cpu.ALU.NG,
            [(short)Jump.LT] = cpu => cpu.ALU.NG,
            [(short)Jump.NE] = cpu => !cpu.ALU.ZR,
            [(short)Jump.LE] = cpu => cpu.ALU.ZR || cpu.ALU.NG,
            [(short)Jump.Always] = cpu => true,
        };

    private bool writeM;

    public Register A { get; } = new Register();

    public Register D { get; } = new Register();

    /// <summary>
    /// Gets the ALU.
    /// </summary>
    public ALU ALU { get; } = new ALU();

    /// <summary>
    /// Gets or sets the value coming in from memory.
    /// </summary>
    public short InM { get; set; }

    /// <summary>
    /// Gets or sets the instruction.
    /// </summary>
    public Instruction Instruction { get; set; }

    /// <summary>
    /// Gets the value that would be stored in memory.
    /// </summary>
    public short OutM => this.ALU.Out;

    /// <summary>
    /// Gets a value indicating whether a value should be stored in memory.
    /// </summary>
    public bool WriteM => this.writeM;

    /// <summary>
    /// Gets the memory address where a value would be stored.
    /// </summary>
    public short AddressM => this.A.Out;

    /// <summary>
    /// Gets the program counter indicating the next instruction.
    /// </summary>
    public short PC { get; private set; }

    public void Tick()
    {
        if (this.Instruction.IsComputation)
        {
            this.ALU.Op = (byte)this.Instruction.Comp;
            this.ALU.X = this.D.Out;
            this.ALU.Y = this.Instruction.IsAlpha
                ? this.InM
                : this.A.Out;
            this.writeM = (this.Instruction.Dest & 0b001) == 0b001;
            this.D.In = this.ALU.Out;
            this.A.In = this.ALU.Out;
            this.D.Load = (this.Instruction.Dest & 0b010) == 0b010;
            this.A.Load = (this.Instruction.Dest & 0b100) == 0b100; 
            this.PC = jumps[this.Instruction.Jump](this)
                ? this.A.Out
                : (short)(this.PC + 1);
        }
        else
        {
            this.A.Load = true;
            this.A.In = this.Instruction.Address;
            this.PC += 1;
        }

        this.A.Tick();
        this.D.Tick();
    }

    public void Tock()
    {
        this.A.Tock();
        this.D.Tock();
    }

    /// <summary>
    /// Performs one clock-cycle of work on the CPU.
    /// </summary>
    public void Cycle()
    {
        Tick();
        Tock();
    }
}