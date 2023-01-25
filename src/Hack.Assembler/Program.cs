using Antlr4.Runtime;
using Hack.Assembler;

var code = @"
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

var input = new AntlrInputStream(code);
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