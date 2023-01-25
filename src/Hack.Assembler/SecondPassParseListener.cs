using Antlr4.Runtime.Misc;

namespace Hack.Assembler;

public class SecondPassParseListener : HackBaseListener
{
    const int DebugOutputPadding = 6;

    private static readonly IDictionary<string, ushort> DefaultSymbols =
        new Dictionary<string, ushort>()
        {
            ["SP"] = 0,
            ["LCL"] = 1,
            ["ARG"] = 2,
            ["THIS"] = 3,
            ["THAT"] = 4,
            ["R0"] = 0,
            ["R1"] = 1,
            ["R2"] = 2,
            ["R3"] = 3,
            ["R4"] = 4,
            ["R5"] = 5,
            ["R6"] = 6,
            ["R7"] = 7,
            ["R8"] = 8,
            ["R9"] = 9,
            ["R10"] = 10,
            ["R11"] = 11,
            ["R12"] = 12,
            ["R13"] = 13,
            ["R14"] = 14,
            ["R15"] = 15,
        };

    private static readonly IDictionary<string, ushort> computations =
        new Dictionary<string, ushort>
        {
            ["0"] = 0b1110_101010_000000,
            ["1"] = 0b1110_111111_000000,
            ["-1"] = 0b1110_111010_000000,
            ["D"] = 0b1110_001100_000000,
            ["A"] = 0b1110_110000_000000,
            ["!D"] = 0b1110_001101_000000,
            ["!A"] = 0b1110_110001_000000,
            ["-D"] = 0b1110_001111_000000,
            ["-A"] = 0b1110_110011_000000,
            ["D+1"] = 0b1110_011111_000000,
            ["A+1"] = 0b1110_110111_000000,
            ["D-1"] = 0b1110_001110_000000,
            ["A-1"] = 0b1110_110010_000000,
            ["D+A"] = 0b1110_000010_000000,
            ["D-A"] = 0b1110_010011_000000,
            ["A-D"] = 0b1110_000111_000000,
            ["D&A"] = 0b1110_000000_000000,
            ["D|A"] = 0b1110_010101_000000,
            ["M"] = 0b1111_110000_000000,
            ["!M"] = 0b1111_110001_000000,
            ["-M"] = 0b1111_110011_000000,
            ["M+1"] = 0b1111_110111_000000,
            ["M-1"] = 0b1111_110010_000000,
            ["D+M"] = 0b1111_000010_000000,
            ["D-M"] = 0b1111_010011_000000,
            ["M-D"] = 0b1111_000111_000000,
            ["D&M"] = 0b1111_000000_000000,
            ["D|M"] = 0b1111_010101_000000,
        };

    private static readonly IDictionary<string, ushort> destinations =
        new Dictionary<string, ushort>
        {
            ["M"] = 0b0000000000_001_000,
            ["D"] = 0b0000000000_010_000,
            ["MD"] = 0b0000000000_011_000,
            ["A"] = 0b0000000000_100_000,
            ["AM"] = 0b0000000000_101_000,
            ["AD"] = 0b0000000000_110_000,
            ["AMD"] = 0b0000000000_111_000,
        };

    private static readonly IDictionary<string, ushort> jumps =
        new Dictionary<string, ushort>
        {
            ["JGT"] = 0b0000000000_000_000,
            ["JEQ"] = 0b0000000000_000_001,
            ["JGE"] = 0b0000000000_000_010,
            ["JLT"] = 0b0000000000_000_011,
            ["JNE"] = 0b0000000000_000_100,
            ["JLE"] = 0b0000000000_000_101,
            ["JMP"] = 0b0000000000_000_111,
        };

    private IList<ushort> instructions =
        new List<ushort>();

    private IDictionary<string, ushort> symbols =
        new Dictionary<string, ushort>();

    public SecondPassParseListener(IDictionary<string, ushort> labels)
    {
        InitializeSymbols(labels);
    }

    public IDictionary<string, ushort> Symbols => this.symbols;

    public IList<ushort> Instructions => this.instructions;

    public override void EnterProgram(
        [NotNull] HackParser.ProgramContext context)
    {
        this.instructions.Clear();
    }

    public override void ExitCompute(
        [NotNull] HackParser.ComputeContext context)
    {
        var instruction = this.Compile(context);
        this.instructions.Add(instruction);

        // debug
        var str = Convert.ToString(instruction, 2);
        var prefix = str.Substring(0, 4);
        var a = str.Substring(3, 1);
        var comp = str.Substring(4, 6);
        var dest = str.Substring(10, 3);
        var jump = str.Substring(13, 3);
        var lhs = $"{context.GetText()}".PadRight(DebugOutputPadding);
        var rhs = $"{a}.{comp}.{dest}.{jump} ({instruction})";
        Console.WriteLine($"{lhs} -> {rhs}");
    }

    public override void ExitAddress(
        [NotNull] HackParser.AddressContext context)
    {
        var instruction = Compile(context);
        this.instructions.Add(instruction);

        // debug
        var lhs = $"{context.GetText()}".PadRight(DebugOutputPadding);
        var rhs = $"{instruction}";
        Console.WriteLine($"{lhs} -> {rhs}");
    }

    private ushort Compile(
        [NotNull] HackParser.ComputeContext context)
    {
        var comp = computations[context.comp().GetText()];

        var dest = context.dest() != null
            ? destinations[context.dest().GetText()]
            : (ushort)0;

        var jump = context.jump() != null
            ? jumps[context.jump().GetText()]
            : (ushort)0;

        return (ushort)(comp | dest | jump);
    }

    private ushort Compile(
        [NotNull] HackParser.AddressContext context)
    {
        const ushort max = 0b0111_1111_1111_1111;
        var address = GetAddressValue(context);
        if (address > max)
        {
            throw new InvalidOperationException();
        }

        return address;
    }

    private ushort GetAddressValue(
        [NotNull] HackParser.AddressContext context)
    {
        ushort address;

        if (context.NAME() != null)
        {
            var name = context.NAME().GetText();
            address = GetSymbolAddress(name);
        }
        else
        {
            // If there's no NAME there *must* be a UINT
            // otherwise the parser is broken.
            address = ushort.Parse(context.UINT().GetText());
        }

        return address;
    }

    private ushort GetSymbolAddress(string name)
    {
        ushort address; 

        // See if we can obtain a reference by looking
        // into the known symbols table.
        if (!this.symbols.TryGetValue(name, out address))
        {
            // Couldn't find anything in the symbols
            // table; just calculate a new address and
            // add the unknown symbol to the table.
            address = (ushort)this.symbols.Count;
            this.symbols.Add(name, address);
        }

        return address;
    }

    private void InitializeSymbols(IDictionary<string, ushort> labels)
    {
        this.symbols = DefaultSymbols.ToDictionary(x => x.Key, x => x.Value);
        foreach (var symbol in labels)
        {
            this.symbols.Add(symbol);
        }
    }
}