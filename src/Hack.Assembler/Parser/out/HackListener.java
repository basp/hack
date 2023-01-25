// Generated from java-escape by ANTLR 4.11.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link HackParser}.
 */
public interface HackListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link HackParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(HackParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(HackParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#a}.
	 * @param ctx the parse tree
	 */
	void enterA(HackParser.AContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#a}.
	 * @param ctx the parse tree
	 */
	void exitA(HackParser.AContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#c}.
	 * @param ctx the parse tree
	 */
	void enterC(HackParser.CContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#c}.
	 * @param ctx the parse tree
	 */
	void exitC(HackParser.CContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#instruction}.
	 * @param ctx the parse tree
	 */
	void enterInstruction(HackParser.InstructionContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#instruction}.
	 * @param ctx the parse tree
	 */
	void exitInstruction(HackParser.InstructionContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#command}.
	 * @param ctx the parse tree
	 */
	void enterCommand(HackParser.CommandContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#command}.
	 * @param ctx the parse tree
	 */
	void exitCommand(HackParser.CommandContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#label}.
	 * @param ctx the parse tree
	 */
	void enterLabel(HackParser.LabelContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#label}.
	 * @param ctx the parse tree
	 */
	void exitLabel(HackParser.LabelContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#comp}.
	 * @param ctx the parse tree
	 */
	void enterComp(HackParser.CompContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#comp}.
	 * @param ctx the parse tree
	 */
	void exitComp(HackParser.CompContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#dest}.
	 * @param ctx the parse tree
	 */
	void enterDest(HackParser.DestContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#dest}.
	 * @param ctx the parse tree
	 */
	void exitDest(HackParser.DestContext ctx);
	/**
	 * Enter a parse tree produced by {@link HackParser#jump}.
	 * @param ctx the parse tree
	 */
	void enterJump(HackParser.JumpContext ctx);
	/**
	 * Exit a parse tree produced by {@link HackParser#jump}.
	 * @param ctx the parse tree
	 */
	void exitJump(HackParser.JumpContext ctx);
}