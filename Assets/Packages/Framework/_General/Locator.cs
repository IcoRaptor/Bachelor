using Framework.Audio;

namespace Framework
{
    /// <summary>
    /// Service locator
    /// </summary>
    public static class Locator
    {
        #region Variables

        private static IAudio _audio;

        #endregion

        #region Properties

        public static IAudio Audio
        {
            get { return _audio; }
        }

        #endregion

        /// <summary>
        /// Setter for the audio interface
        /// </summary>
        /// <param name="audio">Provided audio</param>
        public static void Provide(IAudio audio)
        {
            _audio = audio;
        }
    }
}