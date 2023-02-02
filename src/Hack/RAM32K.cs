namespace Hack;

public class RAM32K
{   
    private readonly short[] data = new short[1 << 15];

    public RAM32K(params short[] data)
    {
        Array.Copy(data, this.data, data.Length);
    }

    public short this[int index]
    {
        get => this.data[index];
        set => this.data[index] = value;
    }

    public short Address { get; set; }

    public short In { get; set; }

    public short Out => this.data[this.Address];

    public bool Load { get; set; }

    public void Cycle()
    {
        this.data[this.Address] = this.Load
            ? this.In
            : this.data[this.Address];
    }
}
