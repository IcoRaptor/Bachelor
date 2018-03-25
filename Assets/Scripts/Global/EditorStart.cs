using UnityEngine;

// Performs necessary setup if the scene is started from the editor
public class EditorStart : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        if (!GameManager.IsAlive)
            GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
#endif
    }
}