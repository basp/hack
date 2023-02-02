using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Hack;

class TestListener : ILBaseListener
{
    private static readonly IDictionary<string, string> segments =
        new Dictionary<string, string>
        {
            ["local"] = "LCL",
            ["argument"] = "ARG",
            ["this"] = "THIS",
            ["that"] = "THAT",
            ["pointer"] = "R3",
            ["temp"] = "R5",
        };

    private readonly StringBuilder builder = new StringBuilder();

    public string Transpiled => this.builder.ToString();

    public override void EnterFunction([NotNull] ILParser.FunctionContext context)
    {
    }

    public override void ExitFunction(
        [NotNull] ILParser.FunctionContext context)
    {
        this.builder.AppendLine($"(END)         // defacto halt");
        this.builder.AppendLine($"    @END");
        this.builder.AppendLine($"    0;JMP");
    }

    public override void ExitPushConstant(
        [NotNull] ILParser.PushConstantContext context)
    {
        var index = context.UINT().GetText();
        this.builder.AppendLine($"// push constant {context.UINT()}");
        this.builder.AppendLine($"    @0");
        this.builder.AppendLine($"    D=A");
        this.builder.AppendLine($"    @{index}");
        this.builder.AppendLine($"    D=D+A");
        this.builder.AppendLine($"    @SP");
        this.builder.AppendLine($"    A=M       // A -> M[@SP]");
        this.builder.AppendLine($"    M=D       // M[M[@SP]] <- D");
        this.builder.AppendLine($"    @SP");
        this.builder.AppendLine($"    M=M+1     // M[@SP] <- M[@SP] + 1");
    }

    public override void ExitPopConstant(
        [NotNull] ILParser.PopConstantContext context)
    {
        var index = context.UINT().GetText();
        this.builder.AppendLine($"// pop constant {context.UINT()}");
        this.builder.AppendLine($"    @SP");
        this.builder.AppendLine($"    A=M");
        this.builder.AppendLine($"    D=M");
        this.builder.AppendLine($"    @SP");
        this.builder.AppendLine($"    M=M-1");
    }
}

class Program
{
    static void Main(string[] args)
    {
        RunAssembler(args[0]);
        // RunTranspiler(args[0]);
    }

    static void RunTranspiler(string path)
    {
        var source = File.ReadAllText(path);
        var input = new AntlrInputStream(source);
        var lexer = new ILLexer(input);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ILParser(tokens);
        var listener = new TestListener();
        parser.AddParseListener(listener);
        parser.function();
        Console.WriteLine(listener.Transpiled);
    }

    static void RunAssembler(string path)
    {
        const int SP = 256;

        var source = File.ReadAllText(path);

        var ram = new RAM32K();
        var rom = new ROM32K(Assembler.Assemble(source));

        ram[0] = SP;

        const int runs = 4;

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

