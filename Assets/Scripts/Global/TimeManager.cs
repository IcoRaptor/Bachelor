using Framework;
using Framework.Messaging;
using UnityEngine;

public class TimeManager : SingletonAsComponent<TimeManager>
{
    #region Variables

    private float _scale = 2f; // 1 second = 2 minutes

    private float _delta;
    private int _seconds;
    private int _minutes;

    #endregion

    #region Properties

    public static TimeManager Instance
    {
        get { return (TimeManager)_Instance; }
    }

    public float TimeScale
    {
        get { return _scale; }
        set { _scale = value; }
    }

    public int ScaledMinutes { get; private set; }

    public int ScaledHours { get; private set; }

    public int ScaledDays { get; private set; }

    #endregion

    private void Start()
    {
        ScaledMinutes = -1;
        _seconds = 60 * 12 - 1; // Start at 12:00
    }

    private void Update()
    {
        Tick();
        GenerateTimeString();
    }

    private void Tick()
    {
        _delta += Time.unscaledDeltaTime * TimeScale;

        if (_delta >= 1.0f)
        {
            _delta = 0;
            ++_seconds;
        }

        _minutes = _seconds / 60;
    }

    private void GenerateTimeString()
    {
        // Skip if delta is too small

        if (_delta < 0.1f)
            return;

        int newScaledMinutes = _seconds % 60;

        // Skip if time hasn't changed

        if (newScaledMinutes == ScaledMinutes)
            return;

        // Calculate new time and generate string for UI

        ScaledMinutes = newScaledMinutes;
        ScaledHours = _minutes % 24;
        ScaledDays = 1 + _minutes / 24;

        string time = string.Format(
            "Day {0} - {1:00}h {2:00}\n",
            ScaledDays, ScaledHours, ScaledMinutes);

        if (GameManager.Instance.GameState == GAME_STATE.PLAYING)
            MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}