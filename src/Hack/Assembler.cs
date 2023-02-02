namespace Hack;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

public class Assembler
{
    public static short[] Assemble(string source)
    {
        var input = new AntlrInputStream(source);
        var lexer = new HackLexer(input);
        var tokens = new CommonTokenStream(lexer);
        var parser = new HackParser(tokens);

        var firstPass = new FirstPass();
        parser.AddParseListener(firstPass);
        parser.program();
        parser.RemoveParseListeners();
        tokens.Reset();

        var secondPass = new SecondPass(firstPass.Labels);
        parser.AddParseListener(secondPass);
        parser.program();
        parser.RemoveParseListeners();
        tokens.Reset();

        return secondPass.Instructions;
    }

    private class FirstPass : HackBaseListener
    {
        public IDictionary<string, short> Labels { get; } =
            new Dictionary<string, short>();

        private short pc;

        public override void EnterProgram(
            [NotNull] HackParser.ProgramContext context)
        {
            this.Labels.Clear();
        }

        public override void ExitLabel(
            [NotNull] HackParser.LabelContext context)
        {
            this.Labels.Add(context.NAME().GetText(), this.pc);
        }

        public override void ExitInstruction(
            [NotNull] HackParser.InstructionContext context)
        {
            this.pc += 1;
        }
    }

    private class SecondPass : HackBaseListener
    {
        private static IDictionary<string, Destination> destinations =
            new Dictionary<string, Destination>
            {
                [""] = Destination.None,
                ["M"] = Destination.M,
                ["D"] = Destination.D,
                ["MD"] = Destination.MD,
                ["A"] = Destination.A,
                ["AM"] = Destination.AM,
                ["AD"] = Destination.AD,
                ["AMD"] = Destination.AMD,
            };

        private static IDictionary<string, Computation> computations =
            new Dictionary<string, Computation>
            {
                ["0"] = Computation.Zero,
                ["1"] = Computation.One,
                ["-1"] = Computation.MinusOne,
                ["D"] = Computation.Data,
                ["A"] = Computation.Address,
                ["!D"] = Computation.NotData,
                ["!A"] = Computation.NotAddress,
                ["-D"] = Computation.MinusData,
                ["-A"] = Computation.MinusAddress,
                ["D+1"] = Computation.DataPlusOne,
                ["A+1"] = Computation.AddressPlusOne,
                ["D-1"] = Computation.DataMinusOne,
                ["A-1"] = Computation.AddressMinusOne,
                ["D+A"] = Computation.DataPlusAddress,
                ["D-A"] = Computation.DataMinusAddress,
                ["A-D"] = Computation.AddressMinusData,
                ["D&A"] = Computation.DataAndAddress,
                ["D|A"] = Computation.DataOrAddress,
                ["M"] = Computation.Memory,
                ["!M"] = Computation.NotMemory,
                ["-M"] = Computation.MinusMemory,
                ["M+1"] = Computation.MemoryPlusOne,
                ["M-1"] = Computation.MemoryMinusOne,
                ["D+M"] = Computation.DataPlusMemory,
                ["D-M"] = Computation.DataMinusMemory,
                ["M-D"] = Computation.MemoryMinusData,
                ["D&M"] = Computation.DataAndMemory,
                ["D|M"] = Computation.DataOrMemory,
            };

        private static IDictionary<string, Jump> jumps =
            new Dictionary<string, Jump>
            {
                [""] = Jump.Never,
                ["JGT"] = Jump.GT,
                ["JEQ"] = Jump.EQ,
                ["JGE"] = Jump.GE,
                ["JLT"] = Jump.LT,
                ["JNE"] = Jump.NE,
                ["JLE"] = Jump.LE,
                ["JMP"] = Jump.Always,
            };

        private readonly IDictionary<string, short> builtin =
            new Dictionary<string, short>
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

        private readonly IDictionary<string, short> variables =
            new Dictionary<string, short>();

        private readonly IDictionary<string, short> labels;

        private IList<short> instructions = new List<short>();

        public SecondPass(IDictionary<string, short> labels)
        {
            this.labels = labels;
        }

        public short[] Instructions => this.instructions.ToArray();

        public override void ExitCompute(
            [NotNull] HackParser.ComputeContext context)
        {
            var comp = context.comp().GetText();
            var dest = context.dest() != null
                ? context.dest().GetText()
                : string.Empty;
            var jump = context.jump() != null
                ? context.jump().GetText()
                : string.Empty;
            var ins = Compiler.Compile(
                computations[comp],
                destinations[dest],
                jumps[jump]);
            this.instructions.Add(ins);
        }

        private short GetSymbolValue(string symbol)
        {
            short value;

            if (this.labels.TryGetValue(symbol, out value))
            {
                return value;
            }

            if (this.builtin.TryGetValue(symbol, out value))
            {
                return value;
            }

            if (this.variables.TryGetValue(symbol, out value))
            {
                return value;
            }

            value = (short)(this.builtin["R15"] + this.variables.Count + 1);
            this.variables.Add(symbol, value);
            return value;
        }

        public override void ExitAddress(
            [NotNull] HackParser.AddressContext context)
        {
            // Strip the '@' symbol from the token text
            var text = context.GetText().Substring(1);

            if (!short.TryParse(text, out var address))
            {
                address = GetSymbolValue(text);
            }

            var ins = Compiler.Compile(address);
            this.instructions.Add(ins);
        }
    }
}