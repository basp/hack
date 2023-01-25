namespace Hack.Hardware;

using System;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;

public class ChipBuilder : HDLBaseListener
{
    private static readonly IDictionary<string, Func<Chip>> factories =
        new Dictionary<string, Func<Chip>>
        {
            ["nand"] = () => new Nand(),
        };

    private Chip? chip;

    private Chip? part;


    public override void EnterChip([NotNull] HDLParser.ChipContext context)
    {
        this.chip = new Chip();
    }

    public override void ExitChip([NotNull] HDLParser.ChipContext context)
    {
        var id = context.chipName().GetText();
        this.chip!.Name = id;
    }

    public override void ExitInputs([NotNull] HDLParser.InputsContext context)
    {
        foreach (var input in context.pin())
        {
            var id = input.pinName().GetText();
            var width = input.pinWidth() != null
                ? int.Parse(input.pinWidth().GetText())
                : 1;
            var pins = Enumerable
                .Range(0, width)
                .Select(i => $"{id}[{i}]")
                .Select(x => new Pin(this.chip!, x, activate: true))
                .ToArray();
            this.chip!.Inputs[id] = pins;
        }
    }

    public override void ExitOutputs([NotNull] HDLParser.OutputsContext context)
    {
        foreach (var output in context.pin())
        {
            var id = output.pinName().GetText();
            var width = output.pinWidth() != null
                ? int.Parse(output.pinWidth().GetText())
                : 1;
            var pins = Enumerable
                .Range(0, width)
                .Select(i => $"{id}[{i}]")
                .Select(x => new Pin(this.chip!, x))
                .ToArray();
            this.chip!.Outputs[id] = pins;
        }
    }

    public override void ExitPartName([NotNull] HDLParser.PartNameContext context)
    {
        var name = context.ID().GetText().ToLowerInvariant();
        this.part = factories[name]();
    }

    public override void ExitConn([NotNull] HDLParser.ConnContext context)
    {
        var lhs = context.pin(0);
        var rhs = context.pin(1);
        var xn = lhs.pinName().GetText();
        var yn = rhs.pinName().GetText();
        var xi = lhs.pinWidth() != null
            ? int.Parse(lhs.pinWidth().GetText())
            : 0;
        var yi = rhs.pinWidth() != null
            ? int.Parse(rhs.pinWidth().GetText())
            : 0;

        Pin from, to;

        if (this.part!.Inputs.ContainsKey(xn))
        {
            to = this.part!.Inputs[xn][xi];

            if (this.chip!.Inputs.ContainsKey(yn))
            {
                from = this.chip!.Inputs[yn][yi];
            }
            else if (this.chip!.Internal.ContainsKey(yn))
            {
                from = this.chip!.Internal[yn][yi];
            }
            else
            {
                from = new Pin(this.chip!, yn);
            }
            
            from.Connect(to);
        }
        else if (this.part!.Outputs.ContainsKey(xn))
        {
            from = this.part!.Outputs[xn][xi];

            if (this.chip!.Outputs.ContainsKey(yn))
            {
                to = this.chip!.Outputs[yn][yi];
            }
            else if (this.chip!.Internal.ContainsKey(xn))
            {
                to = this.chip!.Internal[yn][yi];
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        else
        {
            var msg = "Connection must include either part input or output on left-hand side.";
            throw new Exception(msg);
        }
    }
}