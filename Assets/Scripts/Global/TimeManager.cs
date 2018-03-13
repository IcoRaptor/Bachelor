using Framework;
using Framework.Messaging;
using UnityEngine;

public class TimeManager : SingletonAsObject<TimeManager>
{
    #region Variables

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _scale = 0.5f;

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

    #endregion

    private void Update()
    {
        CalculateTime();
        GenerateTimeString();
    }

    private void CalculateTime()
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
        int newScaledHours = _minutes % 24;

        if (newScaledHours == ScaledHours && newScaledMinutes == ScaledMinutes)
            return;

        ScaledHours = newScaledHours;
        ScaledMinutes = newScaledMinutes;

        string time = string.Format(
            "{0:00} : {1:00}\n",
            ScaledHours, ScaledMinutes);

        MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}