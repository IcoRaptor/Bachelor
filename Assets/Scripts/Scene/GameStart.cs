using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.WakeUp();
        GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
    }
}