namespace Hack.Tests;

using System;
using Hack;
using Xunit;

public class SimulatorTests
{
    [Fact]
    public void TestSimulator5()
    {
        const short address = 130;

        var instructions = new[]
        {
            Compiler.Compile(0),
            Compiler.Compile(Computation.Memory, Destination.D),
            Compiler.Compile(1),
            Compiler.Compile(Computation.DataPlusMemory, Destination.D),
            Compiler.Compile(address),
            Compiler.Compile(Computation.Data, Destination.M),
            Compiler.Halt,
        };

        TestSimulator(
            instructions,
            new short[] { 2, -3 },
            address,
            expected: -1);
    }

    [Fact]
    public void TestSimulator4()
    {
        const short address = 130;

        var instructions = new[]
        {
            Compiler.Compile(0),
            Compiler.Compile(Computation.Memory, Destination.D),
            Compiler.Compile(1),
            Compiler.Compile(Computation.DataMinusMemory, Destination.D),
            Compiler.Compile(address),
            Compiler.Compile(Computation.Data, Destination.M),
            Compiler.Halt,
        };

        TestSimulator(
            instructions,
            new short[] { 2, -3 },
            address,
            expected: 5);
    }

    [Fact]
    public void TestSimulator3()
    {
        const short address = 100;

        var instructions = new[]
        {
            Compiler.Compile(0),
            Compiler.Compile(Computation.Memory, Destination.D),
            Compiler.Compile(1),
            Compiler.Compile(Computation.DataPlusMemory, Destination.D),
            Compiler.Compile(address),
            Compiler.Compile(Computation.Data, Destination.M),
            Compiler.Halt,
        };

        TestSimulator(
            instructions,
            new short[] { -2, -3 },
            address,
            expected: -5);
    }

    [Fact]
    public void TestSimulator2()
    {
        const short address = 100;

        var instructions = new[]
        {
            Compiler.Compile(0),
            Compiler.Compile(Computation.Memory, Destination.D),
            Compiler.Compile(1),
            Compiler.Compile(Computation.DataPlusMemory, Destination.D),
            Compiler.Compile(address),
            Compiler.Compile(Computation.Data, Destination.M),
            Compiler.Halt,
        };

        TestSimulator(
            instructions,
            new short[] { 2, 3 },
            address,
            expected: 5);
    }

    [Fact]
    public void TestSimulator1()
    {
        var instructions = new[]
        {
            Compiler.Compile(0),
            Compiler.Compile(Computation.One, Destination.M),
            Compiler.Compile(1),
            Compiler.Compile(Computation.Zero, Destination.M),
            Compiler.Compile(2),
            Compiler.Compile(Computation.MinusOne, Destination.M),
            Compiler.Halt,
        };

        TestSimulator(
            instructions,
            new short[0],
            new (short, short)[]
            {
                (0, 1),
                (1, 0),
                (2, -1),
            });
    }

    [Fact]
    public void TestHaltingJump()
    {
        var prog = new ROM32K(
            Compiler.Compile(Computation.Zero, jump: Jump.Always));

        var sim = new Simulator(prog);

        sim.Cycle();

        Assert.True(sim.IsHalted);
    }

    [Fact]
    public void TestCPU()
    {
        TestInstruction(
            new CPU { InM = 130 },
            Compiler.Compile(Computation.Memory, Destination.D),
            cpu => Assert.Equal(130, cpu.D));

        TestInstruction(
            new CPU { InM = 130 },
            Compiler.Compile(Computation.Memory, Destination.A),
            cpu => Assert.Equal(130, cpu.A));

        TestInstruction(
            new CPU { InM = 130 },
            Compiler.Compile(Computation.Memory, Destination.AD),
            cpu =>
            {
                Assert.Equal(130, cpu.A);
                Assert.Equal(130, cpu.D);
                Assert.False(cpu.WriteM);
            });

        TestInstruction(
            new CPU { InM = 130 },
            Compiler.Compile(Computation.Memory, Destination.AMD),
            cpu =>
            {
                Assert.Equal(130, cpu.A);
                Assert.Equal(130, cpu.D);
                Assert.Equal(130, cpu.AddressM);
                Assert.Equal(130, cpu.OutM);
                Assert.True(cpu.WriteM);
            });

        TestInstructions(
            new CPU(),
            new[]
            {
                Compiler.Compile(130),
                Compiler.Compile(Computation.Zero, jump: Jump.Always),
            },
            cpu =>
            {
                Assert.Equal(0, cpu.OutM);
                Assert.Equal(130, cpu.PC);
                Assert.False(cpu.WriteM);
            });
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

    private void TestSimulator(
        short[] program,
        short[] data,
        short address,
        short expected) =>
        TestSimulator(program, data, new[] { (address, expected) });

    private void TestSimulator(
        short[] program,
        short[] data,
        (short address, short expected)[] tests)
    {
        var rom = new ROM32K(program);
        var ram = new RAM32K(data);
        var sim = new Simulator(rom, ram);

        for (var i = 0; i < program.Length; i++)
        {
            Assert.False(sim.IsHalted);
            sim.Cycle();
        }

        Assert.True(sim.IsHalted);

        foreach (var (address, expected) in tests)
        {
            sim.Data.Address = address;
            Assert.Equal(expected, sim.Data.Out);
        }
    }

    private void TestInstruction(
        CPU cpu,
        short instruction,
        Action<CPU> assert) =>
        TestInstructions(cpu, new[] { instruction }, assert);

    private void TestInstructions(
        CPU cpu,
        short[] instructions,
        Action<CPU> assert)
    {
        foreach (var i in instructions)
        {
            cpu.Instruction = i;
            cpu.Cycle();
        }

        assert(cpu);
    }
}
