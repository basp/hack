namespace Hack;

using System;

[Flags]
public enum Destination
{
    None = 0,
    Memory = 1,
    Destination = 2,
    Address = 4,

    A = Address,
    M = Memory,
    D = Destination,

    MD = Destination | Memory,

    AM = Address | Memory,

    AD = Address | Destination,

    AMD = Address | Memory | Destination,
}
