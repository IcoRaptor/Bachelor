using AI.GOAP;
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
        yield return new WaitForSecondsRealtime(1.5f);
#endif
        GOAPXmlReader.ReadFile(new XmlFileInfo("GoalSet"));

        GameManager.Instance.SetGameState(GAME_STATE.MAIN_SCENE);
    }
}