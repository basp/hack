// push constant 2
    @0
    D=A
    @2
    D=D+A
    @SP
    A=M       // A -> M[@SP]
    M=D       // M[M[@SP]] <- D
    @SP
    M=M+1     // M[@SP] <- M[@SP] + 1
// push constant 3
    @0
    D=A
    @3
    D=D+A
    @SP
    A=M       // A -> M[@SP]
    M=D       // M[M[@SP]] <- D
    @SP
    M=M+1     // M[@SP] <- M[@SP] + 1
// push constant 5
    @0
    D=A
    @5
    D=D+A
    @SP
    A=M       // A -> M[@SP]
    M=D       // M[M[@SP]] <- D
    @SP
    M=M+1     // M[@SP] <- M[@SP] + 1
(END)         // defacto halt
    @END
    0;JMP