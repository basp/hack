//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\Hack.g4 by ANTLR 4.11.1

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
/// This class provides an empty implementation of <see cref="IHackListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.Diagnostics.DebuggerNonUserCode]
public partial class HackBaseListener : IHackListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProgram([NotNull] HackParser.ProgramContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.program"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProgram([NotNull] HackParser.ProgramContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.compute"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompute([NotNull] HackParser.ComputeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.compute"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompute([NotNull] HackParser.ComputeContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.address"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAddress([NotNull] HackParser.AddressContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.address"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAddress([NotNull] HackParser.AddressContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.instruction"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInstruction([NotNull] HackParser.InstructionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.instruction"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInstruction([NotNull] HackParser.InstructionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCommand([NotNull] HackParser.CommandContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.command"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCommand([NotNull] HackParser.CommandContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.label"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLabel([NotNull] HackParser.LabelContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.label"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLabel([NotNull] HackParser.LabelContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.comp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterComp([NotNull] HackParser.CompContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.comp"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitComp([NotNull] HackParser.CompContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.dest"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDest([NotNull] HackParser.DestContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.dest"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDest([NotNull] HackParser.DestContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="HackParser.jump"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterJump([NotNull] HackParser.JumpContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="HackParser.jump"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitJump([NotNull] HackParser.JumpContext context) { }

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
