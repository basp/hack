namespace Hack;

public class Simulator
{
    public ROM32K Program { get; }

    public RAM32K Data { get; }

    private CPU CPU { get; } = new CPU();

    public Simulator(ROM32K program)
        : this(program, new RAM32K())
    {
    }

    public Simulator(ROM32K program, RAM32K data)
    {
        this.Program = program;
        this.Data = data;
    }

    public bool IsHalted =>
        this.CPU.Instruction.IsComputation
            ? this.CPU.Instruction.Comp == (short)Computation.Zero &&
              this.CPU.Instruction.Jump == (short)Jump.Always &&
              this.CPU.PC == (this.Program.Address - 1)
            : false;

    public void Run()
    {
        while (!this.IsHalted)
        {
            this.Cycle();
        }
    }

    public void Cycle()
    {
        this.Tick();
        this.Tock();
    }

    public void Tick()
    {
        this.Program.Address = this.CPU.PC;
        this.CPU.Instruction = this.Program.Out;
        this.Data.Address = this.CPU.AddressM;
        this.CPU.InM = this.Data.Out;
        this.CPU.Tick();
    }

    public void Tock()
    {
        this.Data.Load = this.CPU.WriteM;
        this.Data.Address = this.CPU.AddressM;
        this.Data.In = this.CPU.OutM;
        this.Data.Cycle();
        this.CPU.Tock();
    }
}