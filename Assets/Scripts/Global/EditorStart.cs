using UnityEngine;

public class EditorStart : MonoBehaviour
{
#if UNITY_EDITOR

    // Necessary setup if the scene is started from the editor

    private void Awake()
    {
        if (!GameManager.IsAlive)
            GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
    }

#endif
}