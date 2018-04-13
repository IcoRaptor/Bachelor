public class GameTime
{
    #region Properties

    /// <summary>
    /// Returns a GameTime set to 12:00
    /// </summary>
    public static GameTime InitialTime
    {
        get
        {
            var time = new GameTime()
            {
                Days = 1,
                Hours = 12,
                Minutes = 0
            };

            time.TimeString = GenerateTimeString(
                time.Days,
                time.Hours,
                time.Minutes);

            return time;
        }
    }

    public uint Minutes { get; set; }

    public uint Hours { get; set; }

    public uint Days { get; set; }

    public string TimeString { get; private set; }

    #endregion

    public void Update()
    {
        // Update minutes

        if (Minutes + 1 != 60)
        {
            Minutes++;
            TimeString = GenerateTimeString(
                Days,
                Hours,
                Minutes);

            return;
        }

        Minutes = 0;

        // Update hours

        if (Hours + 1 != 24)
        {
            Hours++;
            TimeString = GenerateTimeString(
                Days,
                Hours,
                Minutes);

            return;
        }

        Hours = 0;

        // Update days

        Days++;

        TimeString = GenerateTimeString(
            Days,
            Hours,
            Minutes);
    }

    private static string GenerateTimeString(uint days, uint hours, uint minutes)
    {
        return string.Format(
           "Day {0} - {1:00} : {2:00}",
           days, hours, minutes);
    }
}