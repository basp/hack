namespace Hack;

public class Disassembler
{
    public static string[] Disassemble(short[] program)
    {
        var code = new List<string>();
        for (var i = 0; i < program.Length; i++)
        {
            var ins = new Instruction(program[i]);
            code.Add(Decompiler.Decompile(ins));
        }

        return code.ToArray();
    }
}