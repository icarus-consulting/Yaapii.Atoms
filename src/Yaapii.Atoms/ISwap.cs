namespace Yaapii.Atoms
{
    /// <summary>
    /// Swaps input to output.
    /// </summary>
    public interface ISwap<TInput, TOutput>
    {
        /// <summary>
        /// Turn input to output.
        /// </summary>
        TOutput Flip(TInput input);
    }

    /// <summary>
    /// Turn two inputs to one output.
    /// </summary>
    public interface ISwap<TInput1, TInput2, TOutput>
    {
        /// <summary>
        /// Turn two inputs to one output.
        /// </summary>
        TOutput Flip(TInput1 input1, TInput2 input2);
    }

    /// <summary>
    /// Turn three inputs to one output.
    /// </summary>
    public interface ISwap<TInput1, TInput2, TInput3, TOutput>
    {
        /// <summary>
        /// Turn three inputs to one output.
        /// </summary>
        TOutput Flip(TInput1 input1, TInput2 input2, TInput3 input3);
    }
}
