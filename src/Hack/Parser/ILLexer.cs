//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\IL.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public partial class ILLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, SEGMENT=14, UINT=15, COMMENT=16, 
		WS=17;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "SEGMENT", "UINT", "COMMENT", "WS"
	};


	public ILLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public ILLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'push'", "'constant'", "'static'", "'pop'", "'add'", "'sub'", "'neg'", 
		"'eq'", "'gt'", "'lt'", "'and'", "'or'", "'not'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, "SEGMENT", "UINT", "COMMENT", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "IL.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static ILLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,17,148,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,1,0,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,
		1,1,1,1,1,1,2,1,2,1,2,1,2,1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,
		1,5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,7,1,7,1,7,1,8,1,8,1,8,1,9,1,9,1,9,1,
		10,1,10,1,10,1,10,1,11,1,11,1,11,1,12,1,12,1,12,1,12,1,13,1,13,1,13,1,
		13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,
		13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,13,1,
		13,3,13,125,8,13,1,14,4,14,128,8,14,11,14,12,14,129,1,15,1,15,1,15,1,15,
		5,15,136,8,15,10,15,12,15,139,9,15,1,15,1,15,1,15,1,15,1,16,1,16,1,16,
		1,16,1,137,0,17,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,
		12,25,13,27,14,29,15,31,16,33,17,1,0,2,1,0,48,57,3,0,9,10,13,13,32,32,
		154,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,
		0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,
		0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,
		1,0,0,0,1,35,1,0,0,0,3,40,1,0,0,0,5,49,1,0,0,0,7,56,1,0,0,0,9,60,1,0,0,
		0,11,64,1,0,0,0,13,68,1,0,0,0,15,72,1,0,0,0,17,75,1,0,0,0,19,78,1,0,0,
		0,21,81,1,0,0,0,23,85,1,0,0,0,25,88,1,0,0,0,27,124,1,0,0,0,29,127,1,0,
		0,0,31,131,1,0,0,0,33,144,1,0,0,0,35,36,5,112,0,0,36,37,5,117,0,0,37,38,
		5,115,0,0,38,39,5,104,0,0,39,2,1,0,0,0,40,41,5,99,0,0,41,42,5,111,0,0,
		42,43,5,110,0,0,43,44,5,115,0,0,44,45,5,116,0,0,45,46,5,97,0,0,46,47,5,
		110,0,0,47,48,5,116,0,0,48,4,1,0,0,0,49,50,5,115,0,0,50,51,5,116,0,0,51,
		52,5,97,0,0,52,53,5,116,0,0,53,54,5,105,0,0,54,55,5,99,0,0,55,6,1,0,0,
		0,56,57,5,112,0,0,57,58,5,111,0,0,58,59,5,112,0,0,59,8,1,0,0,0,60,61,5,
		97,0,0,61,62,5,100,0,0,62,63,5,100,0,0,63,10,1,0,0,0,64,65,5,115,0,0,65,
		66,5,117,0,0,66,67,5,98,0,0,67,12,1,0,0,0,68,69,5,110,0,0,69,70,5,101,
		0,0,70,71,5,103,0,0,71,14,1,0,0,0,72,73,5,101,0,0,73,74,5,113,0,0,74,16,
		1,0,0,0,75,76,5,103,0,0,76,77,5,116,0,0,77,18,1,0,0,0,78,79,5,108,0,0,
		79,80,5,116,0,0,80,20,1,0,0,0,81,82,5,97,0,0,82,83,5,110,0,0,83,84,5,100,
		0,0,84,22,1,0,0,0,85,86,5,111,0,0,86,87,5,114,0,0,87,24,1,0,0,0,88,89,
		5,110,0,0,89,90,5,111,0,0,90,91,5,116,0,0,91,26,1,0,0,0,92,93,5,97,0,0,
		93,94,5,114,0,0,94,95,5,103,0,0,95,96,5,117,0,0,96,97,5,109,0,0,97,98,
		5,101,0,0,98,99,5,110,0,0,99,125,5,116,0,0,100,101,5,108,0,0,101,102,5,
		111,0,0,102,103,5,99,0,0,103,104,5,97,0,0,104,125,5,108,0,0,105,106,5,
		116,0,0,106,107,5,104,0,0,107,108,5,105,0,0,108,125,5,115,0,0,109,110,
		5,116,0,0,110,111,5,104,0,0,111,112,5,97,0,0,112,125,5,116,0,0,113,114,
		5,112,0,0,114,115,5,111,0,0,115,116,5,105,0,0,116,117,5,110,0,0,117,118,
		5,116,0,0,118,119,5,101,0,0,119,125,5,114,0,0,120,121,5,116,0,0,121,122,
		5,101,0,0,122,123,5,109,0,0,123,125,5,112,0,0,124,92,1,0,0,0,124,100,1,
		0,0,0,124,105,1,0,0,0,124,109,1,0,0,0,124,113,1,0,0,0,124,120,1,0,0,0,
		125,28,1,0,0,0,126,128,7,0,0,0,127,126,1,0,0,0,128,129,1,0,0,0,129,127,
		1,0,0,0,129,130,1,0,0,0,130,30,1,0,0,0,131,132,5,47,0,0,132,133,5,47,0,
		0,133,137,1,0,0,0,134,136,9,0,0,0,135,134,1,0,0,0,136,139,1,0,0,0,137,
		138,1,0,0,0,137,135,1,0,0,0,138,140,1,0,0,0,139,137,1,0,0,0,140,141,5,
		10,0,0,141,142,1,0,0,0,142,143,6,15,0,0,143,32,1,0,0,0,144,145,7,1,0,0,
		145,146,1,0,0,0,146,147,6,16,0,0,147,34,1,0,0,0,4,0,124,129,137,1,6,0,
		0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
