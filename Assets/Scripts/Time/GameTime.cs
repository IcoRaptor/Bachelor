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

    public void Tick()
    {
        // Update minutes

        Minutes = Minutes + 1 == 60 ?
            0 :
            Minutes + 1;

        if (Minutes != 0)
        {
            TimeString = GenerateTimeString(Days, Hours, Minutes);
            return;
        }

        // Update hours

        Hours = Hours + 1 == 24 ?
            0 :
            Hours + 1;

        if (Hours != 0)
        {
            TimeString = GenerateTimeString(Days, Hours, Minutes);
            return;
        }

        // Update days

        Days++;

        TimeString = GenerateTimeString(Days, Hours, Minutes);
    }

    private static string GenerateTimeString(uint days, uint hours, uint minutes)
    {
        return string.Format(
           "Day {0} - {1:00} : {2:00}",
           days, hours, minutes);
    }
}