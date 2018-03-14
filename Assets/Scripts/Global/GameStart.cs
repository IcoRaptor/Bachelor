using Framework.Messaging;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Start()
    {
        TimeManager.Instance.WakeUp();
        MessagingSystem.Instance.WakeUp();

        GameManager.Instance.SetGameState(GAME_STATE.PLAYING);
    }
}