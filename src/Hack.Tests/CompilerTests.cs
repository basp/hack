namespace Hack.Tests;

using Hack;
using Xunit;

public class CompilerTests
{
    [Fact]
    public void TestCompileCInstruction()
    {
        var tests = new[]
        {
            new
            {
                Instruction = Compiler.Compile(
                    Computation.Zero,
                    Destination.None,
                    Jump.None),
                Expected = 0b111_0_101010_000_000,
            },
            new
            {
                Instruction = Compiler.Compile(
                    Computation.One,
                    Destination.A,
                    Jump.GreaterThan),
                Expected = 0b111_0_111111_100_001,
            },
            new
            {
                Instruction = Compiler.Compile(
                    Computation.AddressPlusOne,
                    Destination.MD,
                    Jump.LessThanOrEqual),
                Expected = 0b111_0_110010_011_110,
            },
            new
            {
                Instruction = Compiler.Compile(
                    Computation.MinusOne,
                    Destination.AMD,
                    Jump.NotEqual),
                Expected = 0b111_0_111010_111_101,
            }
        };

        foreach (var t in tests)
        {
            Assert.Equal(t.Instruction, (short)t.Expected);
        }
    }
}
