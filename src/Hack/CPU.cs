namespace Hack;

public class ROM32K
{
    private readonly short[] data = new short[1 << 15];

    public short Address { get; set; }

    public short Out => this.data[this.Address];
}

public class RAM32K
{
    private readonly short[] data = new short[1 << 15];

    public short Address { get; set; }

    public short In { get; set; }

    public short Out => this.data[this.Address];

    public void Load()
    {
        this.data[this.Address] = this.In;
    }
}

public class CPU
{
    public CPU(ROM32K program, RAM32K data)
    {
        this.Program = program;
        this.Data = data;
    }

    public ROM32K Program { get; }

    public RAM32K Data { get; }

    public ALU ALU { get; } = new ALU();

    public short D { get; private set; }

    public short A { get; private set; }

    public short InM { get; set; }

    public short Instruction { get; set; }

    public short OutM { get; private set; }

    public bool WriteM { get; private set; }

    public short AddressM { get; private set; }

    public short PC { get; private set; }

    public void Reset()
    {
        this.PC = 0;                
    }
}