using System.Text;
using Antlr4.Runtime.Misc;
using Antlr4.StringTemplate;

class Transpiler2 : ILBaseListener
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

    private readonly TemplateGroupFile templateGroupFile;

    private string scope;

    public Transpiler2(string path, string templateGroupFile)
    {
        this.path = path;
        this.scope = Path.GetFileNameWithoutExtension(path);
        this.templateGroupFile = new TemplateGroupFile(
            Path.GetFullPath(templateGroupFile));
    }

    public string Transpiled => this.builder.ToString();

    public override void EnterProgram(
        [NotNull] ILParser.ProgramContext context)
    {
        var now = DateTime.Now;
        var ret = this.GenerateSymbol($"Sys.init.return");
        var st = this.templateGroupFile.GetInstanceOf("init");
        st.Add("generator", nameof(Transpiler2));
        st.Add("date", now.ToShortDateString());
        st.Add("time", now.ToShortTimeString());
        st.Add("init", "Sys.init");
        st.Add("nargs", 0);
        st.Add("ret", ret);
        this.builder.AppendLine(st.Render());
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
        var st = this.templateGroupFile.GetInstanceOf("return");
        this.builder.AppendLine(st.Render());
    }

    public override void ExitGoto([NotNull] ILParser.GotoContext context)
    {
        var name = context.NAME().GetText();
        var label = $"{this.scope}.{name}";
        var st = this.templateGroupFile.GetInstanceOf("goto");
        st.Add(nameof(label), label);
        this.builder.AppendLine(st.Render());
    }

    public override void ExitIfGoto(
        [NotNull] ILParser.IfGotoContext context)
    {
        var name = context.NAME().GetText();
        var label = $"{this.scope}.{name}";
        var st = this.templateGroupFile.GetInstanceOf("ifGoto");
        st.Add(nameof(label), label);
        this.builder.AppendLine(st.Render());
    }

    public override void ExitLabel(
        [NotNull] ILParser.LabelContext context)
    {
        var name = context.NAME().GetText();
        this.builder.AppendLine($"({this.scope}.{name})");
    }

    public override void ExitPushConstant(
        [NotNull] ILParser.PushConstantContext context)
    {
        var value = short.Parse(context.UINT().GetText());
        var st = this.templateGroupFile.GetInstanceOf("pushConstant");
        st.Add(nameof(value), value);
        this.builder.AppendLine(st.Render());
    }

    public override void ExitPushStatic(
        [NotNull] ILParser.PushStaticContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        var name = Path.GetFileNameWithoutExtension(this.path);
        var st = this.templateGroupFile.GetInstanceOf("pushStatic");
        st.Add(nameof(name), name);
        st.Add(nameof(index), index);
        this.builder.AppendLine(st.Render());
    }

    public override void ExitPopStatic(
        [NotNull] ILParser.PopStaticContext context)
    {
        var index = int.Parse(context.UINT().GetText());
        var name = Path.GetFileNameWithoutExtension(this.path);
        var st = this.templateGroupFile.GetInstanceOf("popStatic");
        st.Add(nameof(name), name);
        st.Add(nameof(index), index);
        this.builder.AppendLine(st.Render());
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

    private void WriteFunctionDeclaration(string fn, int nlocals)
    {
        var st = this.templateGroupFile.GetInstanceOf("functionDecl");
        var locals = Enumerable.Range(0, nlocals);
        st.Add(nameof(fn), fn);
        st.Add(nameof(locals), locals);
        this.builder.AppendLine(st.Render());
    }

    private void WriteCall(string fn, int nargs)
    {
        var ret = GenerateSymbol($"{fn}.return");
        var st = this.templateGroupFile.GetInstanceOf("call");
        st.Add(nameof(fn), fn);
        st.Add(nameof(nargs), nargs);
        st.Add(nameof(ret), ret);
        this.builder.AppendLine(st.Render());
    }

    private void WriteLogicalOp(string name, string jump)
    {
        var iftrue = GenerateSymbol(name);
        var st = this.templateGroupFile.GetInstanceOf("logicalOp");
        st.Add(nameof(name), name);
        st.Add(nameof(jump), jump);
        st.Add(nameof(iftrue), iftrue);
        this.builder.AppendLine(st.Render());
    }

    private void WriteUnaryOp(string name, string op)
    {
        var st = this.templateGroupFile.GetInstanceOf("unaryOp");
        st.Add(nameof(name), name);
        st.Add(nameof(op), op);
        this.builder.AppendLine(st.Render());
    }

    private void WriteBinaryOp(string name, string op)
    {
        var st = this.templateGroupFile.GetInstanceOf("binaryOp");
        st.Add(nameof(name), name);
        st.Add(nameof(op), op);
        this.builder.AppendLine(st.Render());
    }

    private void WritePushSegment(string segment, int index)
    {
        var symbol = segments[segment];
        var st = this.templateGroupFile.GetInstanceOf("pushSegment");
        st.Add(nameof(symbol), symbol);
        st.Add(nameof(segment), segment);
        st.Add(nameof(index), index);
        this.builder.AppendLine(st.Render());
    }

    private void WritePopSegment(string segment, int index)
    {
        var symbol = segments[segment];
        var st = this.templateGroupFile.GetInstanceOf("popSegment");
        st.Add(nameof(symbol), symbol);
        st.Add(nameof(segment), segment);
        st.Add(nameof(index), index);
        this.builder.AppendLine(st.Render());
    }

    private string GenerateSymbol(string key)
    {
        if (!this.generatedSymbols.TryAdd(key, 0))
        {
            this.generatedSymbols[key] += 1;
        }

        return $"{key}.{this.generatedSymbols[key]}";
    }
}
