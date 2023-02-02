using System.Text;
using Antlr4.Runtime.Misc;

class TestListener : ILBaseListener
{
    private static readonly IDictionary<string, string> segments =
        new Dictionary<string, string>
        {
            // static and constant segments are handled
            // differently so they are absent here
            ["local"] = "LCL",
            ["argument"] = "ARG",
            ["this"] = "THIS",
            ["that"] = "THAT",
            ["pointer"] = "R3",
            ["temp"] = "R5",
        };

    private readonly StringBuilder builder = new StringBuilder();

    public string Transpiled => this.builder.ToString();

    public override void ExitFunction(
        [NotNull] ILParser.FunctionContext context)
    {
        this.Append($"(END)");
        this.Append($"    @END      // defacto halt");
        this.Append($"    0;JMP");
    }

    public override void ExitPushStatic(
        [NotNull] ILParser.PushStaticContext context)
    {
        this.Append($"@Static.{context.UINT()}");
        this.Append($"");
    }

    public override void ExitPushConstant(
        [NotNull] ILParser.PushConstantContext context)
    {
        var index = context.UINT().GetText();
        this.Append($"// push constant {context.UINT()}");
        this.Append($"    @0");
        this.Append($"    D=A");
        this.Append($"    @{index}");
        this.Append($"    D=D+A");
        this.Append($"    @SP");
        this.Append($"    A=M       // M -> M[M[@SP]]");
        this.Append($"    M=D       // store stack value");
        this.Append($"    @SP");
        this.Append($"    M=M+1     // increment stack pointer");
    }

    public override void ExitPopConstant(
        [NotNull] ILParser.PopConstantContext context)
    {
        var index = context.UINT().GetText();
        this.Append($"// pop constant {context.UINT()}");
        this.Append($"    @SP");
        this.Append($"    A=M       // M -> M[M[@SP]]");
        this.Append($"    D=M       // read but just ignore");
        this.Append($"    @SP");
        this.Append($"    M=M-1     // decrement stack pointer");
    }

    public override void EnterAdd(
        [NotNull] ILParser.AddContext context)
    {
        this.Append($"// add");
        this.Append($"// pop x");
    }

    private void Append(string command) =>
        this.builder.AppendLine(command);
}
