using Framework;
using Framework.Messaging;
using UnityEngine;

public class TimeManager : SingletonAsObject<TimeManager>
{
    #region Variables

    [SerializeField]
    [Range(0.1f, 10f)]
    private float _scale = 10;

    private float _delta;
    private int _seconds;
    private int _minutes;

    #endregion

    #region Properties

    public static TimeManager Instance
    {
        get { return (TimeManager)_Instance; }
    }

    public int ScaledMinutes { get; private set; }

    public int ScaledHours { get; private set; }

    public int ScaledDays { get; private set; }

    #endregion

    private void Start()
    {
        ScaledMinutes = -1;
    }

    private void Update()
    {
        Tick();
        GenerateTimeString();
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
        int newScaledMinutes = _seconds % 60;

        if (newScaledMinutes == ScaledMinutes)
            return;

        ScaledMinutes = newScaledMinutes;
        ScaledHours = _minutes % 24;
        ScaledDays = 1 + _minutes / 24;

        string time = string.Format(
            "Day {0}\t{1:00}:{2:00}\n",
            ScaledDays, ScaledHours, ScaledMinutes);

        MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}