public class Timer
{
    #region Variables

    private uint _targetMinutes;
    private uint _minutes;
    private TimerAction _action;

    #endregion

    public Timer(uint hours, uint minutes, TimerAction action)
    {
        _targetMinutes = minutes + hours * 60;
        _action = action;
    }

    public void Tick()
    {
        if (++_minutes == _targetMinutes)
            _action();
    }

    public delegate void TimerAction();
}