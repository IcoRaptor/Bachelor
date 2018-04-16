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

    private float _delta;

    #endregion

    #region Properties

    public static TimeManager Instance
    {
        get { return (TimeManager)_Instance; }
    }

    public GameTime GTime { get; private set; }

    #endregion

    private void Awake()
    {
        GTime = GameTime.InitialTime;
    }

    private void Update()
    {
        if (GameManager.Instance.GameState < GAME_STATE.MAIN_SCENE)
            return;

        _delta += Time.unscaledDeltaTime * _scale;

        if (_delta < 1f)
            return;

        GTime.Tick();

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
        string time = GTime.TimeString;
        MessagingSystem.Instance.QueueMessage(new TimeTextMessage(time));
    }
}