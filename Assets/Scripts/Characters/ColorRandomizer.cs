using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorRandomizer : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}