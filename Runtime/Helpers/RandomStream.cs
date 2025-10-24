namespace DGP.UnityExtensions.Helpers
{
    /// <summary>
    /// Provides an independent, seedable random number stream for deterministic randomization.
    /// Useful for creating multiple isolated random sequences in gameplay systems.
    /// </summary>
    public class RandomStream
    {
        private System.Random _random;

        /// <summary>
        /// Gets the current seed value used by this random stream.
        /// </summary>
        public int Seed { get; private set; }

        /// <summary>
        /// Initializes a new RandomStream with the specified seed.
        /// </summary>
        /// <param name="seed">The seed value for deterministic random generation.</param>
        public RandomStream(int seed)
        {
            SetSeed(seed);
        }
        
        /// <summary>
        /// Initializes a new RandomStream with a random seed from Unity's global Random.
        /// </summary>
        public RandomStream() 
            : this(UnityEngine.Random.Range(int.MinValue, int.MaxValue)) { }

        /// <summary>
        /// Resets the random stream with a new seed, allowing reproduction of the same sequence.
        /// </summary>
        /// <param name="seed">The seed value for deterministic random generation.</param>
        public void SetSeed(int seed)
        {
            Seed = seed;
            _random = new System.Random(seed);
        }

        /// <summary>
        /// Returns a random integer within the specified range.
        /// </summary>
        /// <param name="min">The inclusive lower bound.</param>
        /// <param name="max">The exclusive upper bound.</param>
        /// <returns>A random integer where min &lt;= value &lt; max.</returns>
        public int Range(int min, int max) => _random.Next(min, max);
        
        /// <summary>
        /// Returns a random float within the specified range.
        /// </summary>
        /// <param name="min">The inclusive lower bound.</param>
        /// <param name="max">The inclusive upper bound.</param>
        /// <returns>A random float where min &lt;= value &lt;= max.</returns>
        public float Range(float min, float max) => (float)(_random.NextDouble() * (max - min) + min);

        /// <summary>
        /// Returns a random float between 0.0 (inclusive) and 1.0 (inclusive).
        /// </summary>
        public float Value => (float)_random.NextDouble();
        
        /// <summary>
        /// Returns a random boolean with 50/50 probability.
        /// </summary>
        public bool CoinFlip => _random.Next(2) == 0;
        
        /// <summary>
        /// Generates an array of random floats within the specified range.
        /// </summary>
        /// <param name="count">The number of random values to generate.</param>
        /// <param name="min">The inclusive lower bound.</param>
        /// <param name="max">The inclusive upper bound.</param>
        /// <returns>An array of random floats where min &lt;= value &lt;= max.</returns>
        public float[] RandomSequence(int count, float min, float max)
        {
            float[] sequence = new float[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = Range(min, max);
            }
            return sequence;
        }
        
        /// <summary>
        /// Generates an array of random integers within the specified range.
        /// </summary>
        /// <param name="count">The number of random values to generate.</param>
        /// <param name="min">The inclusive lower bound.</param>
        /// <param name="max">The exclusive upper bound.</param>
        /// <returns>An array of random integers where min &lt;= value &lt; max.</returns>
        public int[] RandomSequence(int count, int min, int max)
        {
            int[] sequence = new int[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = Range(min, max);
            }
            return sequence;
        }
    }
}