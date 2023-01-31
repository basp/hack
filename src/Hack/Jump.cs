namespace Hack;

[Flags]
public enum Jump : short
{
    Never = 0b000,
    GT = 0b001,
    EQ = 0b010,
    LT = 0b100,
    GE = GT | EQ,   
    NE = LT | GT,
    LE = LT | EQ,    
    Always = 0b111,
}
