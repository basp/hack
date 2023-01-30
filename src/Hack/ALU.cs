namespace Hack;

public class ALU
{
    public short X { get; set; }

    public short Y { get; set; }

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

    public bool ZR => this.Out == 0;

    public bool NG => this.Out < 0;

    public bool ZX { get; set; }

    public bool NX { get; set; }

    public bool ZY { get; set; }

    public bool NY { get; set; }

    public bool F { get; set; }

    public bool NO { get; set; }

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
