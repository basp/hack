using System.Text;
using Antlr4.Runtime.Misc;

class Transpiler : ILBaseListener
{
    private const string @constant = "constant";
    private const string @local = "local";
    private const string @argument = "argument";
    private const string @this = "this";
    private const string @that = "that";
    private const string @pointer = "pointer";
    private const string @temp = "temp";
    private const string @add = "add";
    private const string @sub = "sub";
    private const string @neg = "neg";
    private const string @and = "and";
    private const string @or = "or";
    private const string @not = "not";
    private const string @eq = "eq";
    private const string @gt = "gt";
    private const string @lt = "lt";

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

    private readonly string path;

    private string scope;

    public Transpiler(string path)
    {
        this.path = path;
        this.scope = Path.GetFileNameWithoutExtension(path);
    }

    public string Transpiled => this.builder.ToString();

    public override void EnterProgram(
        [NotNull] ILParser.ProgramContext context)
    {
        this.Write($"// Generated by {nameof(Transpiler)} on {DateTime.Now.ToShortDateString()} @ {DateTime.Now.ToShortTimeString()}");
        this.WriteIndented("// Point SP, LCL and ARG to base of stack.");
        this.WriteIndented("@256");
        this.WriteIndented("D=A");
        this.WriteIndented("@SP");
        this.WriteIndented("M=D");
        this.WriteIndented("@LCL");
        this.WriteIndented("M=D");
        this.WriteIndented("@ARG");
        this.WriteIndented("M=D");
        this.WriteIndented("// Point THIS and THAT to base of heap");
        this.WriteIndented("@2048");
        this.WriteIndented("D=A");
        this.WriteIndented("@THIS");
        this.WriteIndented("M=D");
        this.WriteIndented("@THAT");
        this.WriteIndented("M=D");
        this.WriteCall("Sys.init", 0);
        // Should probably leave this value on top 
        // of the stack as the program return value.
        // this.WriteIndented("@SP");
        // this.WriteIndented("M=M-1");
        this.Write($"(END)");
        this.WriteIndented($"@END");
        this.WriteIndented($"0;JMP");
    }

    public override void ExitFunction(
        [NotNull] ILParser.FunctionContext context)
    {
        var f = context.NAME().GetText();
        var n = int.Parse(context.UINT().GetText());
        this.WriteFunctionDeclaration(f, n);
    }

    public override void ExitCall([NotNull] ILParser.CallContext context)
    {
        var f = context.NAME().GetText();
        var n = int.Parse(context.UINT().GetText());
        this.WriteCall(f, n);
    }

    public override void ExitReturn(
        [NotNull] ILParser.ReturnContext context)
    {
        this.WriteReturn();
    }

    public override void ExitGoto([NotNull] ILParser.GotoContext context)
    {
        var name = context.NAME().GetText();
        var label = $"{this.scope}.{name}";
        this.WriteIndented($"// goto {label}");
        this.WriteIndented($"@{label}");
        this.WriteIndented("0;JMP");
    }

    public override void ExitIfGoto(
        [NotNull] ILParser.IfGotoContext context)
    {
        var name = context.NAME().GetText();
        var label = $"{this.scope}.{name}";
        this.WriteIndented($"// if-goto {label}");
        this.WriteIndented("@SP");
        this.WriteIndented("AM=M-1");
        this.WriteIndented("D=M");
        this.WriteIndented($"@{label}");
        this.WriteIndented("D;JNE");
    }

    public override void ExitLabel(
        [NotNull] ILParser.LabelContext context)
    {
        var name = context.NAME().GetText();
        this.Write($"({this.scope}.{name})");
    }

    public override void ExitPushConstant(
        [NotNull] ILParser.PushConstantContext context)
    {
        var value = short.Parse(context.UINT().GetText());
        this.WritePushConstant(value);
    }

    public override void ExitPushStatic(
        [NotNull] ILParser.PushStaticContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        var name = Path.GetFileNameWithoutExtension(this.path);
        this.WriteIndented($"// push static {index}");
        this.WriteIndented($"@{name}.{index}");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@SP");
        this.WriteIndented($"A=M");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    public override void ExitPopStatic(
        [NotNull] ILParser.PopStaticContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        var name = Path.GetFileNameWithoutExtension(this.path);
        this.WriteIndented($"// pop static {index}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"AM=M-1");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@{name}.{index}");
        this.WriteIndented($"M=D");
    }

    public override void ExitPushLocal(
        [NotNull] ILParser.PushLocalContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@local, index);
    }

