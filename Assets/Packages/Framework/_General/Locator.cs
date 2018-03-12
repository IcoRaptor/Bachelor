using Framework.Audio;

namespace Framework
{
    /// <summary>
    /// Service locator
    /// </summary>
    public static class Locator
    {
        #region Properties

        public static IAudio Audio { get; private set; }

        #endregion

        /// <summary>
        /// Setter for the audio interface
        /// </summary>
        /// <param name="audio">Provided audio</param>
        public static void Provide(IAudio audio)
        {
            Audio = audio;
        }
    }
}