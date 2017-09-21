using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoStatics // No Statics
#pragma warning disable VariablesArePrivate // Fields are private
namespace Yaapii.Atoms.Misc
{
    /// <summary>
    /// <see cref="Comparer{T}"/> that can compare reverse to help reversing lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IReverseComparer<T> : Comparer<T>
    {
        public static new readonly IReverseComparer<T> Default = new IReverseComparer<T>(Comparer<T>.Default);


        public static IReverseComparer<T> Reverse(Comparer<T> comparer)
        {
            return new IReverseComparer<T>(comparer);
        }

        private readonly Comparer<T> comparer = Default;

        private IReverseComparer(Comparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public override int Compare(T x, T y)
        {
            return comparer.Compare(y, x);
        }
    }
}
#pragma warning restore NoStatics // No Statics
#pragma warning restore VariablesArePrivate // Fields are private