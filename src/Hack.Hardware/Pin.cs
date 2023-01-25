namespace Hack.Hardware;

public class Pin
{
    private Chip owner;
    private string name;
    private bool value;
    private List<Pin> connections;
    private bool activate;

    public Pin(Chip owner, string name, bool activate = false)
    {
        this.owner = owner;
        this.name = name;
        this.connections = new List<Pin>();
        this.activate = activate;
    }

    public bool Value => this.value;

    public Pin Connect(params Pin[] pins)
    {
        this.connections.AddRange(pins);
        return this;
    }

    public Pin Set(bool value)
    {
        if (this.value == value)
        {
            return this;
        }

        this.value = value;

        if (this.activate)
        {
            this.owner.Evaluate();
        }

        foreach (var conn in this.connections)
        {
            conn.Set(value);
        }

        return this;
    }
}
