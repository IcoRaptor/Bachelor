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
        // Exit the game if ESC is pressed

        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.SetGameState(GAME_STATE.END_GAME);
    }
}