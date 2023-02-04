    @256
    D=A
    @SP
    M=D

(Sys.init)
    @0
    D=A
(start_locals)
    @end_locals
    D;JLE
    D=D-1
    @start_locals
    0;JMP
(end_locals)
    