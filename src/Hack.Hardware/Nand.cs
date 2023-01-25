namespace Hack.Hardware;

public class Nand : Chip
{
    public Nand()
    {
        this.Inputs["a"] = new []
        {
            new Pin(this, "a", activate: true),
        };

        this.Inputs["b"] = new []
        {
            new Pin(this, "b", activate: true),
        };

        this.Outputs["out"] = new []
        {
            new Pin(this, "out"),
        };

        this.Name = nameof(Nand);
    }
    
    public override void Evaluate()
    {
        var a = this.Inputs["a"][0];
        var b = this.Inputs["b"][0];
        var result = !(a.Value && b.Value);
        this.Outputs["out"][0].Set(result);
    }
}