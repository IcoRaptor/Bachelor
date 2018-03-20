using Framework.Messaging;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Start()
    {
        MessagingSystem.Instance.WakeUp();
        TimeManager.Instance.WakeUp();

        GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
    }
}