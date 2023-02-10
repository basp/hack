# Hack
This repository contains a (partial) implementation of the system described in the book "The Elements of Computers Systems" by Nisan and Schocken. It is sometimes also known as "From NAND to Tetris". 

The book specifies a project where we start with only a **NAND** chip and build up an actual computer using only our starting chip and the chips we can built with it. Next up is an assembler that can convert assembly code into a binary that can be executed on this (virtual) computer. The third step is to implement a virtual machine that transpiles intermediate language (IL) into Hack assembly. And finally is a high level programming language that can be compiled all the way to binary using the layers we built earlier.

The system described in this book is simple and elegant. And it is actually quite fun to program in Hack assembly since it is so limited and powerful at the same time. The complete system is not super trivial to implement but it is not that hard either. 

> The simulator, transpiler and assembler are quite useful tools in other situations as well. It is not hard to just instantiate them from code so you could for example have a database with functions that are just bytes that represent a Hack binary. If you want to execute one of these you just need to instantiate a simulator with a ROM chip that contains the bytes of your function. Make sure you set the arguments (if any) correctly and then run it. The result will be at `M[@ARGS]`.

The book contains the specifications for a very satisfying project that comes together in a quite spectacular finale. It's probably one of my favorite books and I recommend every programmer to work through it.

This is a (partial) implementation of the computing platform described in the book. It does not contain all the tooling that is included with the book such as all the visual debuggers and simulators. It only includes one tool that is able to perform the following actions:

* Translate IL (the VM language from the book) into Hack.
* Translate Hack into the binary format for the computer as specified in the book.
* Execute Hack binaries using the simulator.
* Disassemble any Hack binary into its corresponding assembly code. 

Note that formatting and symbols are lost when translating into binary. And since we do not have source mapping yet the disassembled code *will* be dense and without any symbols. It is however quite mesmerizing to look at and can be useful for development purpuses such as debugging and comparing different transpilers and/or assemblers.

## Example Hack assembly
Below is a simple Hack assembly program that computes `2 + 3`.
```
    @2                  
    D=A                 
    @3                  
    D=D+A               // D <- 2 + 3
    @result             
    M=D                 // M[@result] <- D
(end)
    @end
    0;JMP               // defacto halt
```

## Example intermediate langauge (IL)
And although programming in Hack assembly is pretty pleasant once you get into it, it does help to have a somewhat higher level of language. This is where we want to use IL which translates to Hack directly using the VM conventions described in the book.

```
function init 0
    push constant 12
    push constant 12
    call Math.mult 2
    push constant 2
    push constant 3
    call Math.mult 2
    add
    return

function Example04.mult 2   // mult(x, y)
    push constant 0
    pop local 0             // sum = 0
    push argument 1
    pop local 1             // j = y
label loop
    push constant 0
    push local 1
    eq
    if-goto end             // if j = 0 goto end
    push local 0
    push argument 0
    add
    pop local 0             // sum = sum + x
    push local 1
    push constant 1
    sub                     
    pop local 1             // j = j - 1
    goto loop
label end
    push local 0
    return                  // return sum
```

The code above defines a `mult` function with two parameters. In the IL these parameters have lost their identifiers but we can imagine that they are called `x` and `y` in a higher level language. The `mult` function returns the result of multiplying its arguments.

As per the VM specificiation, the `Sys.init` function is defined to be the entry point of the program. It first will call the `mult(12, 12)` which leaves `144` on the stack. Then it will call `mult(2, 3)` which leaves `6` on the stack. Before returning it will call `add` which just adds the top two items on the stack and pushes the result. This means that `144 + 6 = 150` is left on the stack.

When the `mult` function returns, the result of the is left on the stack at `M[256]` and `M[SP]` will be `257` pointing to the top of the stack. It is the responsibility of the caller to pop any results of the stack (if necessary).

After running the above program (as a Hack binary) the memory should look like this:

```
SP   : 257
LCL  : 256
...
R13  : 261
R14  : 63
R15  : 0
...
256: 150 <- LCL
257: 256 <- SP
258: 256
259: 2048
```

## Using the tool
The **Hack** assembly is a .NET program that can be executed to perform various tasks.

The tool can be executed by navigating into the `.\src\Hack` directory where the main `Hack` project is located. And then executing:
```
> dotnet run
```

Which should give output comparable to:
```
No action was specified
Usage - Hack <action> -options

GlobalOption   Description
Help (-?)      Shows this help

Actions

  Dasm <Path> -options - Disassembles a Hack binary

    Option       Description
    Out (-o)     The optional path to the output
    Path* (-P)   The path to the source file

  Bin <Path> -options - Runs a Hack binary

    Option       Description
    Runs (-x)    The number of times to execute [Default='1']
    Path* (-P)   The path to the source file

  Asm <Path> -options - Assembles a Hack file into a binary

    Option       Description
    Out* (-o)    The path to the output
    Path* (-P)   The path to the source file

  IL <Path> -options - Transpiles an IL source file to Hack

    Option       Description
    Out (-o)     The optional path to the output
    Path* (-P)   The path to the source file
```

If you get this output then everything is setup correctly but you're not supplying the right arguments yet. Read on to find out more.

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

This will transpile a file a file written in intermediate code to Hack assembly code.

In this case the output (`-o`) argument for the `il` command is optional. If it is not supplied the transpiled output will be print to the standard output instead. This is useful is you just want some quick (development) output on your console instead of storing it into a file.

All commands that translate to a form that can be converted to a reasonable string encoding will peform in the same way (i.e. if you leave out the `-o` argument they will just print to standard output).

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

### Disassembly a binary
You can disassembly a previously compiled Hack binary into its equivalent assembly instructions. 
```
> dotnet run dasm .\out.bin
```

Which should output something very simular to:
```
@256
D=A
@0
M=D
@1
M=D
@2
M=D
@2048
D=A
@3
M=D
@4
M=D
@63
D=A
@0
A=M
M=D
@0
M=M+1
...
```

Note that during the compilation process all symbolic information is essentially lost so when decompiling you will get back only raw addresses for `@` instructions.

Nevertheless, it can be useful to feed this output back into the assembler to make sure the resulting binary is equivalent to the original (the one that was presumably transpiled from IL).

## Summary
The Hack toolchain currently allows you to go from `IL -> Hack -> Binary`. But using the `dasm` command you can also go from `Binary -> Hack` although you **will** lose all symbolic information.

This feature is generally useful though to double check either the Hack distribution or your own interpretation of it. 

For more information regarding the transpilation between IL and assembly see the appendix which has a complete transpiled example of the example code from example #3.

## Tricks
A decent trick debugging is to transform your *IL* into binary via `IL -> asm -> bin` using the tool. And then feed back the generated binary into the **decompiler** in order to generate super raw Hack assembly code (`bin -> asm`). If you then *assemble* this disassembled assembly code (`asm -> bin`) you should get the exact same binary as you got compiling the IL in the first place.

In pseudocode:
```
expected <- asm(IL())
reversed <- dasm(expected)
actual <- asm(reversed)
actual == expected
```

This is a great way to make sure that all systems are aligned and doing the same thing (even if that is not the right thing).