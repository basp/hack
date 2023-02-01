    @2
    D=A
    @x
    M=D     // x = 2

    @3
    D=A
    @y
    M=D     // y = 3

    @x
    D=M
    @y
    D=D+M 
    @z
    M=D     // z <- (x + y)

    @z
    D=M
    @z
    D=D+M
    @zz
    M=D     // zz <- (z + z)

    @zz
    D=M
    @zz
    D=D+M
    @zzz
    M=D     // zzz <- (zz + zz)

(END)
    @END
    0;JMP