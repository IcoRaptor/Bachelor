using Framework.Debugging;

public static class Timer
{
    public static void StartNew(int hours, int minutes, TimerAction action)
    {
        if (hours < 0 || minutes < 0)
        {
            Debugger.Log(
                LOG_TYPE.ERROR,
                "Time < 0\nNo timer was started!");
        }
        else
            new TimerDelegate(hours, minutes, action);
    }
}

public class TimerDelegate
{
    #region Variables

    private int _targetTime;
    private int _timeUnit;
    private TimerAction _action;

    #endregion

    public TimerDelegate(int hours, int minutes, TimerAction action)
    {
        _timeUnit = -1;
        _targetTime = minutes % 60 + hours * 60;
        _action = action;

        TimeManager.Instance.RegisterTimer(this);
    }

    /// <summary>
    /// Advances the timer. Executes the action if target time is hit
    /// </summary>
    public void Tick()
    {
        ++_timeUnit;

        if (_timeUnit == _targetTime)
        {
            _action();

            TimeManager.Instance.UnregisterTimer(this);
        }
    }
}

public delegate void TimerAction();