namespace Hack;

public enum Jump
{
    None = 0b000,
    GreaterThan = 0b001,
    Equal = 0b010,
    GreaterThanOrEqual = 0b011,
    LessThan = 0b100,
    NotEqual = 0b101,
    LessThanOrEqual = 0b110,
    Unconditionally = 0b111,
}