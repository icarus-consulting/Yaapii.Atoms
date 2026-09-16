# Yaapii.Atoms: Copilot Overview

Yaapii.Atoms provides small, composable object-oriented primitives for .NET. Prefer
composing these objects over extracting values early or reimplementing the same
operations.

## Important behavior

- Most atoms cache their result by default. Use the corresponding `Live*` type
  when the value must be evaluated again on every access.
- `IInput` and `IOutput` are exceptions: they are live by default. Use
  `StickyInput` to cache input content.
- Envelope types (`TextEnvelope`, `ScalarEnvelope<T>`, `ManyEnvelope<T>`,
  `MapEnvelope<...>`, and others) are the extension points for new atoms.
- Several namespaces contain types with the same short name, such as `Mapped`,
  `Filtered`, `Joined`, `Sorted`, and `LengthOf`. Check the namespace before use.

## Main contracts

| Contract | Purpose | Typical entrypoints |
| --- | --- | --- |
| [`IText`](src/Yaapii.Atoms/IText.cs) | Text whose string value is produced by `AsString()` | `TextOf`, `Formatted`, `Joined` |
| [`IScalar<T>`](src/Yaapii.Atoms/IScalar.cs) | Lazily supplies one value through `Value()` | `ScalarOf<T>`, `Solid<T>`, `Live<T>` |
| [`IFunc`](src/Yaapii.Atoms/IFunc.cs), [`IBiFunc`](src/Yaapii.Atoms/IBiFunc.cs), [`IAction`](src/Yaapii.Atoms/IAction.cs) | Object forms of functions and actions | `FuncOf`, `BiFuncOf`, `ActionOf` |
| `IEnumerable<T>` | Composable sequences | `ManyOf<T>`, `Mapped`, `Filtered` |
| `ICollection<T>` / `IList<T>` | Collection and list compositions | `CollectionOf<T>`, `ListOf<T>` |
| `IDictionary<TKey, TValue>` | Map compositions, including lazy values | `MapOf`, `KvpOf` |
| [`IInput`](src/Yaapii.Atoms/IInput.cs), [`IOutput`](src/Yaapii.Atoms/IOutput.cs) | Sources and destinations exposed as streams | `InputOf`, `OutputTo` |
| [`IBytes`](src/Yaapii.Atoms/IBytes.cs) | Byte content exposed by `AsBytes()` | `BytesOf`, `InputAsBytes` |
| [`INumber`](src/Yaapii.Atoms/INumber.cs) | Numeric value with typed conversions | `NumberOf`, `SumOf`, `AvgOf` |
| [`IFail`](src/Yaapii.Atoms/IFail.cs) | Reusable validation that throws on failure | `FailWhen`, `FailEmpty`, `FailNull` |
| [`ISwap`](src/Yaapii.Atoms/ISwap.cs) | Replaces an input with a configured value | `SwapOf`, `SwapSwitch` |

## Capabilities by namespace

### `Yaapii.Atoms.Text`

Create text from strings, inputs, bytes, URIs, or functions with `TextOf`.
Transform or inspect it with `Lower`, `Upper`, `Trimmed`, `Normalized`,
`Replaced`, `Split`, `Contains`, `StartsWith`, and `EndsWith`. Compose output
with `Joined`, `Formatted`, `Paragraph`, `CommaJoined`, or `BlankJoined`.
Encoding helpers include `Base64Text`, `TextBase64`, and `HexOf`.

### `Yaapii.Atoms.Scalar`

Wrap a value or function with `ScalarOf<T>`. Cache, synchronize, retry, select,
or validate scalar evaluation with `Solid<T>`, `Sync<T>`, `Retry<T>`,
`Fallback<T>`, `Ternary<T>`, and `NoNull<T>`. Boolean compositions include
`And`, `Or`, `Not`, `True`, and `False`; sequence lookups include `FirstOf<T>`,
`LastOf<T>`, and `ItemAt<T>`.

### `Yaapii.Atoms.Func`

