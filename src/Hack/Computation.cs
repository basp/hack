namespace Hack;

public enum Computation
{
    Zero = 0b0_101010,
    One = 0b0_111111,
    MinusOne = 0b0_111010,
    Data = 0b0_001100,
    Address = 0b0_110000,
    NotData = 0b0_001101,
    NotAddress = 0b0_110001,
    MinusData = 0b0_001111,
    MinusAddress = 0b0_110011,
    DataPlusOne = 0b0_011111,
    AddressPlusOne = 0b0_110111,
    DataMinusOne = 0b0_001110,
    AddressMinusOne = 0b0_110010,
    DataPlusAddress = 0b0_000010,
    DataMinusAddress = 0b0_010011,
    AddressMinusData = 0b0_000111,
    DataAndAddress = 0b0_000000,
    DataOrAddress = 0b0_010101,
    Memory = 0b1_110000,
    NotMemory = 0b1_110001,
    MinusMemory = 0b1_110011,
    MemoryPlusOne = 0b1_110111,
    MemoryMinusOne = 0b1_110010,
    DataPlusMemory = 0b1_000010,
    DataMinusMemory = 0b1_010011,
    MemoryMinusData = 0b1_000111,
    DataAndMemory = 0b1_000000,
    DataOrMemory = 0b1_010101,
}
