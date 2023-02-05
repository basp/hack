# Hack
Hacking on "The Elements of Computer Systems".

This is a (partial) implementation of the computing platform described in the book mentioned above by Nisan and Schocken.

Since I wasn't in the mood to run too much Java the computer was built from scratch using C#. It's not perfect. The code is a little bit iffy in some places but it works.

The IL features function calls and returns and proper stack management. This will compile to Hack assembly which can be executed using the Hack computer simulator which is provided.

## The killer app
The main use case for this thing is to provide a computer *that can be instantiated* at will at runtime by code if necessary. In other words, it is a computer that can be instanted by a host programming language as part of its normal operating routine. This computer can then be used to perform all kinds of dynamic operations that would otherwise be non-trivial to program in the host operating language.

This computer will then be used to perform various sequences of instructions that are stored in a database that is made available by the server context in which these programs will run. By running in a well defined context and having a well defined specification of operations it's possible to limit the programs an more importantly - guard against - the possibility that they will monopolize the server and therefor hamper accessibility for other users (what is commonly known as a DOS or DDOS attack).

The most important fact of this shenanigans is that the database which stores the commands is accessible to the user. They can program their own commands and in essence create their own world. The trick here is to make sure that they cannot screw up eachothers work so easily unless there's a real gameplay reason for it.

This requires a lot of security and network infrastructure and we are in no way close but we'll keep these goals in mind as we push forward.

