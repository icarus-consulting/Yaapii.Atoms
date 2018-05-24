using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Partitioned Enumerable
    /// <para>Is a IEnumerator</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Partitioned<T> : IEnumerator<IEnumerable<T>>
    {
        private readonly int size;
        private readonly IEnumerator<T> enumerator;

        private readonly List<T> buffer;

        public Partitioned(int size, IEnumerator<T> enumerator) : this(
            size,
            enumerator,
            new List<T>()
        )
        { }

        private Partitioned(int size, IEnumerator<T> enumerator, List<T> buffer)
        {
            this.size = size;
            this.enumerator = enumerator;
            this.buffer = buffer;
        }
        
        public IEnumerable<T> Current
        {
            get {
                if (buffer.Count < 1)
                {
                    throw new InvalidOperationException();
                }

                return buffer;
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (this.size < 1)
            {
                throw new ArgumentException("Partition size < 1");
            }

            return Partitionate();           
        }

        public void Reset()
        {
            this.enumerator.Reset();
            this.buffer.Clear();
        }

        private bool Partitionate()
        {
            bool result = true;

            this.buffer.Clear();

            if (!this.enumerator.MoveNext())
            {
                result = false;
            }

            else
            {
                this.buffer.Add(this.enumerator.Current);

                for (int i = 0; i < this.size - 1 && this.enumerator.MoveNext(); ++i)
                {
                    this.buffer.Add(this.enumerator.Current);
                }
            }
          
            return result;
        }
    }    
}
