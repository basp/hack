# hack
Hacking on The Elements of Computer Systems


// push constant 5
    @R0         
    D=A         
    @5          
    D=D+A       
    @SP         
    A=M         
    M=D         
    @SP
    M=M+1       
(END)
    @END
    0;JMP

// pop constant {x}
    @SP
    A=M         
    D=M
    @SP
    M=M-1
    // ignore result for constant
