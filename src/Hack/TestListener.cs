using System.Text;
using Antlr4.Runtime.Misc;

class TestListener : ILBaseListener
{
    private const string @constant = "constant";
    private const string @local = "local";
    private const string @argument = "argument";
    private const string @this = "this";
    private const string @that = "that";
    private const string @pointer = "pointer";
    private const string @temp = "temp";

    private static readonly IDictionary<string, string> segments =
        new Dictionary<string, string>
        {
            // static and constant segments are handled
            // differently so they are absent here
            [@local] = "LCL",
            [@argument] = "ARG",
            [@this] = "THIS",
            [@that] = "THAT",
            [@pointer] = "R3", 
            [@temp] = "R5",
        };

    private readonly IDictionary<string, int> generatedSymbols =
        new Dictionary<string, int>();

    private readonly StringBuilder builder = new StringBuilder();

    public string Transpiled => this.builder.ToString();

    public override void EnterFunction(
        [NotNull] ILParser.FunctionContext context)
    {
        this.Append($"// Generated by {nameof(TestListener)} on {DateTime.Now.ToShortDateString()} @ {DateTime.Now.ToShortTimeString()}");
    }

    public override void ExitFunction(
        [NotNull] ILParser.FunctionContext context)
    {
        this.Append($"(END)");
        this.Append($"    @END");
        this.Append($"    0;JMP");
    }

    public override void ExitPushConstant(
        [NotNull] ILParser.PushConstantContext context)
    {
        var value = context.UINT().GetText();
        this.Append($"// push constant {context.UINT()}");
        this.Append($"    @{value}");
        this.Append($"    D=A");
        this.Append($"    @SP");
        this.Append($"    A=M");
        this.Append($"    M=D");
        this.Append($"    @SP");
        this.Append($"    M=M+1");
    }

    public override void ExitPushLocal(
        [NotNull] ILParser.PushLocalContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PushSegmentIndex(@local, index);
    }

    public override void ExitPopLocal(
        [NotNull] ILParser.PopLocalContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PopSegmentIndex(@local, index);
    }

    public override void ExitPushArgument(
        [NotNull] ILParser.PushArgumentContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PushSegmentIndex(@argument, index);
    }

    public override void ExitPopArgument(
        [NotNull] ILParser.PopArgumentContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PopSegmentIndex(@argument, index);
    }

    public override void ExitPushThis(
        [NotNull] ILParser.PushThisContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PushSegmentIndex(@this, index);
    }

    public override void ExitPopThis(
        [NotNull] ILParser.PopThisContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PopSegmentIndex(@this, index);
    }

    public override void ExitPushThat(
        [NotNull] ILParser.PushThatContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PushSegmentIndex(@that, index);
    }

    public override void ExitPopThat([NotNull] ILParser.PopThatContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.PopSegmentIndex(@that, index);
    }

    public override void ExitAdd([NotNull] ILParser.AddContext context)
    {
        TranspileBinaryOp("add", "D=D+M");
    }

    public override void ExitSub([NotNull] ILParser.SubContext context)
    {
        TranspileBinaryOp("sub", "D=M-D");
    }

    public override void ExitAnd([NotNull] ILParser.AndContext context)
    {
        TranspileBinaryOp("and", "D=D&M");
    }

    public override void ExitOr([NotNull] ILParser.OrContext context)
    {
        TranspileBinaryOp("or", "D=D|M");
    }

    public override void ExitNeg([NotNull] ILParser.NegContext context)
    {
        TranspileUnaryOp("neg", "M=M-1");
    }

    public override void ExitNot([NotNull] ILParser.NotContext context)
    {
        TranspileUnaryOp("not", "M=!M");
    }

    public override void ExitEq([NotNull] ILParser.EqContext context)
    {
        TranspileLogicalOp("eq", "JEQ");
    }

    public override void ExitGt([NotNull] ILParser.GtContext context)
    {
        TranspileLogicalOp("gt", "JGT");
    }

    public override void ExitLt([NotNull] ILParser.LtContext context)
    {
        TranspileLogicalOp("lt", "JLT");
    }

    private void TranspileLogicalOp(string name, string jump)
    {
        var exit = GenerateSymbol(name);
        this.Append($"// {name}");
        this.Append($"    @SP");
        this.Append($"    A=M");
        this.Append($"    D=M");
        this.Append($"    @SP");
        this.Append($"    M=M-1");
        this.Append($"    A=M");
        this.Append($"    D=M");
        this.Append($"    @SP");
        this.Append($"    M=M-1");
        this.Append($"    A=M");
        this.Append($"    D=M-D");
        this.Append($"    M=-1");   
        this.Append($"    @{exit}");
        this.Append($"    D;{jump}");
        this.Append($"    @SP");    
        this.Append($"    A=M");
        this.Append($"    M=0");
        this.Append($"({exit})");
        this.Append($"    @SP");
        this.Append($"    M=M+1");
    }

    private void TranspileUnaryOp(string name, string op)
    {
        this.Append($"// {name}");
        this.Append($"    @SP");
        this.Append($"    M=M-1");
        this.Append($"    A=M");
        this.Append($"    {op}");
        this.Append($"    @SP");
        this.Append($"    M=M+1");
    }

    private void TranspileBinaryOp(string name, string op)
    {
        this.Append($"// {name}");
        this.Append($"    @SP");
        this.Append($"    M=M-1");
        this.Append($"    A=M");
        this.Append($"    D=M");
        this.Append($"    @SP");
        this.Append($"    M=M-1");
        this.Append($"    A=M");
        this.Append($"    {op}");
        this.Append($"    M=D");
        this.Append($"    @SP");
        this.Append($"    M=M+1");
    }

    private void PushSegmentIndex(string segment, int index)
    {
        var symbol = segments[segment];
        this.Append($"// push {segment} {index}");
        this.Append($"    @{symbol}");
        this.Append($"    D=M");
        this.Append($"    @{index}");
        this.Append($"    A=D+A");
        this.Append($"    D=M");
        this.Append($"    @SP");
        this.Append($"    A=M");
        this.Append($"    M=D");
        this.Append($"    @SP");
        this.Append($"    M=M+1");     
    }

    private void PopSegmentIndex(string segment, int index)
    {
        var symbol = segments[segment];
        this.Append($"// pop {segment} {index}");
        this.Append($"    @SP");
        this.Append($"    AM=M-1");
        this.Append($"    D=M");
        this.Append($"    @R13");
        this.Append($"    M=D");
        this.Append($"    @{symbol}");
        this.Append($"    D=M");
        this.Append($"    @{index}");
        this.Append($"    D=D+A");
        this.Append($"    @R14");
        this.Append($"    M=D");
        this.Append($"    @R13");
        this.Append($"    D=M");
        this.Append($"    @R14");
        this.Append($"    A=M");
        this.Append($"    M=D");
    }

    private void Append(string command) =>
        this.builder.AppendLine(command);

    private string GenerateSymbol(string key)
    {
        if (!this.generatedSymbols.TryAdd(key, 0))
        {
            this.generatedSymbols[key] += 1;
        }

        return $"{key}.{this.generatedSymbols[key]}";
    }
}
