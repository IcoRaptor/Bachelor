using Framework;
using UnityEngine;

public class DateTimeManager : SingletonAsObject<DateTimeManager>
{
    #region Variables

    private const uint _SCALE = 2;

    private float _delta;

    [SerializeField]
    private uint _seconds;
    [SerializeField]
    private uint _minutes;

    #endregion

    #region Properties

    public static DateTimeManager Instance
    {
        get { return (DateTimeManager)_Instance; }
    }

    public uint Seconds
    {
        get { return _seconds; }
    }

    public uint Minutes
    {
        get { return _minutes; }
    }

    public string DateTimeString { get; private set; }

    #endregion

    private void Update()
    {
        _delta += Time.unscaledDeltaTime;

        if (_delta >= 1.0f)
        {
            _delta = 0;
            ++_seconds;
        }

        if (_seconds == 60)
        {
            _seconds = 0;
            ++_minutes;
        }

        GenerateDateTimeString();
    }

    private void GenerateDateTimeString()
    {
        uint scaledSeconds = _seconds / _SCALE;
        uint scaledMinutes = _minutes / _SCALE;

        string format = string.Format(
            "Hours: {0:00}, Minutes: {1:00}\n",
            scaledMinutes, scaledSeconds);

        if (DateTimeString != format)
        {
            DateTimeString = format;
            Debug.Log(DateTimeString);
        }
    }
}