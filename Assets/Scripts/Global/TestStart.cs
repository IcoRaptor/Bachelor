using AI.GOAP;
using UnityEngine;

public class TestStart : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_EDITOR
        GOAPReader.ReadAll();
#endif
    }
}