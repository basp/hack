namespace Hack;

public readonly struct Instruction
{
    private const ushort computationMask = 0b1110_0000_0000_0000;
    
    private const ushort alphaMask = 0b1111_0000_0000_0000;

    private readonly short value;

    public Instruction(short value)
    {
        this.value = value;
    }

    public bool IsComputation =>
        (this.value & computationMask) == computationMask;

    public bool IsAlpha =>
        (this.value & alphaMask) == alphaMask;

    public short Address =>
        (short)(this.value & 0b0111_1111_1111_1111);

    public short Comp =>
        (short)((this.value & 0b0001_111111_000_000) >> 6);

    public short Dest =>
        (short)((this.value & 0b0000_000000_111_000) >> 3);

    public short Jump =>
        (short)((this.value & 0b0000_000000_000_111) >> 0);

    public static implicit operator Instruction(short value) =>
        new Instruction(value);    
}
