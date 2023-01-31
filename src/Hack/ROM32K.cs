namespace Hack;

public class ROM32K
{
    public ROM32K(params short[] data)
    {
        Array.Copy(data, this.data, data.Length);
    }

    private readonly short[] data = new short[1 << 15];

    public short Address { get; set; }

    public short Out => this.data[this.Address];
}
