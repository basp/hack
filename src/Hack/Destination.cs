namespace Hack;

using System;

[Flags]
public enum Destination : short
{
    /// <summary>
    /// The result of the computation should be ignored.
    /// </summary>
    None = 0,

    /// <summary>
    /// The result of the computation should be stored in memory.
    /// </summary>
    M = 1,
    
    /// <summary>
    /// The result of the computation should be stored in the D register.
    /// </summary>
    D = 2,

    /// <summary>
    /// The result of the computation should be stored in the A register.
    /// </summary>
    A = 4,

    /// <summary>
    /// The result of the computation should be stored in memory
    /// and the D register.
    /// </summary>
    MD = M | D,

    /// <summary>
    /// The result of the computation should be stored in memory
    /// and the A register.
    /// </summary>
    AM = A | M,

    /// <summary>
    /// The result of the computation should be stored in the A
    /// and D registers.
    /// </summary>
    AD = A | D,

    /// <summary>
    /// The result of the computation should be stored in memory
    /// and in the A and D registers.
    /// </summary>
    AMD = A | M | D,
}
