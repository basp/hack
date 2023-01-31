namespace Hack;

[Flags]
public enum Jump
{
    None = 0b000,
    GTE = GT | EQ,   
    GT = 0b001,
    EQ = 0b010,
    LT = 0b100,
    NE = LT | GT,
    LTE = LT | EQ,    
    Unconditionally = 0b111,
}
