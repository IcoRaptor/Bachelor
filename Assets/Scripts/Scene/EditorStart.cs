using UnityEngine;
using UnityEngine.SceneManagement;

// Performs necessary setup if the scene is started from the editor
public class EditorStart : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        if (GameManager.IsAlive)
            return;

        GameManager.Instance.WakeUp();

        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex == 1)
            GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);

        if (buildIndex == 2)
            GameManager.Instance.SetGameState(GAME_STATE.TOWN);
#endif
    }
}