namespace Hack;

public enum Computation
{
    Zero = 0b0_101010,
    One = 0b0_111111,
    MinusOne = 0b0_111010,
    Destination = 0b0_001100,
    Address = 0b0_110000,
    NotDestination = 0b0_001101,
    NotAddress = 0b0_110001,
    MinusDestination = 0b0_001111,
    MinusAddress = 0b0_110011,
    DestinationPlusOne = 0b0_011111,
    AddressPlusOne = 0b0_110010,
    DestinationMinusOne = 0b0_001110,
    AddressMinusOne = 0b0_110010,
    DestinationPlusAddress = 0b0_000010,
    DestinationMinusAddress = 0b0_010011,
    AddressMinusDestination = 0b0_000111,
    DestinationAndAddress = 0b0_000000,
    DestinationOrAddress = 0b0_010101,
    Memory = 0b1_110000,
    NotMemory = 0b1_110001,
    MinusMemory = 0b1_110011,
    MemoryPlusOne = 0b1_110111,
    MemoryMinusOne = 0b1_110010,
    DestinationPlusMemory = 0b1_000010,
    DestinationMinusMemory = 0b1_010011,
    MemoryMinusDestination = 0b1_000111,
    DestinationAndMemory = 0b1_000000,
    DestinationOrMemory = 0b1_010101,
}
