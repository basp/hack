# Hack
Hacking on "The Elements of Computer Systems".

This is a (partial) implementation of the computing platform described in the book mentioned above by Nisan and Schocken.

Since I wasn't in the mood to run too much Java the computer was built from scratch using C#. It's not perfect. The code is a little bit iffy in some places but it works.

The IL features function calls and returns and proper stack management. This will compile to Hack assembly which can be executed using the Hack computer simulator which is provided.

## Example Hack assembly
Below is a simple Hack assembly program that computes 2 + 3.
```
    @2
    D=A
    @3
    D=D+A
    @result
    M=D
```

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
    @eq     // defacto end
    0;JMP

```

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

The code above calculates `15` and leaves this value on top of the stack at `M[256]` and `SP` will point to the next address (`257`).
## 