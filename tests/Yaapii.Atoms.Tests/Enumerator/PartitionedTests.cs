using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Enumerator
{
    public class PartitionedTests
    {
        [Fact]
        public void CountsPartititions()
        {
            int size = 3;
            IEnumerator<int> enumerator = new List<int>() {1,2,3,4,5,6,7,8,9,10}.GetEnumerator();


            var partition = new Partitioned<int>(size, enumerator);

            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();

            Assert.Equal(new List<int>() { 10}, partition.Current);
        }


        [Fact]
        public void Partionate()
        {
            int size = 1;
            IEnumerator<string> enumerator = new List<string>() { "Christian","Sauer"}.GetEnumerator();


            var partition = new Partitioned<string>(size, enumerator);

            partition.MoveNext();

           // Assert.Equal(new List<string>() { "Christian", "Sauer" }, partition.Current);
            Assert.Equal(new List<string>() { "Christian"}, partition.Current);
        }

        [Fact]
        public void ThrowsExceptionOnInvalidSize()
        {
            int size = 0;
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();
                     
            Assert.Throws<ArgumentException>(()=>
            new Partitioned<string>(size, enumerator).MoveNext()
            );
        }

        [Fact]
        public void CurrentThrowsException()
        {
            int size = 2;
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();

            Assert.Throws<InvalidOperationException>(() =>
            new Partitioned<string>(size, enumerator).Current
            );
        }

        [Fact]
        public void FailsOnEnd()
        {
            int size = 5;
            IEnumerator<int> enumerator = new List<int>() { 1, 2, 3, 4, 5}.GetEnumerator();

            var partition = new Partitioned<int>(size, enumerator);
            
            partition.MoveNext();
            partition.MoveNext();            

            Assert.Throws<InvalidOperationException>(()=>
            partition.Current
            );
        }
    }
}
