# Hack
This repository contains a (partial) implementation of the system described in the book **The Elements of Computers Systems** by **Nisan** and **Schocken**. It is sometimes also known as **From NAND to Tetris**. 

The book specifies a project where we start with only a **NAND** chip and build up an actual computer using only our starting chip and the chips we can built with it. Next up is an assembler that can convert assembly code into a binary that can be executed on this (virtual) computer. The third step is to implement a virtual machine that transpiles intermediate language (IL) into **Hack** assembly. And finally is a high level programming language that can be compiled all the way to binary using the layers we built earlier.

The system described in the book is simple and elegant. And it is actually quite fun to program in Hack assembly since it is so limited and powerful at the same time. The complete system is not super trivial to implement but it is not that hard either. 

The book contains the specifications for a very satisfying project that comes together in a quite spectacular finale. It's probably one of my favorite books and I recommend every programmer to work through it.

This is a (partial) implementation of the computing platform described in the book. It does not contain all the tooling that is included with the book such as all the visual debuggers and simulators. It only includes one tool that is able to perform the following actions:

* Translate IL (the VM language from the book) into Hack.
* Translate Hack into the binary format for the computer as specified in the book.
* Execute Hack binaries using a custom simulator.
* Disassemble any Hack binary into its corresponding assembly code. 

The custom simulator behaves exactly as specified in the book. However, it does not utilize the HDL from the book but instead is directly simulated using C# the .NET runtime instead.

> There are some ideas to provide a real hardware simulator and language for the CLR Hack platform in the future. This would allow us to built the whole system up from individual bits instead of relying on a relatively high level simulator.

When translating Hack assembly into binary the symbols are lost. And since we do not have source mapping yet the disassembled code *will* be dense and without any symbols. It is however quite mesmerizing to look at and can be useful for development purpuses such as debugging and comparing different transpilers and/or assemblers.

## Example Hack assembly
Below is a simple Hack assembly program that computes `2 + 3`.
```
    @2                  // A <- 2
    D=A                 // D <- A
    @3                  // A <- 3
    D=D+A               // D <- 2 + 3
    @result             // A <- @result
    M=D                 // M[@result] <- D
(end)
    @end                // A <- @end
    0;JMP               // defacto halt (no computation loop)
```

## Example intermediate language (IL)
Programming in Hack assembly is pretty pleasant once you get into it but it does help to have an intermediate language. This is where we want to use IL which translates to Hack using the `il` command of the tool. 

> IL is much more ergonomic when it comes to function declarations, function calls and returns since the transpiler will take care of all the label and stack management that is involved with these operations.

In IL, all code needs to reside in a function and there must be only one single `Sys.init` function to bootstrap the program.

In the file `Sys.vm` we have the `init` function. This function
will call `Math.mult` which should be in `Math.vm` in the same directory.
```
function init 0             // zero locals
    push constant 12
    push constant 12
    call Math.mult 2        // two arguments (x, y)
    push constant 2
    push constant 3
    call Math.mult 2        // same here
    add
    return
```

