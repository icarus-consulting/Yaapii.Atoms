# Yaapii.Atoms

[![Build status](https://ci.appveyor.com/api/projects/status/5vcpwy657jhc657o/branch/master?svg=true)](https://ci.appveyor.com/project/icarus-consulting/Yaapii.Atoms/branch/master)

# Overview
Object-Oriented Primitives for .NET.
This is a .NET port of the java library [Cactoos](https://github.com/yegor256/cactoos) by Yegor Bugayenko.

It follows all the rules suggested in the two "[Elegant Objects](https://www.amazon.de/Elegant-Objects-Yegor-Bugayenko/dp/1519166915)" books.

## Functions
Function signatures in Atoms are:
```csharp
IFunc<Input, Output> //Function with input and output

ICallable<Output> //Function with output only

IProc<Input> //Function with input only
```
- Execute functions
```csharp
var i = new FuncOf<int, int>((number) => number++).Invoke(1); //i will be 2
```
- Cache function output
```csharp
var url = new Url("https://www.google.de");
var f = new StickyFunc<Url, IText>((u) =>
            new TextOf(
                new InputOf(u)).AsString()
        ).Invoke(url);

var html = f.Invoke(); //will load the page content from the web
var html = f.Invoke(); //will load the page content from internal cache
```
- Retry a function
```csharp
new RetryFunc<int, int>(
    input =>
    {
        if (new Random().NextDouble() > input)
        {
            throw new ArgumentException("May happen");
        }
        return 0;
    },
    100000
).Invoke(0.3d); //will try 100.000 times to get a random number under 0.3
```
- Repeat a function
```csharp
var count = 0;
new RepeatedFunc<int, int>(
        input =>
        {
            input++;
        },
        3
    ).Invoke(count); //will return 3
```
- Use a fallback if a function fails
```csharp
new FuncWithFallback<string, string>(
    name =>
    {
        throw new Exception("Failure");
    },
    ex => "Never mind, " + name
).Invoke("Miro"); //will be "Never mind, Miro"
```
- ...

## IO Input / Output
- Read from file, url, byte arrays, streams
- Write to file, url, byte arrays, streams
- Use a fallback input/output if your preferred one fails
- Copy input/output 1:1 while reading or writing
- Cache what you have read

## Lists
- Filter lists
- Get items from a list
- Order lists
- Count items in lists
- Map items in a list to another type
- Create cycling lists
- Join lists together
- Limit lists
- Cache list contents
- ...

## Scalar
- Encapsulate objects
- Encapsulate functions
- Cache objects
- Perform logical operations like
    - And
    - Or
    - Ternary
    - Negative
    - Max
    - Min

## Text
- Transform text
- Reverse text
- Trim text
- Replace text
- Join texts
- Format texts with arguments
- Convert from and to text