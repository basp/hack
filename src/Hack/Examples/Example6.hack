// push constant 2
    @0
    D=A
    @2
    D=D+A
    @SP
    A=M       // M -> M[M[@SP]]
    M=D
    @SP
    M=M+1     // increment stack pointer
// push constant 3
    @0
    D=A
    @3
    D=D+A
    @SP
    A=M       // M -> M[M[@SP]]
    M=D
    @SP
    M=M+1     // increment stack pointer
// push constant 5
    @0
    D=A
    @5
    D=D+A
    @SP
    A=M       // M -> M[M[@SP]]
    M=D
    @SP
    M=M+1     // increment stack pointer
// push constant 7
    @0
    D=A
    @7
    D=D+A
    @SP
    A=M       // M -> M[M[@SP]]
    M=D
    @SP
    M=M+1     // increment stack pointer
// pop constant 2
    @SP
    A=M       // M -> M[M[@SP]]
    D=M       // read but just ignore
    @SP
    M=M-1     // decrement stack pointer
// pop constant 3
    @SP
    A=M       // M -> M[M[@SP]]
    D=M       // read but just ignore
    @SP
    M=M-1     // decrement stack pointer
// pop constant 5
    @SP
    A=M       // M -> M[M[@SP]]
    D=M       // read but just ignore
    @SP
    M=M-1     // decrement stack pointer
// pop constant 7
    @SP
    A=M       // M -> M[M[@SP]]
    D=M       // read but just ignore
    @SP
    M=M-1     // decrement stack pointer
(END)
    @END      // defacto halt
    0;JMP