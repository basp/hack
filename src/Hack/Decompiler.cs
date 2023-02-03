namespace Hack;

using System.Text;

public static class Decompiler
{
    public static string Decompile(Instruction i) =>
        i.IsComputation
            ? DecompileComputation(i)
            : DecompileAddress(i);

    private static string DecompileComputation(Instruction i)
    {
        var dest = Mnemonics.Destinations[(Destination)i.Dest];
        var comp = Mnemonics.Computations[(Computation)i.Comp];
        var jump = Mnemonics.Jumps[(Jump)i.Jump];

        dest = string.IsNullOrEmpty(dest)
            ? string.Empty
            : $"{dest}=";

        jump = string.IsNullOrEmpty(jump)
            ? string.Empty
            : $";{jump}";

        return $"{dest}{comp}{jump}";
    }

    private static string DecompileAddress(Instruction i) =>
        $"@{i.Address}";
}