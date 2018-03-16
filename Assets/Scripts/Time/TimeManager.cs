using Framework;
using Framework.Messaging;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonAsComponent<TimeManager>
{
    #region Variables

    private const float _MIN_SCALE = 2f;
    private const float _MAX_SCALE = 30f;

    private float _scale = 2f; // 2 in-game minutes per second

    private List<TimerDelegate> _timers = new List<TimerDelegate>();
    private Stack<TimerDelegate> _timersToAdd = new Stack<TimerDelegate>();
    private Stack<TimerDelegate> _timersToRemove = new Stack<TimerDelegate>();

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
        Tick();
        GenerateTimeString();
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
    public void RegisterTimer(TimerDelegate timer)
    {
        _timersToAdd.Push(timer);
    }

    /// <summary>
    /// Removes a timer. The timer will be removed before the next tick
    /// </summary>
    public void UnregisterTimer(TimerDelegate timer)
    {
        _timersToRemove.Push(timer);
    }

    /// <summary>
    /// Advances the time simulation
    /// </summary>
    private void Tick()
    {
        _delta += Time.unscaledDeltaTime * _scale;

        if (_delta >= 1.0f)
        {
            _delta = 0f;
            ++_timeUnit;
            _timeUnitLarge = _timeUnit / 60;

            while (_timersToAdd.Count > 0)
                _timers.Add(_timersToAdd.Pop());

            while (_timersToRemove.Count > 0)
                _timers.Remove(_timersToRemove.Pop());

            foreach (var timer in _timers)
                timer.Tick();
        }
    }

    /// <summary>
    /// Generates a string for the UI
    /// </summary>
    private void GenerateTimeString()
    {
        // Skip if delta is too small

        if (_delta < 0.05f)
            return;

        uint newScaledUnit = _timeUnit % 60;

        // Skip if time hasn't changed

        if (newScaledUnit == ScaledMinutes)
            return;

        // Calculate new time and generate string for UI

        ScaledMinutes = newScaledUnit;
        ScaledHours = _timeUnitLarge % 24;
        ScaledDays = 1 + _timeUnitLarge / 24;

        string time = string.Format(
            "Day {0} - {1:00}h {2:00}",
            ScaledDays, ScaledHours, ScaledMinutes);

        if (GameManager.Instance.GameState >= GAME_STATE.MAIN_SCENE)
            MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}