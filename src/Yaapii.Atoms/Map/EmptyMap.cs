using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map without entries
    /// </summary>
    public sealed class EmptyMap : MapEnvelope
    {
        /// <summary>
        /// A map without entries
        /// </summary>
        public EmptyMap() : base(() => new MapOf(new ManyOf()), false)
        { }
    }
}
