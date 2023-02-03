namespace Hack
{
    /// <summary>
    /// Provides a reverse mapping of the respective properties
    /// provided by the <see cref="Code"/> class.
    /// </summary>
    /// <remarks>
    /// Since all of this is static it will also ensure we have no
    /// duplicate instructions in our respective `Code` entries. If
    /// we do an exception will be thrown.
    /// </remarks>
    public static class Mnemonics
    {
        public static IDictionary<Computation, string> Computations { get; } =
            Code.Computations.ToDictionary(x => x.Value, x => x.Key);

        public static IDictionary<Destination, string> Destinations { get; } =
            Code.Destinations.ToDictionary(x => x.Value, x => x.Key);

        public static IDictionary<Jump, string> Jumps { get; } =
            Code.Jumps.ToDictionary(x => x.Value, x => x.Key);
    }
}