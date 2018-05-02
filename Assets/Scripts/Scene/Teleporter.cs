using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Navigation"))
            return;

        switch (GameManager.Instance.GameState)
        {
            case GAME_STATE.MAIN_SCENE:
                GameManager.Instance.SetGameState(GAME_STATE.TOWN);
                break;

            case GAME_STATE.TOWN:
                GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
                break;
        }
    }
}