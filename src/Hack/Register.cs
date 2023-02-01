namespace Hack;

public class Register
{
    private short next;

    public bool Load { get; set; }

    public short In { get; set; }

    public short Out { get; private set; }

    public void Tick()
    {
        this.next = this.Load
            ? this.In
            : this.Out;
    }

    public void Tock()
    {
        this.Out = this.next;
    }
}