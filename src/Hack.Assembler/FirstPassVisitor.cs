using Antlr4.Runtime.Misc;

namespace Hack.Assembler;

public class FirstPassVisitor : HackBaseVisitor<IDictionary<string, ushort>>
{
    private readonly IDictionary<string, ushort> labels =
        new Dictionary<string, ushort>();

    private ushort ip = 0;

    public override IDictionary<string, ushort> VisitInstruction(
        [NotNull] HackParser.InstructionContext context)
    {
        this.ip += 1;
        return this.labels;
    }

    public override IDictionary<string, ushort> VisitLabel(
        [NotNull] HackParser.LabelContext context)
    {
        var name = context.NAME().GetText();
        this.labels.Add(name, this.ip);
        return this.labels;
    }
}