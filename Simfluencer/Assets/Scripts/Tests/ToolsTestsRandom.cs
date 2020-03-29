using System;
using System.Linq;
using NUnit.Framework;
using Random = cvanbattum.Tools.Random;

namespace Tests {
    public class ToolsTestsRandom {
        [Test]
        public void UniqueIntegersRangePasses() {
            // Test min > max
            Assert.Throws<ArgumentException>(() => Random.UniqueIntegers(10, 0, 1));
            // Test len < 1
            Assert.Throws<ArgumentException>(() => Random.UniqueIntegers(0, 4, 0));
            // Test distance
            Assert.Throws<ArgumentException>(() => Random.UniqueIntegers(0, 4, 5));
            // Test tight
            Assert.Throws<ArgumentException>(() => Random.UniqueIntegers(0, 1, 2));
            // Test zeroes
            Assert.Throws<ArgumentException>(() => Random.UniqueIntegers(0, 0, 0));

            // Test valid
            Assert.DoesNotThrow(() => Random.UniqueIntegers(-8, 50, 53));
        }

        [Test]
        public void UniqueIntegersValidation() {
            // Wide range, 'standard use'
            for (var j = 0; j < 50; j++) {
                const int length = 4;
                const int min = -30;
                const int max = 40;
                RunSequence(min, max, length);
            }
        }

        [Test]
        public void UniqueIntegersEdgeCases() {
            // Tight range
            var sequence = RunSequence(0, 1, 1);
            Assert.True(sequence.Length == 1);
            Assert.True(sequence[0] == 0);
        }

        private static int[] RunSequence(int min, int max, int length) {
            var sequence = Random.UniqueIntegers(min, max, length);

            // Check length of array
            Assert.True(sequence.Length == length);

            foreach (var val in sequence) {
                // Check if unique
                Assert.True(sequence.Count(i => i == val) == 1);
                // Check if in range
                Assert.True(val >= min);
                Assert.True(val < max);
            }

            return sequence;
        }
    }
}
