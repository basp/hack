namespace Hack
{
    public class Mnemonics
    {
        public static IDictionary<Computation, string> Computations { get; } =
            Code.Computations.ToDictionary(x => x.Value, x => x.Key);

        public static IDictionary<Destination, string> Destinations { get; } =
            Code.Destinations.ToDictionary(x => x.Value, x => x.Key);

        public static IDictionary<Jump, string> Jumps { get; } =
            Code.Jumps.ToDictionary(x => x.Value, x => x.Key);
    }
}