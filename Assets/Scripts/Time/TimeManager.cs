using Framework;
using Framework.Messaging;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonAsComponent<TimeManager>
{
    #region Variables

    private float _scale = 24f / 5f; // 1 in-game day per 5 minutes

    private List<TimerInstance> _timers = new List<TimerInstance>();
    private Stack<TimerInstance> _timersToAdd = new Stack<TimerInstance>();
    private Stack<TimerInstance> _timersToRemove = new Stack<TimerInstance>();

    private GameTime _gameTime;

    private float _delta;

    #endregion

    #region Properties

    public static TimeManager Instance
    {
        get { return (TimeManager)_Instance; }
    }

    #endregion

    private void Update()
    {
        if (GameManager.Instance.GameState <= GAME_STATE.DEFAULT)
            return;

        _delta += Time.deltaTime * _scale;

        if (_delta < 1f)
            return;

        _gameTime.Tick();

        UpdateTimers();
        UpdateUI();

        _delta = 0f;
    }

    /// <summary>
    /// Changes the time scale of the simulation
    /// </summary>
    public void AdjustTimeScale(float val)
    {
        _scale = Mathf.Clamp(_scale + val, 1f, 30f);
    }

    /// <summary>
    /// Returns the current in-game time
    /// </summary>
    public GameTime GetTimeStamp()
    {
        var gt = new GameTime()
        {
            Days = _gameTime.Days,
            Hours = _gameTime.Hours,
            Minutes = _gameTime.Minutes,
            TimeString = _gameTime.TimeString
        };

        return gt;
    }

    /// <summary>
    /// Adds a timer. The timer will be added before the next tick
    /// </summary>
    internal void RegisterTimer(TimerInstance timer)
    {
        if (!_timers.Contains(timer))
            _timersToAdd.Push(timer);
    }

    /// <summary>
    /// Removes a timer. The timer will be removed before the next tick
    /// </summary>
    internal void UnregisterTimer(TimerInstance timer)
    {
        if (_timers.Contains(timer))
            _timersToRemove.Push(timer);
    }

    /// <summary>
    /// Adds, removes and updates registered timers
    /// </summary>
    private void UpdateTimers()
    {
        while (_timersToAdd.Count > 0)
            _timers.Add(_timersToAdd.Pop());

        while (_timersToRemove.Count > 0)
            _timers.Remove(_timersToRemove.Pop());

        foreach (var timer in _timers)
            timer.Tick();
    }

    /// <summary>
    /// Sends a message to the UI displaying the time
    /// </summary>
    private void UpdateUI()
    {
        string time = _gameTime.TimeString;
        MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }

    public override void WakeUp()
    {
        _gameTime = GameTime.InitialTime;
    }

    public void Clear()
    {
        _timers.Clear();
        _timersToAdd.Clear();
        _timersToRemove.Clear();
    }
}