## Example Hack assembly
Below is a simple Hack assembly program that computes 2 + 3.
```
    @2
    D=A
    @3
    D=D+A
    @result
    M=D
(end)
    @end
    0;JMP       // defacto halt
```
(example #1)

Of course Hack assembly features loops, registers and conditionals as well:
```
    @2
    D=A
    @3
    D=D-A
    @eq
    0;JMP
    @5
    D=A
    @eq
    0;JMP
(eq)
    @eq
    0;JMP       // defacto halt
```
(example #2)

## Example intermediate langauge (IL)
And although programming in Hack assembly is pretty pleasant once you get into it, it does help to have a somewhat higher level of language. This is where we want to use IL which translates to Hack directly using the VM conventions described in the book.

```
function Sys.init 0
    call main 0
    return

function main 0
    push constant 2
    push constant 3
    call Example.foo 2
    push constant 5
    add
    return

function Example.foo 0
    push argument 0
    push argument 1
    add
    push argument 0
    push argument 1
    add
    call Example.bar 2
    return

function Example.bar 0
    push argument 0
    push argument 1
    add
    return
```
(example #3)

The code above calculates `15` and leaves this value on top of the stack at `M[256]` and `SP` will point to the next address (`257`).

The generated Hack assembly code (from example #3) is listed as a reference in the appendix. Be warned though - there's a lot of code there and the whole listing is pretty long. Might be due to the fact that my assembly code generation is not optimal.

## Using the tool
The **Hack** assembly is a **dotnet** program that can be executed to perform various tasks.

### Compile Hack assembly to binary
Hack assembly code can be directly converted to a binary that can be executed using the simulator that is included (see executing binaries example).
```
> dotnet run asm .\input.hack -o .\out.bin
```

The output (`-o`) argument for the `asm` command is mandatory since it is more or less impossible to display the binary output without using a HEX editor. The resulting output (`.\out.bin` in this case) can be run usin the `bin` action of the tool (see **Run a Hack binary** below).

### Transpile IL to Hack assembly
The tool can also be used as a transpiler from IL to Hack assembly.
```
> dotnet run il .\input.vm -o .\out.hack
```

In this case the output (`-o`) argument for the `il` command is optional. If it is not supplied the transpiled output will be print to the standard output instead.

### Run a Hack binary
Once you have assembled a Hack binary file you can run this with:
```
> dotnet run bin .\out.bin
```
This will run the `.\out.bin` file that was assembled by using the `asm` command on `.\out.hack` earlier.

When properly halted, it will also output some information about execution time, the values of well-known registers and information about the stack with the `SP`, `ARG` and `LCL` pointers indicated (unless `ARG`, `LCL` or both coincide with `SP`, in that case only the `SP` pointer will be marked).

```
1 runs in 0,01s (avg. 7,38ms/run)
SP   : 257
LCL  : 256
ARG  : 256
THIS : 2048
R8   : 0
R9   : 0
R10  : 0
R12  : 0
R13  : 261
R14  : 63
R15  : 0
...
256: 15 <- LCL
257: 256 <- SP
258: 256
259: 2048
262: 261
263: 256
265: 2048
266: 15
267: 5
268: 227
269: 266
270: 261
271: 2048
```

### Summary
The Hack toolchain currently allows you to go from `IL -> Hack -> Binary`. But using the `dasm` command you can also go from `Binary -> Hack` although you **will** lose all symbolic information.

This feature is generally useful though to double check either the Hack distribution or your own interpretation of it. 

For more information regarding the transpilation between IL and assembly see the appendix which has a complete transpiled example of the example code from example #3.

### Tricks
A common tactic of debugging is to transform your *IL* into binary via `IL -> asm -> bin` using the tool. And then feed back the generated binary into the **decompiler** in order to generate super raw Hack assembly code (`bin -> asm`). If you then *assemble* this disassembled assembly code (`asm -> bin`) you should get the exact same binary as you got compiling the IL in the first place.

In pseudocode:
```
expected = asm(IL());
reversed = dasm(expected);
actual = asm(reversed);
expected == actual;
```

This is a great way to make sure that all systems are aligned and doing the same thing (even if that is not the right thing).

## Appendix: Generated assembly
The IL from example (#3) will generate the following Hack assembly:
```
// Generated by Transpiler on 05/02/2023 @ 21:40
    // Point SP, LCL and ARG to base of stack.
    @256
    D=A
    @SP
    M=D
    @LCL
    M=D
    @ARG
    M=D
    // Point THIS and THAT to base of heap
    @2048
    D=A
    @THIS
    M=D
    @THAT
    M=D
    // call Sys.init 0
    // Push return address
    @Sys.init.return.0
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save LCL of calling function
    @LCL
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save ARG of calling function
    @ARG
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THIS of calling function
    @THIS
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THAT of calling function
    @THAT
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Reposition ARG for callee
    @SP
    D=M
    @0
    D=D-A
    @5
    D=D-A
    @ARG
    M=D
    // Reposition LCL for callee
    @SP
    D=M
    @LCL
    M=D
    // Transfer control
    @Sys.init
    0;JMP
(Sys.init.return.0)
(END)
    @END
    0;JMP
(Sys.init)
    // call main 0
    // Push return address
    @main.return.0
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save LCL of calling function
    @LCL
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save ARG of calling function
    @ARG
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THIS of calling function
    @THIS
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THAT of calling function
    @THAT
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Reposition ARG for callee
    @SP
    D=M
    @0
    D=D-A
    @5
    D=D-A
    @ARG
    M=D
    // Reposition LCL for callee
    @SP
    D=M
    @LCL
    M=D
    // Transfer control
    @main
    0;JMP
(main.return.0)
    // Prepare return jump
    // Store the top of the frame (FRAME) in temp register
    @LCL
    D=M
    @R13
    M=D
    // Put return address in temp register
    @5
    A=D-A
    D=M
    @R14
    M=D
    // Reposition the return value for the caller
    @SP
    AM=M-1
    D=M
    @ARG
    A=M
    M=D
    // Restore SP of the caller
    @ARG
    D=M+1
    @SP
    M=D
    // Restore THAT of the caller
    @R13
    D=M
    @1
    A=D-A
    D=M
    @THAT
    M=D
    // Restore THIS of the caller
    @R13
    D=M
    @2
    A=D-A
    D=M
    @THIS
    M=D
    // Restore ARG of the caller
    @R13
    D=M
    @3
    A=D-A
    D=M
    @ARG
    M=D
    // Restore LCL of the caller
    @R13
    D=M
    @4
    A=D-A
    D=M
    @LCL
    M=D
    // Goto return address
    @R14
    A=M
    0;JMP
(main)
    // push constant 2
    @2
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // push constant 3
    @3
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // call Example.foo 2
    // Push return address
    @Example.foo.return.0
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save LCL of calling function
    @LCL
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save ARG of calling function
    @ARG
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THIS of calling function
    @THIS
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THAT of calling function
    @THAT
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Reposition ARG for callee
    @SP
    D=M
    @2
    D=D-A
    @5
    D=D-A
    @ARG
    M=D
    // Reposition LCL for callee
    @SP
    D=M
    @LCL
    M=D
    // Transfer control
    @Example.foo
    0;JMP
(Example.foo.return.0)
    // push constant 5
    @5
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // add
    @SP
    AM=M-1
    D=M
    @SP
    AM=M-1
    D=D+M
    M=D
    @SP
    M=M+1
    // Prepare return jump
    // Store the top of the frame (FRAME) in temp register
    @LCL
    D=M
    @R13
    M=D
    // Put return address in temp register
    @5
    A=D-A
    D=M
    @R14
    M=D
    // Reposition the return value for the caller
    @SP
    AM=M-1
    D=M
    @ARG
    A=M
    M=D
    // Restore SP of the caller
    @ARG
    D=M+1
    @SP
    M=D
    // Restore THAT of the caller
    @R13
    D=M
    @1
    A=D-A
    D=M
    @THAT
    M=D
    // Restore THIS of the caller
    @R13
    D=M
    @2
    A=D-A
    D=M
    @THIS
    M=D
    // Restore ARG of the caller
    @R13
    D=M
    @3
    A=D-A
    D=M
    @ARG
    M=D
    // Restore LCL of the caller
    @R13
    D=M
    @4
    A=D-A
    D=M
    @LCL
    M=D
    // Goto return address
    @R14
    A=M
    0;JMP
(Example.foo)
    // push argument 0
    @ARG
    D=M
    @0
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // push argument 1
    @ARG
    D=M
    @1
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // add
    @SP
    AM=M-1
    D=M
    @SP
    AM=M-1
    D=D+M
    M=D
    @SP
    M=M+1
    // push argument 0
    @ARG
    D=M
    @0
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // push argument 1
    @ARG
    D=M
    @1
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // add
    @SP
    AM=M-1
    D=M
    @SP
    AM=M-1
    D=D+M
    M=D
    @SP
    M=M+1
    // call Example.bar 2
    // Push return address
    @Example.bar.return.0
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save LCL of calling function
    @LCL
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save ARG of calling function
    @ARG
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THIS of calling function
    @THIS
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Save THAT of calling function
    @THAT
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // Reposition ARG for callee
    @SP
    D=M
    @2
    D=D-A
    @5
    D=D-A
    @ARG
    M=D
    // Reposition LCL for callee
    @SP
    D=M
    @LCL
    M=D
    // Transfer control
    @Example.bar
    0;JMP
(Example.bar.return.0)
    // Prepare return jump
    // Store the top of the frame (FRAME) in temp register
    @LCL
    D=M
    @R13
    M=D
    // Put return address in temp register
    @5
    A=D-A
    D=M
    @R14
    M=D
    // Reposition the return value for the caller
    @SP
    AM=M-1
    D=M
    @ARG
    A=M
    M=D
    // Restore SP of the caller
    @ARG
    D=M+1
    @SP
    M=D
    // Restore THAT of the caller
    @R13
    D=M
    @1
    A=D-A
    D=M
    @THAT
    M=D
    // Restore THIS of the caller
    @R13
    D=M
    @2
    A=D-A
    D=M
    @THIS
    M=D
    // Restore ARG of the caller
    @R13
    D=M
    @3
    A=D-A
    D=M
    @ARG
    M=D
    // Restore LCL of the caller
    @R13
    D=M
    @4
    A=D-A
    D=M
    @LCL
    M=D
    // Goto return address
    @R14
    A=M
    0;JMP
(Example.bar)
    // push argument 0
    @ARG
    D=M
    @0
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // push argument 1
    @ARG
    D=M
    @1
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
    // add
    @SP
    AM=M-1
    D=M
    @SP
    AM=M-1
    D=D+M
    M=D
    @SP
    M=M+1
    // Prepare return jump
    // Store the top of the frame (FRAME) in temp register
    @LCL
    D=M
    @R13
    M=D
    // Put return address in temp register
    @5
    A=D-A
    D=M
    @R14
    M=D
    // Reposition the return value for the caller
    @SP
    AM=M-1
    D=M
    @ARG
    A=M
    M=D
    // Restore SP of the caller
    @ARG
    D=M+1
    @SP
    M=D
    // Restore THAT of the caller
    @R13
    D=M
    @1
    A=D-A
    D=M
    @THAT
    M=D
    // Restore THIS of the caller
    @R13
    D=M
    @2
    A=D-A
    D=M
    @THIS
    M=D
    // Restore ARG of the caller
    @R13
    D=M
    @3
    A=D-A
    D=M
    @ARG
    M=D
    // Restore LCL of the caller
    @R13
    D=M
    @4
    A=D-A
    D=M
    @LCL
    M=D
    // Goto return address
    @R14
    A=M
    0;JMP
```
