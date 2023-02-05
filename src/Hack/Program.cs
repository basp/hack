using Antlr4.Runtime;
using Hack;
using PowerArgs;

using Args = PowerArgs.Args;

public class ReadArgs
{
    [ArgRequired]
    [ArgDescription("The path to the source file")]
    [ArgPosition(1)]
    public string Path { get; set; } = string.Empty;
}

public class ReadWriteArgs : ReadArgs
{
    [ArgRequired]
    [ArgDescription("The path to the output")]
    [ArgShortcut("-o")]
    public string Out { get; set; } = string.Empty;
}

public class ReadWriteOptionalArgs : ReadArgs
{
    [ArgDescription("The optional path to the output")]
    [ArgShortcut("-o")]
    public string Out { get; set; } = string.Empty;
}

public class ReadExecuteArgs : ReadArgs
{
    [ArgDefaultValue(1)]
    [ArgDescription("The number of times to execute")]
    [ArgShortcut("-x")]
    public int Runs { get; set; }
}

[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
public class HackProgram
{
    [HelpHook]
    [ArgDescription("Shows this help")]
    [ArgShortcut("-?")]
    public bool Help { get; set; }

    [ArgActionMethod]
    [ArgDescription("Disassembles a Hack binary")]
    public void Dasm(ReadWriteOptionalArgs args)
    {
        void Write(string[] code)
        {
            if (!string.IsNullOrEmpty(args.Out))
            {
                // If output is specified then write
                // to that path
                File.WriteAllLines(args.Out, code);
            }
            else
            {
                // Otherwise we just write to console
                foreach (var cmd in code)
                {
                    Console.WriteLine(cmd);
                }
            }
        }

        var prog = ReadBinary(args.Path);
        var code = Disassembler.Disassemble(prog);
        Write(code);
    }

    [ArgActionMethod]
    [ArgDescription("Runs a Hack binary")]
    public void Bin(ReadExecuteArgs args)
    {
        var rom = new ROM32K(ReadBinary(args.Path));
        var ram = new RAM32K();

        var start = DateTime.Now;
        for (var i = 0; i < args.Runs; i++)
        {
            var sim = new Simulator(rom, ram);
            sim.Run();
        }

        var finish = DateTime.Now;
        var duration = (finish - start);
        var seconds = Math.Round(duration.TotalSeconds, 2);
        var avg = Math.Round(duration.TotalMilliseconds / args.Runs, 2);

        Console.WriteLine($"{args.Runs} runs in {seconds}s (avg. {avg}ms/run)");

        var registers = new []
        {
            "SP   ",
            "LCL  ",
            "ARG  ",
            "THIS ",
            "THAT ",
        };

        for (var i = 0; i < registers.Length; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{registers[i]}: {ram.Out}");
        }

        for (var i = registers.Length; i < 16; i++)
        {
            var reg = $"R{i}".PadRight(5, ' ');
            ram.Address = (short)i;
            Console.WriteLine($"{reg}: {ram.Out}");
        }

        Console.WriteLine("...");   

        var sp = ram[0];
        var lcl = ram[1];
        var arg = ram[2];

        for (var i = 256; i < 256 + 16; i++)
        {
            string pointer = string.Empty;
            if (i == sp)
            {
                pointer = " <- SP";
            }
            else if (i == lcl)
            {
                pointer = " <- LCL";
            }
            else if (i == arg)
            {
                pointer = " <- ARG";
            }
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}{pointer}");
        }
    }

    [ArgActionMethod]
    [ArgDescription("Assembles a Hack file into a binary")]
    public void Asm(ReadWriteArgs args)
    {
        if (File.Exists(args.Out))
        {
            File.Delete(args.Out);
        }

        var source = File.ReadAllText(args.Path);
        var code = Assembler.Assemble(source);
        using var @out = File.OpenWrite(args.Out);
        using var writer = new BinaryWriter(@out);
        Array.ForEach(code, s => writer.Write(s));
    }

    [ArgActionMethod]
    [ArgDescription("Transpiles an IL source file to Hack")]
    [ArgIgnoreCase]
    public void IL(ReadWriteOptionalArgs args)
    {
        void Write(string output)
        {
            if(string.IsNullOrWhiteSpace(args.Out))
            {
                Console.WriteLine(output);
            }
            else
            {
                File.WriteAllText(args.Out, output);
            }
        }

        var source = File.ReadAllText(args.Path);
        var input = new AntlrInputStream(source);
        var lexer = new ILLexer(input);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ILParser(tokens);
        var listener = new Transpiler(args.Path);
        parser.AddParseListener(listener);
        parser.program();
        Write(listener.Transpiled);
    }

    private short[] ReadBinary(string path)
    {
        var instructions = new List<short>();
        using (var stream = File.OpenRead(path))
        using (var reader = new BinaryReader(stream))
        {
            while (stream.Position < stream.Length)
            {
                instructions.Add(reader.ReadInt16());
            }
        }

        return instructions.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Args.InvokeAction<HackProgram>(args);
    }
}