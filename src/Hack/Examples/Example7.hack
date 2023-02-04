    @256
    D=A
    @SP
    M=D

    // call f n (assumes n arguments have been pushed)
    // push return address
    @RETURN
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    
    // push LCL
    @LCL
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    
    // push ARG
    @ARG
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1

    // push THIS
    @THIS
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1

    // push THAT
    @THAT
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1

    // ARG = SP - n - 5
    @SP
    D=A
    @5
    D=D-A
    @0      // number of arguments
    D=D-A
    @ARG
    M=D

    // LCL = SP
    @SP
    D=A
    @LCL
    M=D
(RETURN)
(END)
    @END
    0;JMP