Adapt delegates with `FuncOf`, `BiFuncOf`, and `ActionOf`. Compose behavior with
`ChainedFunc`, `Each`, `ActionIf`, or `ActionSwitch`; add caching,
synchronization, retries, repetition, null checks, or fallbacks with the
`Sticky*`, `Sync*`, `Retry*`, `Repeated*`, `NoNullsFunc`, and
`*WithFallback` types.

### `Yaapii.Atoms.Enumerable` and `Yaapii.Atoms.Enumerator`

Start a sequence with `ManyOf<T>`, `ManyOf`, `Params<T>`, `Repeated<T>`, or
`Endless<T>`. Query and transform it with `Mapped`, `Filtered`, `Reduced`,
`Distinct`, `Sorted`, `SortedBy`, `Reversed`, `Partitioned`, `HeadOf`,
`Skipped`, `Joined`, `Union`, and `Cycled`. Use `LengthOf`, `Contains`,
`NotEmpty`, `None`, `Single`, `MoreThan`, `LessThan`, or `ExactAmount` for
sequence questions. Enumerator utilities provide cached and live length
calculations.

### `Yaapii.Atoms.Collection` and `Yaapii.Atoms.Lists`

Use `CollectionOf<T>` and `ListOf<T>` as cached collection entrypoints.
`LiveCollection<T>` and `LiveList<T>` re-evaluate their source. Both families
provide mapped, joined, sorted, synchronized, and non-empty variants; lists
also provide indexed list semantics.

### `Yaapii.Atoms.Map`

Build maps with `MapOf` and entries with `KvpOf`. Overloads support
string/string, string/generic, and generic/generic maps. Values may be supplied
lazily. Use `Grouped`, `Joined`, `Sorted`, `FallbackMap`, `NoNulls`, `Solid`,
`Sync`, `LiveMap`, and `VersionMap` for common map compositions. `MapInputOf`
represents a map whose value is selected by an input key.

### `Yaapii.Atoms.IO`

Open stream-backed sources and destinations with `InputOf`, `OutputTo`,
`InputStreamOf`, `OutputStreamTo`, `ReaderOf`, and `WriterTo`. Read resources
with `Url`, `ResourceOf`, `MemoryInput`, or console inputs; write to files,
memory, or console outputs. Use `Tee*` and `AppendTo` to copy data while
reading or writing. Compression, archive, and integrity support includes
`GZipInput`, `GZipOutput`, `Zip`, `ZipFiles`, `ValidatedZip`, `Md5DigestOf`,
`Sha1DigestOf`, and `Sha256DigestOf`. Temporary resources are available through
`TempFile` and `TempDirectory`.

### `Yaapii.Atoms.Bytes`

Create byte content with `BytesOf`, `InputAsBytes`, or `ReaderAsBytes`. Convert
to and from Base64 or hexadecimal with `Base64Bytes`, `BytesBase64`, and
`HexBytes`; compare byte content with `BytesEqual`.

### `Yaapii.Atoms.Number` and `Yaapii.Atoms.Primitives`

Create typed numeric values with `NumberOf` and aggregate them with `SumOf`,
`AvgOf`, `MinOf`, and `MaxOf`. `LiveNumber` re-evaluates its source and
`Similar` compares floating-point values with a tolerance. Primitive adapters
include `BoolOf`, `CharOf`, `DoubleOf`, `FloatOf`, `IntOf`, and `LongOf`.

### Other namespaces

- `Yaapii.Atoms.Error`: conditional and specialized validation failures.
- `Yaapii.Atoms.Time`: convert between `DateTime` and formatted text with
  `DateOf` and `DateAsText`.
- `Yaapii.Atoms.Swap`: fixed and conditional input replacement.
- `Yaapii.Atoms.SymbolicLinkSupport`: symbolic-link operations for
  `FileInfo` and `DirectoryInfo`.

## Finding the right atom

Start with the capability directory under [`src/Yaapii.Atoms`](src/Yaapii.Atoms).
Each public atom has XML documentation describing its constructors and
behavior. Corresponding examples and edge cases are usually available under
[`tests/Yaapii.Atoms.Tests`](tests/Yaapii.Atoms.Tests). The user-oriented
[`README.md`](README.md) contains longer usage examples and LINQ analogies.
