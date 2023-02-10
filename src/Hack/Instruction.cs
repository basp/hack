namespace Hack;

public readonly struct Instruction
{
    // If the three high bits are 1 then we have a C-instruction (computation) 
    // otherwise, it will be an A-instruction (memory addressing).
    private const ushort computationMask = 0b1110_0000_0000_0000;

    // Some C-instructions involve memory locations instead of the A and D
    // registers. These are signalled as alpha instructions. The four high
    // bits will be one.
    private const ushort alphaMask = 0b1111_0000_0000_0000;

    private readonly short value;

    public Instruction(short value)
    {
        this.value = value;
    }

    /// <summary>
    /// Gets a value indicating whether this is a C-instruction.
    /// </summary>
    public bool IsComputation =>
        (this.value & computationMask) == computationMask;

    /// <summary>
    /// Gets a value indicating whether this is an alpha C-instruction.
    /// </summary>
    /// <remarks>
    /// C-instructions that have their alpha flag set instruct the processor
    /// to use direct memory access for one of the operands instead. If the
    /// alpha flag is not set then the processor will use only the A and D
    /// registers.
    /// </remarks>
    public bool IsAlpha =>
        (this.value & alphaMask) == alphaMask;

    /// <summary>
    /// Gets the memory address of an A-instruction.
    /// </summary>
    /// <remarks>
    /// Although this property will always return a value it should only
    /// be interpreted as an address when dealing with an A-instruction (i.e. 
    /// when `IsComputation` returns `false`).
    /// </remarks>
    public short Address =>
        (short)(this.value & 0b0111_1111_1111_1111);

    /*
     * The bitops properties below involve some magic numbers but they make
     * sense if you understand the Hack instruction format. In essence they
     * will first extract a particular section of the bits by using a mask
     * and then that section is immediately right shifted to normalize the 
     * value into its original value.
     */

    /// <summary>
    /// Gets the computation value from a C-instruction.
    /// </summary>
    public short Comp =>
        (short)((this.value & 0b0001_111111_000_000) >> 6);

    /// <summary>
    /// Gets the destination value from a C-instruction.
    /// </summary>
    public short Dest =>
        (short)((this.value & 0b0000_000000_111_000) >> 3);

    /// <summary>
    /// Gets the jump value from a C-instruction.
    /// </summary>
    public short Jump =>
        (short)((this.value & 0b0000_000000_000_111) >> 0);

    /// <summary>
    /// Implicitly converts a <c>short</c> into an <c>Instruction</c>.
    /// </summary>
    /// <remarks>
    /// Since an instruction *is* a short and this class is basically just
    /// some masking and other bitops around the short it makes sense to
    /// be able to convert them easily without worrying too much. They are
    /// both value types so it's relatively harmless.
    /// </remarks>
    public static implicit operator Instruction(short value) =>
        new Instruction(value);
}
