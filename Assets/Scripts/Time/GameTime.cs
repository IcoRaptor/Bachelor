public class GameTime
{
    #region Properties

    public uint ScaledMinutes { get; set; }

    public uint ScaledHours { get; set; }

    public uint ScaledDays { get; set; }

    #endregion

    public void Update(uint timeUnit, uint timeUnitLarge)
    {
        ScaledMinutes = timeUnit % 60;
        ScaledHours = timeUnitLarge % 24;
        ScaledDays = 1 + timeUnitLarge / 24;
    }

    public override string ToString()
    {
        return string.Format(
            "Day {0} - {1:00}h {2:00}",
            ScaledDays, ScaledHours, ScaledMinutes);
    }
}