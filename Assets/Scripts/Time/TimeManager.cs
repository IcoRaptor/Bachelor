using Framework;
using Framework.Messaging;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonAsComponent<TimeManager>
{
    #region Variables

    private const float _MIN_SCALE = 2f;
    private const float _MAX_SCALE = 30f;

    private float _scale = 24f / 5f; // 1 in-game day per 5 minutes

    private List<TimerInstance> _timers = new List<TimerInstance>();
    private Stack<TimerInstance> _timersToAdd = new Stack<TimerInstance>();
    private Stack<TimerInstance> _timersToRemove = new Stack<TimerInstance>();

    private float _delta;
    private uint _timeUnit;
    private uint _timeUnitLarge;

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
        _timeUnit = 60 * 12 - 1; // Start at 12:00
    }

    private void Update()
    {
        _delta += Time.unscaledDeltaTime * _scale;

        if (_delta >= 1.0f)
        {
            _delta = 0f;

            ++_timeUnit;
            _timeUnitLarge = _timeUnit / 60;

            ScaledMinutes = _timeUnit % 60;
            ScaledHours = _timeUnitLarge % 24;
            ScaledDays = 1 + _timeUnitLarge / 24;

            while (_timersToAdd.Count > 0)
                _timers.Add(_timersToAdd.Pop());

            while (_timersToRemove.Count > 0)
                _timers.Remove(_timersToRemove.Pop());

            foreach (var timer in _timers)
                timer.Tick();
        }

        UpdateUI();
    }

    /// <summary>
    /// Changes the time scale of the simulation
    /// </summary>
    public void AdjustTimeScale(float val)
    {
        _scale = Mathf.Clamp(_scale + val, _MIN_SCALE, _MAX_SCALE);
    }

    /// <summary>
    /// Adds a timer. The timer will be added before the next tick
    /// </summary>
    public void RegisterTimer(TimerInstance timer)
    {
        if (!_timers.Contains(timer))
            _timersToAdd.Push(timer);
    }

    /// <summary>
    /// Removes a timer. The timer will be removed before the next tick
    /// </summary>
    public void UnregisterTimer(TimerInstance timer)
    {
        if (_timers.Contains(timer))
            _timersToRemove.Push(timer);
    }

    /// <summary>
    /// Generates a string for the UI
    /// </summary>
    private void UpdateUI()
    {
        bool timePassed = _delta < 0.05f || ScaledMinutes != _timeUnit % 60;
        bool town = GameManager.Instance.GameState == GAME_STATE.TOWN_3;

        if (timePassed || town)
            return;

        string time = string.Format(
            "Day {0} - {1:00}h {2:00}",
            ScaledDays, ScaledHours, ScaledMinutes);

        MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}