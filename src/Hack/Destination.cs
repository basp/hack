namespace Hack;

[Flags]
public enum Destination
{
    None = 0,
    Memory = 1,
    Destination = 2,
    Address = 4,
}