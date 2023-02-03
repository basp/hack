using Antlr4.Runtime;
using Hack;
using PowerArgs;

using Args = PowerArgs.Args;

public class PathArgs
{
    [ArgRequired]
    [ArgDescription("The path to the source file")]
    [ArgPosition(1)]
    public string Path { get; set; } = string.Empty;
}

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

[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
public class HackProgram
{
    [HelpHook]
    [ArgDescription("Shows this help")]
    [ArgShortcut("-?")]
    public bool Help { get; set; }

    [ArgActionMethod]
    [ArgDescription("Runs a Hack binary")]
    public void Bin(ReadArgs args)
    {
        const int SP = 256;
        const int LCL = 2048;
        const int ARG = LCL + 256;

        var instructions = new List<short>();
        using (var stream = File.OpenRead(args.Path))
        using (var reader = new BinaryReader(stream))
        {
            while (stream.Position < stream.Length)
            {
                instructions.Add(reader.ReadInt16());
            }
        }


        var rom = new ROM32K(instructions.ToArray());
        var ram = new RAM32K()
        {
            [0] = SP,
            [1] = LCL,
            [2] = ARG,
        };

        // The amount of time we want this program to
        // run using the same ram. This means that results
        // in memory will accumulate over time as the
        // program will overwrite and add data each run it
        // is executed. Nothing is reset.
        const int runs = (1 << 0);

        var start = DateTime.Now;
        for (var i = 0; i < runs; i++)
        {
            var sim = new Simulator(rom, ram);
            sim.Run();
        }

        var finish = DateTime.Now;
        var duration = (finish - start);
        var seconds = Math.Round(duration.TotalSeconds, 2);
        var avg = Math.Round(duration.TotalMilliseconds / runs, 2);

        Console.WriteLine($"{runs} runs in {seconds}s (avg. {avg}ms/run)");

        for (var i = 0; i < 16; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
        }

        Console.WriteLine("...");

        for (var i = SP; i < SP + 8; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
        }

        Console.WriteLine("...");

        for (var i = ARG; i < ARG + 8; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
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
    public void Transpile(ReadWriteArgs args)
    {
        var source = File.ReadAllText(args.Path);
        var input = new AntlrInputStream(source);
        var lexer = new ILLexer(input);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ILParser(tokens);
        var listener = new TestListener();
        parser.AddParseListener(listener);
        parser.function();
        File.WriteAllText(args.Out, listener.Transpiled);
    }

    [ArgActionMethod]
    [ArgDescription("Test a Hack assembly file.")]
    [ArgIgnoreCase]
    public void Test2(PathArgs args)
    {
        var source = File.ReadAllText(args.Path);

        var ram = new RAM32K();
        var rom = new ROM32K(Assembler.Assemble(source));

        const int SP = 256;
        const int LCL = 2048;
        const int ARG = LCL + 256;

        ram[0] = SP;
        ram[1] = LCL;
        ram[2] = ARG;

        // The amount of time we want this program to
        // run using the same ram. This means that results
        // in memory will accumulate over time as the
        // program will overwrite and add data each run it
        // is executed. Nothing is reset.
        const int runs = (1 << 0);

        var start = DateTime.Now;
        for (var i = 0; i < runs; i++)
        {
            var sim = new Simulator(rom, ram);
            sim.Run();
        }

        var finish = DateTime.Now;
        var duration = (finish - start);
        var seconds = Math.Round(duration.TotalSeconds, 2);
        var avg = Math.Round(duration.TotalMilliseconds / runs, 2);

        Console.WriteLine($"{runs} runs in {seconds}s (avg. {avg}ms/run)");

        for (var i = 0; i < 16; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
        }

        Console.WriteLine("...");

        for (var i = SP; i < SP + 8; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
        }

        Console.WriteLine("...");

        for (var i = ARG; i < ARG + 8; i++)
        {
            ram.Address = (short)i;
            Console.WriteLine($"{i}: {ram.Out}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Args.InvokeAction<HackProgram>(args);
    }
}