And then in file `Math.vm` we have the `mult` function. This is called by `Sys.init` from the `Sys.vm` file.
```
function mult 2   // two locals (sum, j)
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

The code above defines a `mult` function with two parameters. In the IL these parameters have lost their identifiers but we can imagine that they are called `x` and `y` in a higher level language. The `mult` function returns the result of multiplying its arguments by leaving it on top of the stack.

As per the VM specificiation, the `Sys.init` function is defined to be the entry point of the program. It first will call `mult(12, 12)` which leaves `144` on the stack. Then it will call `mult(2, 3)` which leaves `6` on the stack. Before returning it will call `add` which pops the top two items off the stack, adds them together and pushes the result back. This means that `144 + 6 = 150` is left on the stack.

When the `mult` function returns, the result is left on the stack at `M[ARG]` and `M[SP]` will be `257`, pointing to the top of the stack. It is the responsibility of the caller to pop any results of the stack (if necessary).

After running the above program (as a Hack binary) the memory should look like this:

```
SP   : 257 (M[SP] = 256)
LCL  : 256 (M[LCL] = 150)
ARG  : 256 (M[ARG] = 150)
THIS : 2048 (M[THIS] = 0)
THAT : 2048 (M[THAT] = 0)
...
R13  : 261
R14  : 63
R15  : 0
...
256: 150 <- LCL
257: 256 <- SP
258: 256
259: 2048
...
```

You will notice that `150` (our result) is left at the base of the stack at `M[256]`. This is also the address of the `LCL` and `ARG` registers before we made the jump(s) to `Math.mult`. 

By design, the return argument will be at `M[ARG]` when a function call returns. Additionally, `SP` will be equal to `ARG + 1`. 

If the caller (to which we return) has no locals then `LCL` will be equal to `ARG` and point to the same position in the stack (like in the example above).

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

If you get output that remotely resembles this then everything is setup correctly but you're probably not supplying the right arguments yet. Those will be explained next.

### Compile Hack assembly to binary
Hack assembly code can be directly converted to a binary that can be executed using the custom simulator.
```
> dotnet run asm .\input.hack -o .\out.bin
```

The output (`-o`) argument for the `asm` command is mandatory since it is not really useful to display the raw binary output.

> If you want to really inspect the binary output visually it is probably a good idea to use a HEX editor instead.

The resulting output (`.\out.bin` in this case) can be run usin the `bin` action of the tool (see **Run a Hack binary** below).

### Transpile IL to Hack assembly
The tool can also be used as a transpiler from IL to Hack assembly.
```
> dotnet run il .\input.vm -o .\out.hack
```

This will transpile a file written in intermediate language to Hack assembly code.

In this case the output (`-o`) argument for the `il` command is optional. If it is not supplied the transpiled output will be written to standard output instead. This is useful is you just want some quick (development) output on your console instead of storing it into a file.

> All commands that translate to a form that can be converted to a reasonable string encoding will peform in the same way (i.e. if you leave out the `-o` argument they will just print to standard output).

### Run a Hack binary
Once you have assembled a Hack binary file you can run this with:
```
> dotnet run bin .\out.bin
```

This will run the `.\out.bin` file that was assembled by using the `asm` command on `.\out.hack` earlier.

When properly halted, it will also output some information about execution time, the values of well-known registers and information about the stack with the `SP`, `ARG` and `LCL` pointers indicated (unless `SP`, `ARG`, `LCL` or both coincide with eachother, in that case only the most significant pointer will be marked). 

You can check the lowest memory positions to see if registers are pointing to the same address (which is common and perfectly normal). In the `mult` example from above, both `LCL` and `ARG` point to the same memory address the stack but only `LCL` will show up when the memory gets dumped at the end of the run.

> There are some plans to improve diagnostics on this part so that *all* pointers will be indicated if they point to the same address.

### Disassembly a binary
You can disassembly a previously compiled Hack binary into its equivalent assembly instructions. 
```
> dotnet run dasm .\out.bin
```

Which should output something similar to:
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

> The exact instructions you get depend on the assembler and also how you have been following along so far so do not worry if your output looks different. Just make sure that you see a `@256` and `@2048` in the beginning somewhere, that (most likely) means at least the stack and heap pointers are properly initialized.

Note that during the compilation process all symbolic information is essentially lost so when decompiling you will get back only raw addresses for `@` instructions.

> Yes, this makes the disassembled output very hard to follow.

Nevertheless, it can be useful to feed this output back into the assembler to make sure the resulting binary is equivalent to the original (the one that was presumably transpiled from IL).

## Summary
The Hack toolchain currently allows you to go from `IL -> Hack -> Binary`. But using the `dasm` command you can also go from `Binary -> Hack` although you **will** lose all symbolic information.

## Tricks
A decent sanity check is to transform your IL into binary via `IL -> asm -> bin` using the tool. And then feed back the generated binary into the **decompiler** in order to generate super raw Hack assembly code (`bin -> asm`). If you then *assemble* this disassembled assembly code (`asm -> bin`) you should get the exact same binary as you got compiling the IL in the first place.

In pseudocode:
```
expected <- asm(IL())
disassembled <- dasm(expected)
actual <- asm(disassembled)
actual == expected
```

This is a great way to make sure that all systems are aligned and doing the same thing (even if that is not the right thing).

## Credits
* **Noam Nisan** & **Shimon Schocken** for writing the book **The Elements of Computing Systems** and designing a wonderful system to implement and work with and teaching me so much about computers in general while implementing it.
* **Terence Parr** for **ANTLR** and **StringTemplate** which now have become indispensable in most of my programming projects. Having these tools was a huge time saver when implementing the various parsers and transpilers for this project.
* **Adam Abdelhamed** for the excellent **PowerArgs** which is my goto library whenever I need to deal with command line arguments. It is in almost all of my applications.
* **Adam Kay** for telling me that most software and programming languages suck, for being so instrumental in creating **Smalltalk** and for teaching me what *real* object oriented programming should be like.
* Joe Armstrong for being instrumental in creating Erlang which (although not used for this project) tought so much about programming and designing systems and interfaces in general. And also, Erlang is just really fun to program in.
* The wonderful people that are developing the **.NET framework**. You are doing amazing work and it is been amazing to see how things have progressed from .NET 1.1 (when I jumped on the train).
* All the unnamed heroes that went before and laid the foundation for these great people to provide awesome software.

And finally to **Adele Goldberg** who was instrumental to the development of Smalltalk and also the writing of the famous **Blue Book** (**Smalltalk-80, The Language and It's Implementation**). 

My frustration with OOP in the 90's led me to Smalltalk and I was fascinated by it. It didn't took too long for me to find the **Blue Book** which specifies the Smalltalk virtual machine. The machine that is specified is so elegant and eloquently described in the book that it is hard to put into words how wondeful it is.

I tried to implement Smalltalk but failed every time. This was by no means a fault of the book but just a side effect of my own incompetence. 

Now that I have some more experience I will likely give it another try. Maybe there will be a Smalltalk for .NET one day. I'm still dreaming. Without the **Blue Book** I wouldn't even contemplate this. Thank you Adele!