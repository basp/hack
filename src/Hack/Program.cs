using Hack;

class Program
{
    static void Main(string[] args)
    {
        var source = File.ReadAllText(args[0]);

        var ram = new RAM32K();
        var rom = new ROM32K(Assembler.Assemble(source));

        const int runs = 1_000_000;

        var start = DateTime.Now;
        for (var i = 0; i < runs; i++)
        {
            var sim = new Simulator(rom, ram);
            sim.Run();
        }

        var finish = DateTime.Now;
        var duration = (finish - start);
        var seconds = Math.Round(duration.TotalSeconds, 2);
        var avg = Math.Round(duration.TotalMilliseconds / runs, 2);

        Console.WriteLine($"{runs} runs in {seconds}s (avg. {avg}ms/run)");

        for (short i = 0; i < 5; i++)
        {
            ram.Address = i;
            Console.WriteLine($"{i}: {ram.Out}");
        }
    }
}

