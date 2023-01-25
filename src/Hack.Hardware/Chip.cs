namespace Hack.Hardware;

using System.Collections.Generic;

public class Chip
{
    private string name;

	public IDictionary<string, Pin[]> Inputs { get; } =
		new Dictionary<string, Pin[]>();

	public IDictionary<string, Pin[]> Outputs { get; } =
		new Dictionary<string, Pin[]>();

    public IDictionary<string, Pin[]> Internal { get; } =
        new Dictionary<string, Pin[]>();

    public string Name { get; set; }

    public virtual void Evaluate()
    {        
    }
}