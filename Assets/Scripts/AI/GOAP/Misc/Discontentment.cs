using UnityEngine;

namespace AI.GOAP
{
    public class Discontentment
    {
        #region Variables

        private static readonly int _maxValues = GOAPResolver.RelevanceCount;

        private int[] _values;

        private RNG _rng;

        #endregion

        #region Constructors

        public Discontentment()
        {
            _values = new int[_maxValues];
            _rng = new RNG(Time.frameCount, SEED_TYPE.NOT_IN_BUILD);

            Increase();
        }

        #endregion

        #region Indexer

        public int this[int i]
        {
            get { return _values[i]; }
        }

        #endregion

        public void ClearValue(int i)
        {
            _values[i] = 0;
        }

        private void Increase()
        {
            for (int i = 0; i < _values.Length; i++)
                _values[i] += _rng.Generate(10, 20);

            Timer.StartNew(0, 30, () =>
            {
                Increase();
            });
        }
    }
}