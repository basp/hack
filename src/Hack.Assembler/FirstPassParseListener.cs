using Antlr4.Runtime.Misc;

namespace Hack.Assembler;

public class FirstPassParseListener : HackBaseListener
{
    private readonly IDictionary<string, ushort> labels =
        new Dictionary<string, ushort>();

    private ushort ip = 0;

    public IDictionary<string, ushort> Labels => this.labels;

    public override void EnterProgram(
        [NotNull] HackParser.ProgramContext context)
    {
        this.labels.Clear();
    }

    public override void ExitInstruction(
        [NotNull] HackParser.InstructionContext context)
    {
        this.ip += 1;
    }

    public override void ExitLabel(
        [NotNull] HackParser.LabelContext context)
    {
        var id = context.NAME().GetText();
        labels.Add(id, this.ip);
    }
}