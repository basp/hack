namespace Hack.Tests;

using Xunit;

public class AssemblerTests
{
    [Fact]
    public void TestAssembler1()
    {
        var source = @"
                @i
                M=1
                @sum
                M=0
            (LOOP)
                @i
                D=M
            (END)
                @END
                0;JMP";

        var bin = Assembler.Assemble(source);

        Assert.Equal(8, bin.Length);

        var tests = new[]
        {
            Compiler.Compile(16),
            Compiler.Compile(Computation.One, Destination.M),
            Compiler.Compile(17),
            Compiler.Compile(Computation.Zero, Destination.M),
            Compiler.Compile(16),
            Compiler.Compile(Computation.Memory, Destination.D),
            Compiler.Compile(6),
            Compiler.Compile(Computation.Zero, jump: Jump.Always),
        };

        for (var i = 0; i < tests.Length; i++)
        {
            Assert.Equal(tests[i], bin[i]);
        }
    }

    [Fact]
    public void TestAssembler2()
    {
        var source = @"
            // Adds 1 + ... + 100
                @i
                M=1
                @sum
                M=0
            (LOOP)
                @i
                D=M
                @100
                D=D-A
                @END
                D;JGT
                @i
                D=M
                @sum
                M=D+M
                @i
                M=M+1
                @LOOP
                0;JMP
            (END)
                @END
                0;JMP
            ";

        var bin = Assembler.Assemble(source);

        var expected = new ushort[]
        {
            0b0000_0000_0001_0000,
            0b1110_1111_1100_1000,
            0b0000_0000_0001_0001,
            0b1110_1010_1000_1000,
            
            0b0000_0000_0001_0000,
            0b1111_1100_0001_0000,
            0b0000_0000_0110_0100,
            0b1110_0100_1101_0000,

            0b0000_0000_0001_0010,
            0b1110_0011_0000_0001,
            0b0000_0000_0001_0000,
            0b1111_1100_0001_0000,

            0b0000_0000_0001_0001,
            0b1111_0000_1000_1000,
            0b0000_0000_0001_0000,
            0b1111_1101_1100_1000,

            0b0000_0000_0000_0100,
            0b1110_1010_1000_0111,
            0b0000_0000_0001_0010,
            0b1110_1010_1000_0111,
        };

        for(var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], (ushort)bin[i]);
        }
    }
}
