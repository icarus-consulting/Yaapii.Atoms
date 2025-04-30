using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class ManyEnvelopeTests
    {
        [Fact]
        public void CachesWhenLiveIsFalse()
        {
            var enumerated = 0;
            var many = new MockMany<int>(
                Enumerating,
                false
            );
            foreach (var _ in many) { }
            foreach (var _ in many) { }
            Assert.Equal(1, enumerated);

            IEnumerable<int> Enumerating()
            {
                enumerated++;
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }

        [Fact]
        public void DoesNotCacheWhenLiveIsTrue()
        {
            var enumerated = 0;
            var many = new MockMany<int>(
                Enumerating,
                true
            );
            foreach (var _ in many) { }
            foreach (var _ in many) { }
            Assert.Equal(2, enumerated);

            IEnumerable<int> Enumerating()
            {
                enumerated++;
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }
    }

    public sealed class MockMany<T> : ManyEnvelope<T>
    {
        public MockMany(Func<IEnumerable<T>> source, bool live) : base(source, live)
        { }
    }
}
