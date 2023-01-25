// Generated from d:\basp\hack\src\Hack.Hardware\HDL.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class HDLParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, ID=14, INT=15, LETTER=16, DIGIT=17, 
		WS=18;
	public static final int
		RULE_chip = 0, RULE_body = 1, RULE_part = 2, RULE_partName = 3, RULE_chipName = 4, 
		RULE_pinName = 5, RULE_pinWidth = 6, RULE_connections = 7, RULE_conn = 8, 
		RULE_inputs = 9, RULE_outputs = 10, RULE_pin = 11;
	private static String[] makeRuleNames() {
		return new String[] {
			"chip", "body", "part", "partName", "chipName", "pinName", "pinWidth", 
			"connections", "conn", "inputs", "outputs", "pin"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'CHIP'", "'{'", "'IN'", "';'", "'OUT'", "'}'", "'PARTS:'", "'('", 
			"')'", "','", "'='", "'['", "']'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, "ID", "INT", "LETTER", "DIGIT", "WS"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "HDL.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public HDLParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ChipContext extends ParserRuleContext {
		public ChipNameContext chipName() {
			return getRuleContext(ChipNameContext.class,0);
		}
		public InputsContext inputs() {
			return getRuleContext(InputsContext.class,0);
		}
		public OutputsContext outputs() {
			return getRuleContext(OutputsContext.class,0);
		}
		public BodyContext body() {
			return getRuleContext(BodyContext.class,0);
		}
		public ChipContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_chip; }
	}

	public final ChipContext chip() throws RecognitionException {
		ChipContext _localctx = new ChipContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_chip);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(24);
			match(T__0);
			setState(25);
			chipName();
			setState(26);
			match(T__1);
			setState(27);
			match(T__2);
			setState(28);
			inputs();
			setState(29);
			match(T__3);
			setState(30);
			match(T__4);
			setState(31);
			outputs();
			setState(32);
			match(T__3);
			setState(33);
			body();
			setState(34);
			match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BodyContext extends ParserRuleContext {
		public List<PartContext> part() {
			return getRuleContexts(PartContext.class);
		}
		public PartContext part(int i) {
			return getRuleContext(PartContext.class,i);
		}
		public BodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_body; }
	}

	public final BodyContext body() throws RecognitionException {
		BodyContext _localctx = new BodyContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_body);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(36);
			match(T__6);
			setState(38); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(37);
				part();
				}
				}
				setState(40); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==ID );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PartContext extends ParserRuleContext {
		public PartNameContext partName() {
			return getRuleContext(PartNameContext.class,0);
		}
		public ConnectionsContext connections() {
			return getRuleContext(ConnectionsContext.class,0);
		}
		public PartContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_part; }
	}

	public final PartContext part() throws RecognitionException {
		PartContext _localctx = new PartContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_part);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(42);
			partName();
			setState(43);
			match(T__7);
			setState(44);
			connections();
			setState(45);
			match(T__8);
			setState(46);
			match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PartNameContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(HDLParser.ID, 0); }
		public PartNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_partName; }
	}

	public final PartNameContext partName() throws RecognitionException {
		PartNameContext _localctx = new PartNameContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_partName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(48);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ChipNameContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(HDLParser.ID, 0); }
		public ChipNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_chipName; }
	}

	public final ChipNameContext chipName() throws RecognitionException {
		ChipNameContext _localctx = new ChipNameContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_chipName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(50);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PinNameContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(HDLParser.ID, 0); }
		public PinNameContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pinName; }
	}

	public final PinNameContext pinName() throws RecognitionException {
		PinNameContext _localctx = new PinNameContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_pinName);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(52);
			match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PinWidthContext extends ParserRuleContext {
		public TerminalNode INT() { return getToken(HDLParser.INT, 0); }
		public PinWidthContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pinWidth; }
	}

	public final PinWidthContext pinWidth() throws RecognitionException {
		PinWidthContext _localctx = new PinWidthContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_pinWidth);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(54);
			match(INT);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConnectionsContext extends ParserRuleContext {
		public List<ConnContext> conn() {
			return getRuleContexts(ConnContext.class);
		}
		public ConnContext conn(int i) {
			return getRuleContext(ConnContext.class,i);
		}
		public ConnectionsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_connections; }
	}

	public final ConnectionsContext connections() throws RecognitionException {
		ConnectionsContext _localctx = new ConnectionsContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_connections);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(56);
			conn();
			setState(61);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__9) {
				{
				{
				setState(57);
				match(T__9);
				setState(58);
				conn();
				}
				}
				setState(63);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConnContext extends ParserRuleContext {
		public List<PinContext> pin() {
			return getRuleContexts(PinContext.class);
		}
		public PinContext pin(int i) {
			return getRuleContext(PinContext.class,i);
		}
		public ConnContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conn; }
	}

	public final ConnContext conn() throws RecognitionException {
		ConnContext _localctx = new ConnContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_conn);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(64);
			pin();
			setState(65);
			match(T__10);
			setState(66);
			pin();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class InputsContext extends ParserRuleContext {
		public List<PinContext> pin() {
			return getRuleContexts(PinContext.class);
		}
		public PinContext pin(int i) {
			return getRuleContext(PinContext.class,i);
		}
		public InputsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inputs; }
	}

	public final InputsContext inputs() throws RecognitionException {
		InputsContext _localctx = new InputsContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_inputs);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(68);
			pin();
			setState(73);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__9) {
				{
				{
				setState(69);
				match(T__9);
				setState(70);
				pin();
				}
				}
				setState(75);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OutputsContext extends ParserRuleContext {
		public List<PinContext> pin() {
			return getRuleContexts(PinContext.class);
		}
		public PinContext pin(int i) {
			return getRuleContext(PinContext.class,i);
		}
		public OutputsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_outputs; }
	}

	public final OutputsContext outputs() throws RecognitionException {
		OutputsContext _localctx = new OutputsContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_outputs);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(76);
			pin();
			setState(81);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__9) {
				{
				{
				setState(77);
				match(T__9);
				setState(78);
				pin();
				}
				}
				setState(83);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PinContext extends ParserRuleContext {
		public PinNameContext pinName() {
			return getRuleContext(PinNameContext.class,0);
		}
		public PinWidthContext pinWidth() {
			return getRuleContext(PinWidthContext.class,0);
		}
		public PinContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pin; }
	}

	public final PinContext pin() throws RecognitionException {
		PinContext _localctx = new PinContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_pin);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(84);
			pinName();
			setState(89);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__11) {
				{
				setState(85);
				match(T__11);
				setState(86);
				pinWidth();
				setState(87);
				match(T__12);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\24^\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t\13\4"+
		"\f\t\f\4\r\t\r\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\2\3\3\3\3"+
		"\6\3)\n\3\r\3\16\3*\3\4\3\4\3\4\3\4\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3"+
		"\b\3\b\3\t\3\t\3\t\7\t>\n\t\f\t\16\tA\13\t\3\n\3\n\3\n\3\n\3\13\3\13\3"+
		"\13\7\13J\n\13\f\13\16\13M\13\13\3\f\3\f\3\f\7\fR\n\f\f\f\16\fU\13\f\3"+
		"\r\3\r\3\r\3\r\3\r\5\r\\\n\r\3\r\2\2\16\2\4\6\b\n\f\16\20\22\24\26\30"+
		"\2\2\2V\2\32\3\2\2\2\4&\3\2\2\2\6,\3\2\2\2\b\62\3\2\2\2\n\64\3\2\2\2\f"+
		"\66\3\2\2\2\168\3\2\2\2\20:\3\2\2\2\22B\3\2\2\2\24F\3\2\2\2\26N\3\2\2"+
		"\2\30V\3\2\2\2\32\33\7\3\2\2\33\34\5\n\6\2\34\35\7\4\2\2\35\36\7\5\2\2"+
		"\36\37\5\24\13\2\37 \7\6\2\2 !\7\7\2\2!\"\5\26\f\2\"#\7\6\2\2#$\5\4\3"+
		"\2$%\7\b\2\2%\3\3\2\2\2&(\7\t\2\2\')\5\6\4\2(\'\3\2\2\2)*\3\2\2\2*(\3"+
		"\2\2\2*+\3\2\2\2+\5\3\2\2\2,-\5\b\5\2-.\7\n\2\2./\5\20\t\2/\60\7\13\2"+
		"\2\60\61\7\6\2\2\61\7\3\2\2\2\62\63\7\20\2\2\63\t\3\2\2\2\64\65\7\20\2"+
		"\2\65\13\3\2\2\2\66\67\7\20\2\2\67\r\3\2\2\289\7\21\2\29\17\3\2\2\2:?"+
		"\5\22\n\2;<\7\f\2\2<>\5\22\n\2=;\3\2\2\2>A\3\2\2\2?=\3\2\2\2?@\3\2\2\2"+
		"@\21\3\2\2\2A?\3\2\2\2BC\5\30\r\2CD\7\r\2\2DE\5\30\r\2E\23\3\2\2\2FK\5"+
		"\30\r\2GH\7\f\2\2HJ\5\30\r\2IG\3\2\2\2JM\3\2\2\2KI\3\2\2\2KL\3\2\2\2L"+
		"\25\3\2\2\2MK\3\2\2\2NS\5\30\r\2OP\7\f\2\2PR\5\30\r\2QO\3\2\2\2RU\3\2"+
		"\2\2SQ\3\2\2\2ST\3\2\2\2T\27\3\2\2\2US\3\2\2\2V[\5\f\7\2WX\7\16\2\2XY"+
		"\5\16\b\2YZ\7\17\2\2Z\\\3\2\2\2[W\3\2\2\2[\\\3\2\2\2\\\31\3\2\2\2\7*?"+
		"KS[";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}