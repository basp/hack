namespace Hack;

public static class Code
{
    public static IDictionary<string, Destination> Destinations =
        new Dictionary<string, Destination>
        {
            [""] = Destination.None,
            ["M"] = Destination.M,
            ["D"] = Destination.D,
            ["MD"] = Destination.MD,
            ["A"] = Destination.A,
            ["AM"] = Destination.AM,
            ["AD"] = Destination.AD,
            ["AMD"] = Destination.AMD,
        };

    public static IDictionary<string, Computation> Computations =
        new Dictionary<string, Computation>
        {
            ["0"] = Computation.Zero,
            ["1"] = Computation.One,
            ["-1"] = Computation.MinusOne,
            ["D"] = Computation.Data,
            ["A"] = Computation.Address,
            ["!D"] = Computation.NotData,
            ["!A"] = Computation.NotAddress,
            ["-D"] = Computation.MinusData,
            ["-A"] = Computation.MinusAddress,
            ["D+1"] = Computation.DataPlusOne,
            ["A+1"] = Computation.AddressPlusOne,
            ["D-1"] = Computation.DataMinusOne,
            ["A-1"] = Computation.AddressMinusOne,
            ["D+A"] = Computation.DataPlusAddress,
            ["D-A"] = Computation.DataMinusAddress,
            ["A-D"] = Computation.AddressMinusData,
            ["D&A"] = Computation.DataAndAddress,
            ["D|A"] = Computation.DataOrAddress,
            ["M"] = Computation.Memory,
            ["!M"] = Computation.NotMemory,
            ["-M"] = Computation.MinusMemory,
            ["M+1"] = Computation.MemoryPlusOne,
            ["M-1"] = Computation.MemoryMinusOne,
            ["D+M"] = Computation.DataPlusMemory,
            ["D-M"] = Computation.DataMinusMemory,
            ["M-D"] = Computation.MemoryMinusData,
            ["D&M"] = Computation.DataAndMemory,
            ["D|M"] = Computation.DataOrMemory,
        };

    public static IDictionary<string, Jump> Jumps =
        new Dictionary<string, Jump>
        {
            [""] = Jump.Never,
            ["JGT"] = Jump.GT,
            ["JEQ"] = Jump.EQ,
            ["JGE"] = Jump.GE,
            ["JLT"] = Jump.LT,
            ["JNE"] = Jump.NE,
            ["JLE"] = Jump.LE,
            ["JMP"] = Jump.Always,
        };
}