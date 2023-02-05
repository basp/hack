param(
    [string] $Path
)

dotnet run il $Path -o .\out.hack
dotnet run asm .\out.hack -o .\out.bin
dotnet run bin .\out.bin