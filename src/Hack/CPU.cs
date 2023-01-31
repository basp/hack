namespace Hack;

public class CPU
{
    private static readonly IDictionary<short, Func<CPU, bool>> jumps =
        new Dictionary<short, Func<CPU, bool>>
        {
            // Never jump
            [0b000] = cpu => false,

            // Jump if out > 0
            [0b001] = cpu => !cpu.ALU.NG && !cpu.ALU.ZR,

            // Jump if out == 0
            [0b010] = cpu => cpu.ALU.ZR,

            // Jump if out >= 0
            [0b011] = cpu => cpu.ALU.ZR || !cpu.ALU.NG,

            // Jump if out < 0
            [0b100] = cpu => cpu.ALU.NG,

            // Jump if out != 0
            [0b101] = cpu => !cpu.ALU.ZR,

            // Jump if out <= 0
            [0b110] = cpu => cpu.ALU.ZR || cpu.ALU.NG,

            // Always jump
            [0b111] = cpu => true,
        };

    private short a;

    private short d;

    private bool writeM;

    /// <summary>
    /// Gets the value of the A register.
    /// </summary>
    public short A => this.a;

    /// <summary>
    /// Gets the value of the D register.
    /// </summary>
    public short D => this.d;

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
    public short AddressM => this.a;

    /// <summary>
    /// Gets the program counter indicating the next instruction.
    /// </summary>
    public short PC { get; private set; }

    /// <summary>
    /// Performs one clock-cycle of work on the CPU.
    /// </summary>
    public void Cycle()
    {
        if (this.Instruction.IsComputation)
        {
            this.ALU.Op = (byte)this.Instruction.Comp;            
            this.ALU.X = this.d;
            this.ALU.Y = this.Instruction.IsAlpha
                ? this.InM
                : this.a;
            this.writeM = (this.Instruction.Dest & 0b001) == 0b001;
            this.d = (this.Instruction.Dest & 0b010) == 0b010
                ? this.ALU.Out
                : this.d;
            this.a = (this.Instruction.Dest & 0b100) == 0b100
                ? this.ALU.Out
                : this.a;
            this.PC = jumps[this.Instruction.Jump](this)
                ? this.a
                : (short)(this.PC + 1);
        }
        else
        {
            this.a = this.Instruction.Address;
            this.PC += 1;
        }
    }
}