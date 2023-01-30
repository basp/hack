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
                Instruction = Compiler.CompileC(
                    Computation.Zero,
                    Destination.None,
                    Jump.None),
                Expected = 0b111_0_101010_000_000,
            },
            new
            {
                Instruction = Compiler.CompileC(
                    Computation.One,
                    Destination.A,
                    Jump.GreaterThan),
                Expected = 0b111_0_111111_100_001,
            },
            new
            {
                Instruction = Compiler.CompileC(
                    Computation.AddressPlusOne,
                    Destination.MD,
                    Jump.LessThanOrEqual),
                Expected = 0b111_0_110010_011_110,
            },
            new
            {
                Instruction = Compiler.CompileC(
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

public class SimulatorTests
{
    [Fact]
    public void TestCPU()
    {

    }

    [Fact]
    public void TestALU()
    {
        const short x = 0b0000_0000_0000_0010;
        const short y = 0b0000_0000_0000_0011;

        var alu = new ALU();

        var tests = new (byte op, int expected)[]
        {
            (0b101010, 0),
            (0b111111, 1),
            (0b111010, -1),
            (0b001100, x),
            (0b110000, y),
            (0b001101, 0b1111_1111_1111_1101),
            (0b110001, 0b1111_1111_1111_1100),
            (0b001111, -x),
            (0b110011, -y),
            (0b011111, x + 1),
            (0b110111, y + 1),
            (0b001110, x - 1),
            (0b110010, y - 1),
            (0b000010, x + y),
            (0b010011, x - y),
            (0b000111, y - x),
            (0b000000, x & y),
            (0b010101, x | y),
        };

        foreach (var t in tests)
        {
            alu.X = x;
            alu.Y = y;
            alu.Op = t.op;
            Assert.Equal((short)t.expected, alu.Out);
        }
    }
}