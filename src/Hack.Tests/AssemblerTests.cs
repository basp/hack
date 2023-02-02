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
}
