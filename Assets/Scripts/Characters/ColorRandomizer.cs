using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorRandomizer : MonoBehaviour
{
    #region Variables

    private static int _counter = 123;
    private RNG _rng;

    #endregion

    private void Start()
    {
        _rng = new RNG(_counter++, SEED_TYPE.IN_BUILD);

        float r = _rng.Generate01();
        float g = _rng.Generate01();
        float b = _rng.Generate01();
        var color = new Color(r, g, b);

        GetComponent<Renderer>().material.color = color;
    }
}