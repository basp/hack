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

[ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
public class HackProgram
{
    [HelpHook]
    [ArgDescription("Shows this help")]
    [ArgShortcut("-?")]
    public bool Help { get; set; }

    [ArgActionMethod]
    [ArgDescription("Transpiles an IL source file to Hack")]
    [ArgIgnoreCase]
    public void Transpile(PathArgs args)
    {
        var source = File.ReadAllText(args.Path);
        var input = new AntlrInputStream(source);
        var lexer = new ILLexer(input);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ILParser(tokens);
        var listener = new TestListener();
        parser.AddParseListener(listener);
        parser.function();
        Console.WriteLine(listener.Transpiled);
    }

    [ArgActionMethod]
    [ArgDescription("Assembles and runs a Hack assembly file.")]
    [ArgIgnoreCase]
    public void Assemble(PathArgs args)
    {
        var source = File.ReadAllText(args.Path);

        var ram = new RAM32K();
        var rom = new ROM32K(Assembler.Assemble(source));

        // Stack starts at position 256.
        const int SP = 256;
        ram[0] = SP;

        // The amount of time we want this program to
        // run using the same ram. This means that results
        // in memory will accumulate over time as the
        // program will overwrite and add data each run it
        // is executed. Nothing is reset.
        const int runs = (1 << 10);

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

        for (var i = SP; i < SP + 16; i++)
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