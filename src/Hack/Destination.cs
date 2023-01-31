namespace Hack;

using System;

[Flags]
public enum Destination
{
    None = 0,

    M = 1,
    
    D = 2,

    A = 4,

    MD = M | D,

    AM = A | M,

    AD = A | D,

    AMD = A | M | D,
}
