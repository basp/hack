namespace Hack.Tests;

public class Compiler
{
    public static short CompileC(
        Computation comp,
        Destination dest = Destination.None,
        Jump jump = Jump.None)
    {
        short r = 0;
        r = (short)(r
            | 0b111_0_000000_000_000
            | (int)comp << 6
            | (int)dest << 3
            | (int)jump << 0);

        return r;
    }

    public static short CompileA(short address) =>
        (short)(
            0b0111_1111_1111_1111
            & address);
}
