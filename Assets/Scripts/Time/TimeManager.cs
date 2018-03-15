using Framework;
using Framework.Messaging;
using UnityEngine;

public class TimeManager : SingletonAsComponent<TimeManager>
{
    #region Variables

    private float _scale = 2f; // 2 in-game minutes per second
    private float _delta;
    private uint _seconds;
    private uint _minutes;

    #endregion

    #region Properties

    public static TimeManager Instance
    {
        get { return (TimeManager)_Instance; }
    }

    public uint ScaledMinutes { get; private set; }

    public uint ScaledHours { get; private set; }

    public uint ScaledDays { get; private set; }

    #endregion

    private void Start()
    {
        ScaledMinutes = uint.MaxValue;
        _seconds = 60 * 12 - 1; // Start at 12:00
    }

    private void Update()
    {
        Tick();
        GenerateTimeString();
    }

    public void AdjustTimeScale(float val)
    {
        _scale = Mathf.Clamp(_scale + val, 1f, 60f);
    }

    private void Tick()
    {
        _delta += Time.unscaledDeltaTime * _scale;

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

        if (_delta < 0.05f)
            return;

        uint newScaledMinutes = _seconds % 60;

        // Skip if time hasn't changed

        if (newScaledMinutes == ScaledMinutes)
            return;

        // Calculate new time and generate string for UI

        ScaledMinutes = newScaledMinutes;
        ScaledHours = _minutes % 24;
        ScaledDays = 1 + _minutes / 24;

        string time = string.Format(
            "Day {0} - {1:00}h {2:00}",
            ScaledDays, ScaledHours, ScaledMinutes);

        if (GameManager.Instance.GameState == GAME_STATE.PLAYING)
            MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}