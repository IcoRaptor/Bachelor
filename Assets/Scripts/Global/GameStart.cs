using Framework.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.WakeUp();
        MessagingSystem.Instance.WakeUp();

        SceneManager.LoadSceneAsync(1);
    }
}