using UnityEngine;

public class GlobalInput : MonoBehaviour
{
    private void Update()
    {
        if (_Input_ESC_Down)
            GameManager.Instance.SetGameState(GAME_STATE.SHUTDOWN);

        if (_Input_Plus_Down)
            TimeManager.Instance.AdjustTimeScale(1);

        if (_Input_Minus_Down)
            TimeManager.Instance.AdjustTimeScale(-1);

        if (_Input_Shift_Plus)
            TimeManager.Instance.AdjustTimeScale(Time.deltaTime * 10f);

        if (_Input_Shift_Minus)
            TimeManager.Instance.AdjustTimeScale(Time.deltaTime * -10f);

#if UNITY_EDITOR
        if (_Input_T_Down)
            TimerTest();
#endif
    }

    private void TimerTest()
    {
        Debug.LogFormat(
               "Timer started... Execution in 10 minutes!\n{0}",
               TimeManager.Instance.GetTimeStamp().TimeString);

        Timer.StartNew(0, 10, () =>
        {
            Debug.LogFormat(
                "Timer finished.\n{0}",
                TimeManager.Instance.GetTimeStamp().TimeString);
        });
    }

    #region Input

    private bool _Input_ESC_Down
    {
        get { return Input.GetKeyDown(KeyCode.Escape); }
    }

    private bool _Input_Plus_Down
    {
        get { return Input.GetKeyDown(KeyCode.KeypadPlus); }
    }

    private bool _Input_Minus_Down
    {
        get { return Input.GetKeyDown(KeyCode.KeypadMinus); }
    }

    private bool _Input_Shift_Plus
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) &&
                Input.GetKey(KeyCode.KeypadPlus);
        }
    }

    private bool _Input_Shift_Minus
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) &&
                Input.GetKey(KeyCode.KeypadMinus);
        }
    }

    private bool _Input_T_Down
    {
        get { return Input.GetKeyDown(KeyCode.T); }
    }

    #endregion
}