public class GameTime
{
    #region Properties

    public uint Minutes { get; set; }

    public uint Hours { get; set; }

    public uint Days { get; set; }

    public string TimeString { get; private set; }

    #endregion

    public void Update(uint timeUnit, uint timeUnitLarge)
    {
        Minutes = timeUnit % 60;
        Hours = timeUnitLarge % 24;
        Days = 1 + timeUnitLarge / 24;

        TimeString = string.Format(
            "Day {0} - {1:00}h {2:00}",
            Days, Hours, Minutes);
    }
}