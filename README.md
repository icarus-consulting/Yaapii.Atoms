[![Build status](https://ci.appveyor.com/api/projects/status/py42p14apauef2uy/branch/master?svg=true)](https://ci.appveyor.com/project/icarus-consulting/yaapii-atoms/branch/master)
[![codecov](https://codecov.io/gh/icarus-consulting/Yaapii.Atoms/branch/master/graph/badge.svg)](https://codecov.io/gh/icarus-consulting/Yaapii.Atoms)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)
[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg?style=flat-square)](http://commitizen.github.io/cz-cli/)
[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)
# Overview
Object-Oriented Primitives for .NET.
This is a .NET port of the java library [Cactoos](https://github.com/yegor256/cactoos) by Yegor Bugayenko.

It follows all the rules suggested in the two "[Elegant Objects](https://www.amazon.de/Elegant-Objects-Yegor-Bugayenko/dp/1519166915)" books.

## Maintainer[s]:
<a href="https://github.com/koeeenig" style="margin-left: 5px">
    <img src="https://avatars.githubusercontent.com/u/18014331?v=4" width="50" title="koeeenig">
</a>

# Table Of Contents
- [Design Change](#Design-change-V1.0-vs-V2.0)
- [Migration](#migration)
- [Maps](#maps)
- [Functions](#functions)
- [IO Input / Output](#io-input--output)
- [Lists](#lists)
- [Scalar](#scalar)
- [Text](#text)
- [LinQ Analogy](#linq-analogy)



## Design change Version 1.0 vs 2.0

### Caching

Version 1 of Atoms follows the principles of cactoos. All objects in cactoos are so-called live objects. This means, if you create a Text from a url ```new TextOf(new Uri("http://www.google.de"))```, every call to that object will fetch the content again. There is no caching until you explicitely define it by using a Sticky object. Sticky objects exist for all Types (Text, Enumerable, Map...).

However, after two years of working with Atoms, we realized that developers in our teams tend to think differently. They build objects and use them as if they have been cached. This produces a lot of unnecessary calculations and leads to slowdowns in our apps which we then solve in a second round where we analyze which objects should have been sticky. On the other hand, there are only a few cases where developers really need the live sensing of changes. 

This has led to the decision to invert the library caching principle. **Atoms 2.0 now has all objects sticky by default**. We then introduced new **Live** Decorators instead. So if you need an object which senses changes, you decorate it using the live decorator:

```csharp
var exchangeRate = new Live(() => new TextOf(new Uri("https://api.exchangeratesapi.io/latest")));
```

Live decorators are available for all types.



#### Caching envelopes

If you want to write your own objects based on Atoms envelopes, you have a switch which you can use to tell the envelope if it should behave as a live object or not (default is no)

```csharp
public sealed class MyLiveTextObject : TextEnvelope
{
     MyLiveTextObject(...) : base(..., live: true)
}
```



#### Exception: Input and output

Input and Output types are not sticky by default.





### Shorthand Generics

While Java developers can skip the generic expression in constructors, C# does not allow it. We added shorthand objects to allow skipping it if you use string as generic type, for Enumerables and Maps. You have two objects to make an Enumerable: ```new ManyOf``` to get an enumerable of strings and ```new ManyOf<T>``` if you need another type.

There are three objects to make a map:

```csharp
new MapOf(new KvpOf("Key", "Value")); //A map string to string
new MapOf<int>(new KvpOf<int>("Key", 123)); //A map string to generic type
new MapOf<int, int>(new KvpOf<int, int>(123, 123)); //A map generic type to generic type
```

Envelopes are available for all three map types.



### Migration

We decided to not leave the old sticky objects in the new version to get a fresh start.

If you want to migrate from 1.x to 2.x and want to preserve the behaviour regarding sticky objects, you should (in this order):

- Replace ```ScalarOf``` with ```Live```
- Replace ```Sticky``` with ```ScalarOf```
- Replace ```ManyOf``` with ```LiveMany```
- Replace ```StickyEnumerable``` with ``ManyOf``
- Replace ```ListOf``` with ```LiveList```
- Replace ```StickyList``` with ``ListOf``
- Replace ```CollectionOf``` with ```LiveCollection```
- Replace ```Collection.Sticky``` with ```CollectionOf```
- Replace ```TextOf``` with ```LiveText``` (StickyText did not exist)
- Replace ```NumberOf``` with ```LiveNumber``` (Stickynumber did not exist)



## Maps

Maps are grouped in the Lookup Area of atoms. All map objects implement C# ```IDictionary<Key, Value> ``` interface.

```csharp
var map = new MapOf<string, int>("Amount", 100);
```

#### Kvp

CSharp maps use ```KeyValuePair<Key,Value>``` as contents. These are structs and therefore not objects which we can implement or decorate. Atoms offers the type ```IKVp<Key,Value>``` as alternative.

```csharp
var map = 
   new MapOf<string,int>(
      new KvpOf<string, int>("age", 28),
      new KvpOf<string, int>("height", 184),
   )
```



#### Lazy values

This allows maps to accept functions to build the value, and get a simple **strategy pattern**

```csharp
//If you ask the map for "github", it will execute the function and retrieve the content. The content of the first kvp is NOT retrieved.
var githubContent =
   new MapOf<string,string>(
      new KvpOf<string,string>("google", () => new TextOf(new Uri("https://www.google.de"))),
      new KvpOf<string,string>("github", () => new TextOf(new Uri("https://www.github.com")))
   )["github"];

//Note that MapOf is sticky, so if you need live content, decorate the map:
var liveContent =
   new LiveMap<string,string>(() =>
      new MapOf<string,string>(
         new KvpOf<string,string>("google", () => new TextOf(new Uri("https://www.google.de"))),
         new KvpOf<string,string>("github", () => new TextOf(new Uri("https://www.github.com")))
      )
   )["github"];

//Beware: If you have lazy values, you normally do NOT want to execute all functions. Atoms prevents it, so the following will fail:
foreach(var doNotDoThis in githubContent)
{
   ...
}

//If you know that you need all values, simply enumerate the keys:
foreach(var key in githubContent.Keys)
{
  var value = githubContent[key];
}
```



#### Shorthand Generics

To save typing, there are two shorthand map objects:

```csharp
new MapOf(new KvpOf("Key", "Value")) //Use without generic to get a string-string map
new MapOf<int>(new KvpOf("Key", 100)) //Use without generic to get a string-generic map
```



#### Shorthand Stringmap

You can make a string-string map by simple writing value pairs one after another:

```csharp
var translations = 
   new MapOf(
   	"Objects", "Objekte",
   	"Functions", "Funktionen",
   	"Bigmac", "Viertelpfünder mit Käse"
   )
```



## Functions

The interfaces are:
```csharp
//Function with input and output
public interface IFunc<In, Out>
{
    Out Invoke(In input);
}

//Function with output only
public interface IFunc<Out>
{
    Out Invoke();
}

//Function with two inputs and one output
public interface IFunc<In1, In2, Out>
{
    Out Invoke(In1 input1, In2 input2);
}

//Function with input only
public interface IAction<In>
{
    void Invoke(In input);
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
## Enumerables
Enumerables use the IEnumerable<T> and IEnumerator<T> interfaces from C#:
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
### Base Objects

Naming of base objects differs. To save chars, shorthand names are used: 

```csharp
//use without generic and get an IEnumerable<string>
var strings = new ManyOf("a string", "another string"); 

//use with generic and get an IEnumerable<T>
var ints = new ManyOf<int>(98, 20); 
```

### Filter

```csharp
new Filtered<string>(
    new List<string>() { "A", "B", "C" },
    (input) => input != "B"); //will be a list with "A" and "C" inside
```
### Get an item from an enumerable
```csharp
new ItemAt<int>(
        new ManyOf<int>(1, 2, 3),
        2
    ).Value(); //will be 3 (Zero based)

//To get the first item simply do not specify a position:
new ItemAt<int>(
        new ManyOf<int>(1, 2, 3)
    ).Value(); //will be 1

//To get an item with a fallback if it isn't there:
String fallback = "fallback";
                new ItemAt<string>(
                    new ManyOf<string>(), //empty list,
                    12, //position 12 which does not exist
                    fallback
                ).Value(); //will be "fallback"
```
### Sort lists
```csharp
//Default sorting is forward
new Sorted<int>(
    new ManyOf<int>(3, 2, 10, 44, -6, 0)
); //items will be sorted to -6, 0, 2, 3, 10, 44

//Use another comparator for sorting
new Sorted<string>(
    IReverseComparer<string>.Default, //comparator is from C#.NET library
    new ManyOf<string>(
        "a", "c", "hello", "dude", "Friend"
    )
); //will be sorted to hello, Friend, dude, c, a
```
### Count items in enumerables
```csharp
var l = new LengthOf<int>(
            new ManyOf<int>(1, 2, 3, 4, 5)
        ).Value(); //will be 5
```
### Map items in an enumerable to another type
```csharp
IText greeting = 
    new ItemAt<IText>(
        new Mapped<String, IText>(
            new ManyOf<string>("hello", "world", "damn"),
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
        new ManyOf<string>(
            "one", "two", "three"
            )),
    7
    ).Value(); //will be "two"
```
### Join lists together
```csharp
new LengthOf(
    new Joined<string>(
        new ManyOf<string>("hello", "world", "Miro"),
        new ManyOf<string>("how", "are", "you"),
        new ManyOf<string>("what's", "up")
    )
).Value(); //will be 8
```
### Limit lists
```csharp
new SumOfInts(
    new HeadOf<int>(
        new ManyOf<int>(0, 1, 2, 3, 4),
        3
    )).Value(); //will be 3 (0 + 1 + 2)
```
 ### Cache list contents
 ```csharp

//this snippet has an endless list, which then is limited to the size. Every time someone calls the list, size increases and the list would grow. But StickyEnumerable prevents that and always returns the same list.
 int size = 2;
 var list =
    new StickyEnumerable<int>(
        new HeadOf<int>(
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
            new ManyOf<int>(1,2,3,4,5));
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

## LinQ Analogy
### Standard Query Operators

| LinQ                   | Yaapii.Atoms                             |
| ---------------------- | ---------------------------------------- |
| **Aggregate**          | *Not available yet*                      |
| **All**                | And&lt;T&gt; <!--pre>var allResult = new And&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;() { "A", "B", "C" },<br>&nbsp;&nbsp;(input) => input != "B"<br>); //newFiltered contains A & C</pre--> |
| **Any**                | Or&lt;T&gt;<!--pre>var b = new Contains&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("Hello", "my", "cat", "is", "missing"),<br>&nbsp;&nbsp;(str) => str == "cat"<br>).Value()); //b = true </pre--> |
| **AsEnumerable**       | <pre>var arr = new int[]{ 1, 2, 3, 4 }; <br>var enumerable = new ManyOf&lt;int&gt;(arr);</pre> |
| **Average**            | <pre>var avg = new AvgOf(1, 2, 3, 4).AsFloat(); //avg = 2.5</pre> |
| **Cast**               | *Not available yet*                      |
| **Concat**             | <pre>var joined = new Joined&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("dies", "ist"),<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("ein", "Test")<br>).Value(); //joined = {"dies", "ist", "ein", "Test"}</pre> |
| **Contains**           | <pre>var b = new Contains&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("Hello", "my", "cat", "is", "missing"),<br>&nbsp;&nbsp;(str) => str == "cat"<br>).Value()); //b = true |
| **Count**              | <pre>var length = new LengthOf&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(1, 2, 3, 4, 5)<br>).Value(); //length will be 5</pre> |
| **DefaultIfEmpty**     | *Not available yet*                      |
| **Distinct**           | <pre>var dis = new Distinct&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(1, 2, 3),<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(10, 2, 30)<br>).Value() // dis = {1, 2, 3, 10, 30}</pre> //actual with bug |
| **ElementAt**          | <pre>var itm = new ItemAt&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(0,2,5),<br>&nbsp;&nbsp; 2<br>).Value() //itm = 2</pre> |
| **ElementAtOrDefault** | <pre>var itm = new ItemAt&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;(),<br>&nbsp;&nbsp;12,<br>&nbsp;&nbsp;fallback<br>).Value() // itm = "fallback"</pre> |
| **Empty**              | <pre>new EnmuerableOf&lt;int&gt;()</pre> |
| **Except**             | *Not available yet*                      |
| **First**              | <pre>var list = new EnumerableO&lt;int&gt;(1, 2, 3);<br>&nbsp;var first = new ItemAt&lt;int&gt;(list).Value();<br>&nbsp;// first = 1</pre> |
| **FirstOrDefault**     | <pre>var itm = new ItemAt&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;(),<br>&nbsp;&nbsp;1,<br>&nbsp;&nbsp;fallback<br>).Value() // itm = "fallback"</pre> |
| **Foreach**            | <pre>var list = new List[];<br> new Each&lt;int&gt;(<br>&nbsp;&nbsp;(i) => lst[i] = i,<br>&nbsp;&nbsp;0,1,2<br>).Invoke(); //eachlist = {0,1,2} |
| **GroupBy**            | *Not available yet*                      |
| **GroupJoin**          | *Not available yet*                      |
| **Intersect**          | *Not available yet*                      |
| **Join**               | *Not available yet*                      |
| **Last**               | <pre>var last = new ItemAt&lt;int&gt;(<br>&nbsp;&nbsp;new Reversed&lt;int&gt;(<br>&nbsp;&nbsp;&nbsp;&nbsp;new ManyOf(5, 6 ,7 ,8)<br>&nbsp;&nbsp;)<br>).Value() // last = 8</pre> |
| **LastOrDefault**      | <pre>var itm = new ItemAt&lt;string&gt;(<br>&nbsp;&nbsp;new Reversed&lt;string&gt;(<br>&nbsp;&nbsp;&nbsp;&nbsp;new ManyOf&lt;string&gt;()<br>&nbsp;&nbsp;),<br>&nbsp;&nbsp;1,<br>&nbsp;&nbsp;fallback<br>).Value() // itm = "fallback"</pre> |
| **LongCount**          | *Not available yet**                     |
| **Max**                | <pre>var max = new MaxOf(22, 2.5, 35.8).AsDouble(); //max = 35.8; .AsInt() = 35</pre> |
| **Min**                | <pre>var max = new MaxOf(22, 2.5, 35.8).AsDouble(); //max = 2.5; .AsInt() = 2</pre> |
| **OfType**             | *Not available yet*                      |
| **OrderBy**            | <pre>var sorted = new Sorted&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(3, 2, 10, 44, -6, 0)<br>) //sorted = {-6, 0, 2, 3, 10, 44</pre> |
| **OrderByDescending**  | <pre>var sorted = new Sorted&lt;string&gt;(<br>&nbsp;&nbsp;IReverseComparer&lt;string&gt;.Default,<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("a", "c", "hello", "dude", "Friend")<br>) //sorted = {hello, Friend, dude, c, a}</pre> |
| **Range**              | *Not available yet*                      |
| **Repeat**             | <pre>var repeated = new Repeated&lt;int&gt;(10,5) // repeated = {10, 10, 10, 10, 10}</pre> |
| **Reverse**            | <pre> var reversed = new Reversed&lt;int&gt;(ManyOf<int>(2,3,4)); //reversed = {4,3,2}</pre> |
| **Select**             | <pre>var selected = Mapped&lt;string,string&gt;(<br>&nbsp;&nbsp;new List&lt;string&gt;() {"One", "Two", Three"}, <br>&nbsp;&nbsp;(tStr, index) => $"{tStr}={index+1}"<br>)// selected = {"One=1", "Two=2", Three=3"}</pre> |
| **SelectMany**         | *Not available yet*                      |
| **SequenceEqual**      | *Not available yet*                      |
| **Single**             | *Not available yet*                      |
| **SingleOrDefault**    | *Not available yet*                      |
| **Skip**               | <pre>var skipped = new Skipped&lt;string&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;string&gt;("one", "two", "three", "four"),<br>&nbsp;&nbsp;2<br>) // skipped = {three, four}</pre> |
| **SkipWhile**          | *Not available yet*                      |
| **Sum**                | <pre>var sum = new SumOf(<br>&nbsp;&nbsp;1.5F, 2.5F, 3.5F<br>).AsFloat() //sum = 7.5</pre> |
| **Take**               | <pre>var lmt = new HeadOf&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(0, 1, 2, 3, 4),<br>&nbsp;&nbsp;3<br>)//lmt = {0, 1, 2}</pre> |
| **TakeWhile**          | *Not available yet*                      |
| **ThenBy**             | *Not available yet*                      |
| **ThenByDescending**   | *Not available yet*                      |
| **ToArray**            | *Not available yet*                      |
| **ToDictionary**       | <pre>var dic = new MapOf(<br>&nbsp;&nbsp;new Enumerable&lt;KeyValuePair<string,string&gt;>(<br>&nbsp;&nbsp;&nbsp;&nbsp;new KyValuePair&lt;string,string&gt;("key","value"),<br>&nbsp;&nbsp;&nbsp;&nbsp;new KyValuePair&lt;string,string&gt;("key2", "value2")<br>&nbsp;&nbsp;)<br>) // dic = {{key, value}{key2, value2}}</pre> |
| **ToList**             | <pre>var list = new CollectionOf&lt;int&gt;(<br>&nbsp;&nbsp;new ManyOf&lt;int&gt;(1,2,3,4)<br>);</pre> |
| **ToLookup**           | *Not available yet*                      |
| **Union**              | <pre>var enu = new Distinct&lt;int&gt;(<br>&nbsp;&nbsp;new Joined&lt;int&gt;(<br>&nbsp;&nbsp;&nbsp;&nbsp;new ManyOf&lt;int&gt;(1,2,3,4),<br>&nbsp;&nbsp;&nbsp;&nbsp;new ManyOf&lt;int&gt;(3,4,5,6)<br>&nbsp;&nbsp;).Value()<br>).Value(); //enu ={1,2,3,4,5,6} </pre> |
| **Where**              | <pre>var newFiltered = new Filtered&lt;string&gt;(<br>&nbsp;&nbsp;new List&lt;string&gt;() { "A", "B", "C" },<br>&nbsp;&nbsp;(input) => input != "B"<br>); //newFiltered contains A & C</pre> |
| **Zip**                | *Not available yet*                      |
