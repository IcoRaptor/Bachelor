using UnityEngine;

public class GlobalInput : MonoBehaviour
{
#if UNITY_EDITOR

    // Necessary setup if the main scene is started from the editor

    private void Awake()
    {
        if (!TimeManager.IsAlive)
            TimeManager.Instance.WakeUp();

        if (!GameManager.IsAlive)
            GameManager.Instance.SetGameState(GAME_STATE.PLAYING);
    }

#endif

    private void Update()
    {
        if (_Input_ESC_Down)
            GameManager.Instance.SetGameState(GAME_STATE.END_GAME);

        if (_Input_Plus_Down)
            TimeManager.Instance.AdjustTimeScale(1);

        if (_Input_Shift_Plus)
            TimeManager.Instance.AdjustTimeScale(Time.deltaTime * 10f);

        if (_Input_Minus_Down)
            TimeManager.Instance.AdjustTimeScale(-1);

        if (_Input_Shift_Minus)
            TimeManager.Instance.AdjustTimeScale(Time.deltaTime * -10f);
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

    #endregion
}