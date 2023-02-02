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

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ILParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.CLSCompliant(false)]
public interface IILListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ILParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] ILParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ILParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] ILParser.FunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>pushConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPushConstant([NotNull] ILParser.PushConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>pushConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPushConstant([NotNull] ILParser.PushConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>pushStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPushStatic([NotNull] ILParser.PushStaticContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>pushStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPushStatic([NotNull] ILParser.PushStaticContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>pushDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPushDynamic([NotNull] ILParser.PushDynamicContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>pushDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPushDynamic([NotNull] ILParser.PushDynamicContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>popConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPopConstant([NotNull] ILParser.PopConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>popConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPopConstant([NotNull] ILParser.PopConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>popStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPopStatic([NotNull] ILParser.PopStaticContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>popStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPopStatic([NotNull] ILParser.PopStaticContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>popDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPopDynamic([NotNull] ILParser.PopDynamicContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>popDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPopDynamic([NotNull] ILParser.PopDynamicContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>add</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdd([NotNull] ILParser.AddContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>add</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdd([NotNull] ILParser.AddContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>sub</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSub([NotNull] ILParser.SubContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>sub</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSub([NotNull] ILParser.SubContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>neg</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNeg([NotNull] ILParser.NegContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>neg</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNeg([NotNull] ILParser.NegContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>eq</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEq([NotNull] ILParser.EqContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>eq</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEq([NotNull] ILParser.EqContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>gt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGt([NotNull] ILParser.GtContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>gt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGt([NotNull] ILParser.GtContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>lt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLt([NotNull] ILParser.LtContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>lt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLt([NotNull] ILParser.LtContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>and</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] ILParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>and</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] ILParser.AndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>or</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] ILParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>or</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] ILParser.OrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>not</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNot([NotNull] ILParser.NotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>not</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNot([NotNull] ILParser.NotContext context);
}