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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IILListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class ILBaseListener : IILListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ILParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunction([NotNull] ILParser.FunctionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="ILParser.function"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunction([NotNull] ILParser.FunctionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>pushConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPushConstant([NotNull] ILParser.PushConstantContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>pushConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPushConstant([NotNull] ILParser.PushConstantContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>pushStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPushStatic([NotNull] ILParser.PushStaticContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>pushStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPushStatic([NotNull] ILParser.PushStaticContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>pushDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPushDynamic([NotNull] ILParser.PushDynamicContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>pushDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPushDynamic([NotNull] ILParser.PushDynamicContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>popConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPopConstant([NotNull] ILParser.PopConstantContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>popConstant</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPopConstant([NotNull] ILParser.PopConstantContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>popStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPopStatic([NotNull] ILParser.PopStaticContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>popStatic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPopStatic([NotNull] ILParser.PopStaticContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>popDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPopDynamic([NotNull] ILParser.PopDynamicContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>popDynamic</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPopDynamic([NotNull] ILParser.PopDynamicContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>add</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAdd([NotNull] ILParser.AddContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>add</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAdd([NotNull] ILParser.AddContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>sub</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSub([NotNull] ILParser.SubContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>sub</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSub([NotNull] ILParser.SubContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>neg</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNeg([NotNull] ILParser.NegContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>neg</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNeg([NotNull] ILParser.NegContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>eq</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEq([NotNull] ILParser.EqContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>eq</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEq([NotNull] ILParser.EqContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>gt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGt([NotNull] ILParser.GtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>gt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGt([NotNull] ILParser.GtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>lt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLt([NotNull] ILParser.LtContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>lt</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLt([NotNull] ILParser.LtContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>and</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAnd([NotNull] ILParser.AndContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>and</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAnd([NotNull] ILParser.AndContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>or</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOr([NotNull] ILParser.OrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>or</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOr([NotNull] ILParser.OrContext context) { }
	/// <summary>
	/// Enter a parse tree produced by the <c>not</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNot([NotNull] ILParser.NotContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>not</c>
	/// labeled alternative in <see cref="ILParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNot([NotNull] ILParser.NotContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}