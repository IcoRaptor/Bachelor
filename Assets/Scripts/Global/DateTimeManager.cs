using Framework;
using Framework.Messaging;
using UnityEngine;

public class DateTimeManager : SingletonAsObject<DateTimeManager>
{
    #region Variables

    private const float _SCALE = 2f;

    private float _delta;
    private uint _seconds;
    private uint _minutes;
    private string dateTime;

    #endregion

    #region Properties

    public static DateTimeManager Instance
    {
        get { return (DateTimeManager)_Instance; }
    }

    #endregion

    private void Update()
    {
        _delta += Time.unscaledDeltaTime;

        if (_delta >= 1.0f)
        {
            _delta = 0;
            ++_seconds;
        }

        _minutes = _seconds / 60;

        GenerateDateTimeString();
    }

    private void GenerateDateTimeString()
    {
        uint scaledMinutes = (uint)(_seconds / _SCALE) % 60;
        uint scaledHours = (uint)(_minutes / _SCALE) % 24;

        string format = string.Format(
            "{0:00} : {1:00}\n",
            scaledHours, scaledMinutes);

        if (string.CompareOrdinal(dateTime, format) != 0)
        {
            dateTime = format;
            MessagingSystem.Instance.QueueMessage(new TimeTextMessage(dateTime));
        }
    }
}