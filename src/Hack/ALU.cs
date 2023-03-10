namespace Hack;

public class ALU
{
    /// <summary>
    /// Gets or sets the left input for an ALU operation.
    /// </summary>
    public short X { get; set; }

    /// <summary>
    /// Gets or sets the right input for an ALU operation.
    /// </summary>
    public short Y { get; set; }

    /// <summary>
    /// Gets or sets the ALU operation as a byte where
    /// bits 0 to 5 indicate the computation to be performed.
    /// </summary>
    /// <remarks>
    /// This is equivalent to setting the F flags (i.e. ZX, 
    /// NX, etc.) individually but instead uses bits 0 to and
    /// including 5 to determine either the flags or the 
    /// byte value of the flags.
    /// </remarks>
    public byte Op
    {
        get
        {
            byte op = 0;
            op |= (byte)(this.NO ? (1 << 0) : 0);
            op |= (byte)(this.F ? (1 << 1) : 0);
            op |= (byte)(this.NY ? (1 << 2) : 0);
            op |= (byte)(this.ZY ? (1 << 3) : 0);
            op |= (byte)(this.NX ? (1 << 4) : 0);
            op |= (byte)(this.ZX ? (1 << 5) : 0);
            return op;
        }
        set
        {
            this.NO = (value & (1 << 0)) == (1 << 0);
            this.F = (value & (1 << 1)) == (1 << 1);
            this.NY = (value & (1 << 2)) == (1 << 2);
            this.ZY = (value & (1 << 3)) == (1 << 3);
            this.NX = (value & (1 << 4)) == (1 << 4);
            this.ZX = (value & (1 << 5)) == (1 << 5);
        }
    }

    /// <summary>
    /// Gets a flag indicating if the ALU output is zero.
    /// </summary>
    public bool ZR => this.Out == 0;

    /// <summary>
    /// Gets a flag indicating if the ALU output is negative.
    /// </summary>
    public bool NG => this.Out < 0;

    /// <summary>
    /// Gets or sets a flag indicating whether the left
    /// input should be zerod.
    /// </summary>
    public bool ZX { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the left
    /// input should be negated.
    /// </summary>
    public bool NX { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the right
    /// input should be zerod.
    /// </summary>
    public bool ZY { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the right
    /// input should be negated.
    /// </summary>
    public bool NY { get; set; }

    /// <summary>
    /// Gets or sets whether a arithmetic or logical operation is performed.
    /// </summary>
    public bool F { get; set; }

    /// <summary>
    /// Gets or sets whether the output should be fed through a NOT16.
    /// </summary>
    public bool NO { get; set; }

    /// <summary>
    /// Gets the output of this ALU depending on its inputs.
    /// </summary>
    public short Out
    {
        get
        {
            short @out = 0;

            var x = this.X;
            var y = this.Y;

            x = this.ZX ? (short)0 : x;
            x = this.NX ? (short)~x : x;

            y = this.ZY ? (short)0 : y;
            y = this.NY ? (short)~y : y;

            @out = this.F ? (short)(x + y) : (short)(x & y);
            @out = this.NO ? (short)~@out : @out;

            return @out;
        }
    }
}
