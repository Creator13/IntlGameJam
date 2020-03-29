using System;
using System.Linq;

namespace cvanbattum.Tools {
    public static class Random {
        /// <summary>
        /// Generates an array of random integers within the given range that are all unique.
        /// </summary>
        /// <param name="min">Inclusive lower bound</param>
        /// <param name="max">Exclusive upper bound</param>
        /// <param name="len">Length of array to generate</param>
        public static int[] UniqueIntegers(int min, int max, int len) {
            if (len < 1)
                throw new ArgumentException($"Length of array to generate must be a positive integer. Was: {len}");

            if (min >= max) {
                throw new ArgumentException(
                    $"min value cannot be larger than or equal to max value. Got {min} > {max}");
            }

            if (max - min < len) {
                throw new ArgumentException(
                    $"Distance ({max - min}) between min ({min}) and max ({max}) was smaller than requested length ({len}).");
            }

            var values = new int[len];
            var random = new System.Random();

            for (var i = 0; i < len; i++) {
                int val;
                do {
                    val = random.Next(min, max);
                } while (values.Contains(val) && val != 0);

                values[i] = val;
            }

            return values;
        }
    }
}
