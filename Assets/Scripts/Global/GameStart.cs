using System.Collections;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
#if UNITY_EDITOR
        yield return null;
#else
        yield return new WaitForSecondsRealtime(1.0f);
#endif
        GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
    }
}