    public override void ExitPopLocal(
        [NotNull] ILParser.PopLocalContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@local, index);
    }

    public override void ExitPushArgument(
        [NotNull] ILParser.PushArgumentContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@argument, index);
    }

    public override void ExitPopArgument(
        [NotNull] ILParser.PopArgumentContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@argument, index);
    }

    public override void ExitPushThis(
        [NotNull] ILParser.PushThisContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@this, index);
    }

    public override void ExitPopThis(
        [NotNull] ILParser.PopThisContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@this, index);
    }

    public override void ExitPushThat(
        [NotNull] ILParser.PushThatContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@that, index);
    }

    public override void ExitPopThat(
        [NotNull] ILParser.PopThatContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@that, index);
    }

    public override void ExitPushPointer(
        [NotNull] ILParser.PushPointerContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@pointer, index);
    }

    public override void ExitPopPointer(
        [NotNull] ILParser.PopPointerContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@pointer, index);
    }

    public override void ExitPushTemp([NotNull] ILParser.PushTempContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePushSegment(@temp, index);
    }

    public override void ExitPopTemp([NotNull] ILParser.PopTempContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        this.WritePopSegment(@temp, index);
    }

    public override void ExitAdd([NotNull] ILParser.AddContext context)
    {
        WriteBinaryOp(@add, "D=D+M");
    }

    public override void ExitSub([NotNull] ILParser.SubContext context)
    {
        WriteBinaryOp(@sub, "D=M-D");
    }

    public override void ExitAnd([NotNull] ILParser.AndContext context)
    {
        WriteBinaryOp(@and, "D=D&M");
    }

    public override void ExitOr([NotNull] ILParser.OrContext context)
    {
        WriteBinaryOp(@or, "D=D|M");
    }

    public override void ExitNeg([NotNull] ILParser.NegContext context)
    {
        WriteUnaryOp(@neg, "M=M-1");
    }

    public override void ExitNot([NotNull] ILParser.NotContext context)
    {
        WriteUnaryOp(@not, "M=!M");
    }

    public override void ExitEq([NotNull] ILParser.EqContext context)
    {
        WriteLogicalOp(@eq, "JEQ");
    }

    public override void ExitGt([NotNull] ILParser.GtContext context)
    {
        WriteLogicalOp(@gt, "JGT");
    }

    public override void ExitLt([NotNull] ILParser.LtContext context)
    {
        WriteLogicalOp(@lt, "JLT");
    }

    private void WriteReturn()
    {
        this.WriteIndented("// Store the top of the frame (FRAME) in temp register");
        this.WriteIndented("@LCL");
        this.WriteIndented("D=M");
        this.WriteIndented("@R13");
        this.WriteIndented("M=D");

        this.WriteIndented("// Put return address in temp register");
        this.WriteIndented("@5");
        this.WriteIndented("A=D-A");
        this.WriteIndented("D=M");
        this.WriteIndented("@R14");
        this.WriteIndented("M=D");

        this.WriteIndented("// Reposition the return value for the caller");
        this.WriteIndented("@SP");
        this.WriteIndented("AM=M-1");
        this.WriteIndented("D=M");
        this.WriteIndented("@ARG");
        this.WriteIndented("A=M");
        this.WriteIndented("M=D");

        this.WriteIndented("// Restore SP of the caller");
        this.WriteIndented("@ARG");
        this.WriteIndented("D=M+1");
        this.WriteIndented("@SP");
        this.WriteIndented("M=D");

        this.WriteIndented("// Restore THAT of the caller");
        this.WriteIndented("@R13");
        this.WriteIndented("D=M");
        this.WriteIndented("@1");
        this.WriteIndented("A=D-A");
        this.WriteIndented("D=M");
        this.WriteIndented("@THAT");
        this.WriteIndented("M=D");

        this.WriteIndented("// Restore THIS of the caller");
        this.WriteIndented("@R13");
        this.WriteIndented("D=M");
        this.WriteIndented("@2");
        this.WriteIndented("A=D-A");
        this.WriteIndented("D=M");
        this.WriteIndented("@THIS");
        this.WriteIndented("M=D");

        this.WriteIndented("// Restore ARG of the caller");
        this.WriteIndented("@R13");
        this.WriteIndented("D=M");
        this.WriteIndented("@3");
        this.WriteIndented("A=D-A");
        this.WriteIndented("D=M");
        this.WriteIndented("@ARG");
        this.WriteIndented("M=D");

        this.WriteIndented("// Restore LCL of the caller");
        this.WriteIndented("@R13");
        this.WriteIndented("D=M");
        this.WriteIndented("@4");
        this.WriteIndented("A=D-A");
        this.WriteIndented("D=M");
        this.WriteIndented("@LCL");
        this.WriteIndented("M=D");

        this.WriteIndented("// Goto return address");
        this.WriteIndented("@R14");
        this.WriteIndented("A=M");
        this.WriteIndented("0;JMP");
    }

    private void WriteFunctionDeclaration(string f, int n)
    {
        this.Write($"({f})");
        for (var i = 0; i < n; i++)
        {
            this.WriteIndented($"// push local {i}");
            this.WriteIndented("D=0");
            this.WriteIndented("@SP");
            this.WriteIndented("A=M");
            this.WriteIndented("M=D");
            this.WriteIndented("@SP");
            this.WriteIndented("M=M+1");
        }
    }

    private void WritePushAddress(short value)
    {
        this.WritePushSymbol(value.ToString());
    }

    private void WritePushAddress(string sym)
    {
        this.WritePushSymbol(sym);
    }

    private void WritePushMemory(string sym)
    {
        this.WriteIndented($"@{sym}");
        this.WriteIndented("D=M");
        this.WriteIndented("@SP");
        this.WriteIndented("A=M");
        this.WriteIndented("M=D");
        this.WriteIndented("@SP");
        this.WriteIndented("M=M+1");
    }

    private void WriteCall(string f, int n)
    {
        var ret = GenerateSymbol($"{f}.return");

        this.WriteIndented($"// call {f} {n}");
        this.WriteIndented("// Push return address");
        this.WritePushAddress(ret);

        this.WriteIndented("// Save LCL of calling function");
        this.WritePushMemory("LCL");

        this.WriteIndented("// Save ARG of calling function");
        this.WritePushMemory("ARG");

        this.WriteIndented("// Save THIS of calling function");
        this.WritePushMemory("THIS");

        this.WriteIndented("// Save THAT of calling function");
        this.WritePushMemory("THAT");

        this.WriteIndented("// Reposition ARG for callee");
        this.WriteIndented("@SP");
        this.WriteIndented("D=M");
        this.WriteIndented($"@{n}");
        this.WriteIndented("D=D-A");
        this.WriteIndented("@5");
        this.WriteIndented("D=D-A");
        this.WriteIndented("@ARG");
        this.WriteIndented("M=D");

        this.WriteIndented("// Reposition LCL for callee");
        this.WriteIndented("@SP");
        this.WriteIndented("D=M");
        this.WriteIndented("@LCL");
        this.WriteIndented("M=D");

        this.WriteIndented("// Transfer control");
        this.WriteIndented($"@{f}");
        this.WriteIndented("0;JMP");

        // Declare label for return address
        this.Write($"({ret})");
    }

    private void WriteLogicalOp(string name, string jump)
    {
        var exit = GenerateSymbol(name);
        this.WriteIndented($"// {name}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"A=M");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M-1");
        this.WriteIndented($"A=M");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M-1");
        this.WriteIndented($"A=M");
        this.WriteIndented($"D=M-D");
        this.WriteIndented($"M=-1");
        this.WriteIndented($"@{exit}");
        this.WriteIndented($"D;{jump}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"A=M");
        this.WriteIndented($"M=0");
        this.Write($"({exit})");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    private void WriteUnaryOp(string name, string op)
    {
        this.WriteIndented($"// {name}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"AM=M-1");
        this.WriteIndented($"{op}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    private void WriteBinaryOp(string name, string op)
    {
        this.WriteIndented($"// {name}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"AM=M-1");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@SP");
        this.WriteIndented($"AM=M-1");
        this.WriteIndented($"{op}");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    private void WritePushSegment(string segment, int index)
    {
        var symbol = segments[segment];
        this.WriteIndented($"// push {segment} {index}");
        this.WriteIndented($"@{symbol}");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@{index}");
        this.WriteIndented($"A=D+A");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@SP");
        this.WriteIndented($"A=M");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    private void WritePopSegment(string segment, int index)
    {
        var symbol = segments[segment];
        this.WriteIndented($"// pop {segment} {index}");
        this.WriteIndented($"@SP");
        this.WriteIndented($"AM=M-1");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@R13");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@{symbol}");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@{index}");
        this.WriteIndented($"D=D+A");
        this.WriteIndented($"@R14");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@R13");
        this.WriteIndented($"D=M");
        this.WriteIndented($"@R14");
        this.WriteIndented($"A=M");
        this.WriteIndented($"M=D");
    }

    private void WritePushConstant(short value)
    {
        this.WriteIndented($"// push constant {value}");
        this.WriteIndented($"@{value}");
        this.WriteIndented($"D=A");
        this.WriteIndented($"@SP");
        this.WriteIndented($"A=M");
        this.WriteIndented($"M=D");
        this.WriteIndented($"@SP");
        this.WriteIndented($"M=M+1");
    }

    private void WritePushSymbol(string sym)
    {
        this.WriteIndented($"@{sym}");
        this.WriteIndented("D=A");
        this.WriteIndented("@SP");
        this.WriteIndented("A=M");
        this.WriteIndented("M=D");
        this.WriteIndented("@SP");
        this.WriteIndented("M=M+1");
    }

    private void WriteIndented(string s, int indent = 4) =>
        this.builder.AppendLine(string.Concat("".PadRight(indent, ' '), s));

    private void Write(string command) =>
        this.builder.AppendLine(command);

    private string GetQualifiedFunctionName(string f) =>
        string.Concat(Path.GetFileNameWithoutExtension(this.path), ".", f);

    private string GenerateSymbol(string key)
    {
        if (!this.generatedSymbols.TryAdd(key, 0))
        {
            this.generatedSymbols[key] += 1;
        }

        return $"{key}.{this.generatedSymbols[key]}";
    }
}
