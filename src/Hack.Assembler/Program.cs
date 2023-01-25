using Antlr4.Runtime;
using Hack.Assembler;
using Hack.Hardware;

var source = @"
    @i          // 0
    M=1         // 1
    @sum        // 2
    M=0         // 3
(LOOP)          // -> 4
    @i          // 4
    D=M         // 5
    @100        // 6
(END)           // -> 7
    @END        // 7
    0;JMP       // 8
";

var input = new AntlrInputStream(source);
var lexer = new HackLexer(input);
var tokens = new CommonTokenStream(lexer);
var parser = new HackParser(tokens);

HackParser.ProgramContext context;

var firstPass = new FirstPassParseListener();
parser.AddParseListener(firstPass);
context = parser.program();
parser.RemoveParseListeners();
tokens.Reset();

var secondPass = new SecondPassParseListener(firstPass.Labels);
parser.AddParseListener(secondPass);
context = parser.program();
parser.RemoveParseListeners();
tokens.Reset();

var sim = new Tiny();
var code = secondPass.Instructions.ToArray();
sim.Run(code);
Console.WriteLine(sim.Out);