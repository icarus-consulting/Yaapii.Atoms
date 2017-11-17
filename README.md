# Yaapii.Atoms

[![Build status](https://ci.appveyor.com/api/projects/status/py42p14apauef2uy/branch/master?svg=true)](https://ci.appveyor.com/project/icarus-consulting/yaapii-atoms/branch/master)

# Overview
Object-Oriented Primitives for .NET.
This is a .NET port of the java library [Cactoos](https://github.com/yegor256/cactoos) by Yegor Bugayenko.

It follows all the rules suggested in the two "[Elegant Objects](https://www.amazon.de/Elegant-Objects-Yegor-Bugayenko/dp/1519166915)" books.

## Functions
The interfaces are:
```csharp
//Function with input and output
public interface IFunc<In, Out>
{
    Out Invoke(In input);
}

//Function with output only
public interface ICallable<T>
{
    T Call();
}

//Function with input only
public interface IProc<X>
{
    void Exec(X input);
}
```
### Execute functions
```csharp
var i = new FuncOf<int, int>(
                (number) => number++
            ).Invoke(1); //i will be 2
```
### Cache function output
```csharp
var url = new Url("https://www.google.de");
var f = new StickyFunc<Url, IText>((u) =>
            new TextOf(
                new InputOf(u))
        ).Invoke(url);

var html = f.Invoke(); //will load the page content from the web
var html = f.Invoke(); //will load the page content from internal cache
```
### Retry a function
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
### Repeat a function
```csharp
var count = 0;
new RepeatedFunc<int, int>(
        input => input++,
        3
    ).Invoke(count); //will return 3
```
### Use a fallback if a function fails
```csharp
new FuncWithFallback<string, string>(
    name =>
    {
        throw new Exception("Failure");
    },
    ex => "Never mind, " + name
).Invoke("Miro"); //will be "Never mind, Miro"
```
- And more

## IO Input / Output
The IInput and IOutput interfaces:
```csharp
public interface IInput
{
    Stream Stream();       
}

public interface IOutput
{
    Stream Stream();
}
```
### Input Stream of a file
```csharp
new InputOf(
        new Uri(@"file:///c:\secret-content-inside.txt")
    ).Stream(); //returns a readable stream of the file
```
### Output Stream to a file
```csharp
new OutputTo(
        new Uri(@"file:///c:\secret-content-inside.txt")
    ).Stream(); //returns a readable stream of the file
```
### Output Stream of Console
```csharp
new ConsoleOutput().Stream(); //Default console output
new ConsoleErrorOutput().Stream(); //Console error output
```
### Read file content as string
```csharp
var fileContent = 
    new TextOf(
        new InputOf(
                new Uri(@"file:///c:\secret-content-inside.txt")
            )
    ).AsString(); //reads the content and gives it as text
```
### Read Url page as string
```csharp
new TextOf(
    new InputOf(
        new Url("https://www.google.de"))
).AsString(); //gives the content html of the google start page
```

### Read a file and use a fallback if fails
``` csharp
new TextOf(
    new InputWithFallback(
        new InputOf(
            new Uri(Path.GetFullPath("/this-file-does-not-exist.txt")) //file to read
        ),
        new InputOf(new TextOf("Alternative text!")) //fallback to use
    ).AsString(); //will be "Alternative Text!"
```
### Get the length of a file
```csharp
long length =
    new LengthOf(
        new InputOf(
                new Uri(@"file:///c:\great-content-inside.txt")
            )).Value(); //will be the number of bytes in the file
```
- Write to file, url, byte arrays, streams
### Copy all you are reading from a Stream to a output stream
```csharp
new LengthOf(
    new TeeInput(
        "Welcome to the world of c:!", 
        new OutputTo(new Uri(@"file:///c:\greeting.txt")))
    ).Value();  //will write "Welcome to the world of c:!" to the file c:\greeting.txt. 
                //This happens because TeeInput puts every byte that is read to the specified output. 
                //When calling Value(), every byte is read to count the content.
```
### Copy input/output 1:1 while reading or writing
```csharp
var inPath = Path.GetFullPath(@"file:///c:\input-file.txt");
var outPath = Path.GetFullPath(@"file:///c:\output-file.txt");

new LengthOf(
    new TeeInput(
        new InputOf(new Uri(inPath)),
        new OutputTo(new Uri(outPath))
    )).Value(); //since LengthOf will read all the content when calling Value(), all that has been read will be copied to the output path.

//Alternative: Copy to Console output
new LengthOf(
    new TeeInput(
        new InputOf(new Uri(inPath)),
        new ConsoleOutput()
    )).Value(); //will dump the read content to output
```
### Cache what you have read
```csharp
    new TextOf(
        new StickyInput(
            new InputOf(new Url("http://www.google.de"))));

var html1 = input.AsString(); //will read the url from the web
var html2 = input.AsString(); //will return from the cache
```
## Lists
Lists use the IEnumerable<T> and IEnumerator<T> interfaces from C#:
```csharp
public interface IEnumerable<out T> : IEnumerable
{
    IEnumerator<T> GetEnumerator();
}

public interface IEnumerator<out T> : IEnumerator, IDisposable
{
    T Current { get; }
}
```
### Filter lists
```csharp
new Filtered<string>(
    new List<string>() { "A", "B", "C" },
    (input) => input != "B"); //will be a list with "A" and "C" inside
```
### Get an item from a list
```csharp
new ItemAt<int>(
        new EnumerableOf<int>(1, 2, 3),
        2
    ).Value(); //will be 3 (Zero based)

//To get the first item simply do not specify a position:
new ItemAt<int>(
        new EnumerableOf<int>(1, 2, 3)
    ).Value(); //will be 1

//To get an item with a fallback if it isn't there:
String fallback = "fallback";
                new ItemAt<string>(
                    new EnumerableOf<string>(), //empty list,
                    12, //position 12 which does not exist
                    fallback
                ).Value(); //will be "fallback"
```
### Sort lists
```csharp
//Default sorting is forward
new Sorted<int>(
    new EnumerableOf<int>(3, 2, 10, 44, -6, 0)
); //items will be sorted to -6, 0, 2, 3, 10, 44

//Use another comparator for sorting
new Sorted<string>(
    IReverseComparer<string>.Default, //comparator is from C#.NET library
    new EnumerableOf<string>(
        "a", "c", "hello", "dude", "Friend"
    )
); //will be sorted to hello, Friend, dude, c, a
```
### Count items in lists
```csharp
var l = new LengthOf<int>(
            new EnumerableOf<int>(1, 2, 3, 4, 5)
        ).Value(); //will be 5
```
### Map items in a list to another type
```csharp
IText greeting = 
    new ItemAt<IText>(
        new Mapped<String, IText>(
            new EnumerableOf<string>("hello", "world", "damn"),
            input => new UpperText(new TextOf(input)) //is applied to every item and will make a uppertext of it
            ),
        0
    ).Value(); //will be "HELLO"
```
```csharp
// Mapping items of a list to another type using index of items
new Mapped<string,string>(
    new List<string>() {"One", "Two", Three"},
    (input, index) => $"{input}={index+1}");
// Returns a IEnumerable<string> with Content {"One=1", "Two=2", Three=3"}
```
    
### Create cycling lists
```csharp
//here is a list with 3 items and you call the 7th item. The cycled list will not fail but start over when it reaches the end.
new ItemAt<string>(
    new Cycled<string>( //make a cycled list of the enumerable with 3 items
        new EnumerableOf<string>(
            "one", "two", "three"
            )),
    7
    ).Value(); //will be "two"
```
### Join lists together
```csharp
new LengthOf<string>(
    new Joined<string>(
        new EnumerableOf<string>("hello", "world", "Miro"),
        new EnumerableOf<string>("how", "are", "you"),
        new EnumerableOf<string>("what's", "up")
    )
).Value(); //will be 8
```
### Limit lists
```csharp
new SumOfInts(
    new Limited<int>(
        new EnumerableOf<int>(0, 1, 2, 3, 4),
        3
    )).Value(); //will be 3 (0 + 1 + 2)
```
 ### Cache list contents
 ```csharp

//this snippet has an endless list, which then is limited to the size. Every tome someone call the list, size increases and the list would grow. But StickyEnumerable prevents that and always returns the same list.
 int size = 2;
 var list =
    new StickyEnumerable<int>(
        new Limited<int>(
            new Endless<int>(1),
            new ScalarOf<int>(() => Interlocked.Increment(ref size))
            ));

new LengthOf(list).Value(); //will be 2
new LengthOf(list).Value(); //will be 2
 ```
- and more

## Scalar
The IScalar interface looks like this:
```csharp
public interface IScalar<T>
{
    T Value();
}
```
A scalar is an object which can encapsulate objects and functions that return objects. It enables you to let a function appear as its return value. This is very useful to keep constructors code-free but also keep your overall object count low.

Also, scalars can be used to perform logical operations like And, Or, Not and more on function results or objects.
### Encapsulate objects
```csharp
var sc1 = new ScalarOf<string>("this brave string");
string str = sc.Value(); //returns the string

var sc2 = new ScalarOf<IEnumerable<int>>(
            new EnumerableOf<int>(1,2,3,4,5));
IEnumerable<int> lst = sc2.Value(); //returns the list
```
### Encapsulate functions
```csharp
var sc =
    new ScalarOf<string>(
        () =>
        new TextOf(
            new InputOf(
                new Url("http://www.ars-technica.com")
            )
        ).AsString());

string html = sc.Value(); //will fetch the html from the url and return it as a string
```
 ### Cache function results
 ```csharp
 var sc =
    new StickyScalar<string>(
        () =>
        new TextOf(
            new InputOf(
                new Url("http://www.ars-technica.com")
            )
        ).AsString()).Value();

string html = sc.Value(); //will fetch the html from the url and return it as a string
string html2 = sc.Value(); //will return the html from the cache
```
### Logical And
```csharp
var result = 
    new And<True>(
        () => true,
        () => false,
        () => true).Value(); //will be false

var number = 3;
new And<True>(
    () => true, //function that returns true
    () => number == 4 //function that returns false
).Value(); //will be false

//you can also pass scalars into AND, and more.
```

### Logical ternary
```csharp
new Ternary<bool, int>(
    new True(), //condition is true
    6, //if true
    16 //if false
).Value(); //will be 6

new Ternary<int, int>(
    5, //input to test
    input => input > 3, //condition
    input => input = 8, //return if condition true
    input => input = 2 //return if condition false
).Value(); //will be 8
```
And more...

- Negative
- Max
- Min
- Or

## Text
The IText interface looks like this:
```csharp
public interface IText : IEquatable<IText>
{
    String AsString();
}
```
### Transform text
```csharp
//Lower a text
new LowerText(
    new TextOf("HelLo!")).AsString(); //will be "hello!"

//upper a text
new UpperText(
    new TextOf("Hello!")).AsString(); //will be "HELLO!"
```
### Reverse text
```csharp
new ReversedText(
    new TextOf("Hello!")).AsString(); //"!olleH"
```
### Trim text
```csharp
new TrimmedText(
    new TextOf("  Hello!   \t ")
    ).AsString(); // "Hello!"

new TrimmedLeftText(
    new TextOf("  Hello!   \t ")
    ).AsString(); // "Hello!   \t "

new TrimmedRightText(
    new TextOf("  Hello!   \t ")
    ).AsString(); // "  Hello!"
```
### Split text
```csharp
IEnumerable<Text> splitted = 
    new SplitText(
        "Hello world!", "\\s+"
    );
```
### Replace text
```csharp
new ReplacedText(
    new TextOf("Hello!"),
    "ello",     // what to replace
    "i"         // replacement
).AsString();   // "Hi!"
```
### Join texts
```csharp
new JoinedText(
    " ", 
    "hello", 
    "world"
).AsString();// "hello world"
```
### Format texts with arguments
```csharp
new FormattedText(
        "{0} Formatted {1}", 1, "text"
    ).AsString(); // "1 Formatted text"
```
### Convert from and to text
```csharp
//text from a string with encoding
var content = "Greetings, Mr.Freeman!";
new TextOf(
    content,
    Encoding.UTF8
);

//text from a input with default encoding
var content = "Hello, my precious coding friend! with default charset";
new TextOf(
    new InputOf(content)
);

//text from a StringReader
String source = "Hello, Dude!";
new TextOf(
    new StringReader(source),
    Encoding.UTF8
);

//text from a char array
new TextOf(
    'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
    ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a'
    );

//text from a byte array
byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
new TextOf(bytes);

//text from a StringBuilder
String starts = "Name it, ";
String ends = "then it exists!";
Assert.True(
        new TextOf(
            new StringBuilder(starts).Append(ends)
        );

//text from an exception
new TextOf(
    new IOException("It doesn't work at all")
);
```
