namespace Hack;

// The A register can be used to either hold memory
// address or a code address (to an instruction).
// A C-instruction that may cause a jump should not
// contain any references to M and an expression
// that has any references to M should not contain
// a jump specification.
public class Simulator
{
    private readonly ROM32K program = new ROM32K();

    private readonly RAM32K data = new RAM32K();

    private readonly CPU cpu;

    public bool IsHalted => this.cpu.PC < 0;

    public Simulator()
    {
        this.cpu = new CPU(this.program, this.data);
    }
}