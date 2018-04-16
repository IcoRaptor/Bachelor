using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorRandomizer : MonoBehaviour
{
    #region Variables

    private static int _counter = 42;

    #endregion

    private void Start()
    {
        var rng = new RNG(_counter++, SEED_TYPE.IN_BUILD);

        float r = rng.Generate01();
        float g = rng.Generate01();
        float b = rng.Generate01();
        var color = new Color(r, g, b);

        GetComponent<Renderer>().material.color = color;
    }
}