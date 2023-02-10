namespace Hack;

public class Simulator
{
    public ROM32K Program { get; }

    public RAM32K Data { get; }

    private CPU CPU { get; }

    public Simulator(ROM32K program)
        : this(program, new RAM32K())
    {
    }

    public Simulator(ROM32K program, RAM32K data)
    {
        this.Program = program;
        this.Data = data;
        this.CPU = new CPU(data);
    }

    public bool IsHalted =>
        this.CPU.Instruction.IsComputation ?   
            this.CPU.Instruction.Comp == (short)Computation.Zero &&
            this.CPU.Instruction.Jump == (short)Jump.Always &&
            // If the computation is zero (0) and the jump destination is the 
            // previous instruction or the current instruction and we know that 
            // this is an unconditional jump then we also know that we are in a 
            // tight infinite loop that *calculates nothing*.
            // At this point it is pretty safe to signal the halted flag.
            (
                this.CPU.PC == (this.Program.Address - 1) || 
                this.CPU.PC == this.Program.Address
            )
            : false;

    /// <summary>
    /// Runs the simulator in a (potentially endless) loop
    /// until it encounters the halt signature.
    /// </summary>
    /// <remarks>
    /// For debugging and development purposes you might want
    /// to use the `Cycle` method instead which just executes
    /// one instruction before returning.
    /// </remarks>
    public int Run()
    {
        var i = 0;
        while (!this.IsHalted)
        {
            this.Cycle();
            i++;
        }

        return i;
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
        this.CPU.Tick();
    }

    public void Tock()
    {
        this.CPU.Tock();
    }
}