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
              this.CPU.Instruction.Jump == (short)Jump.Unconditionally
            : false;
        

    public void Cycle()
    {
        this.Tick();
        this.Tock();
    }

    public void Tick()
    {
        // Load next instruction from ROM
        this.Program.Address = this.CPU.PC;
        this.CPU.Instruction = this.Program.Out;

        // Load the CPU memory input from RAM
        this.Data.Address = this.CPU.AddressM;
        this.CPU.InM = this.Data.Out;

        // Perform a cycle on the GPU
        // to decode and evaluate the 
        // instruction
        this.CPU.Cycle();
    }

    public void Tock()
    {
        // Setup the RAM with CPU outputs
        this.Data.Load = this.CPU.WriteM;
        this.Data.Address = this.CPU.AddressM;
        this.Data.In = this.CPU.OutM;

        // Cycle the RAM to store the CPU
        // memory output
        this.Data.Cycle();
    }
}