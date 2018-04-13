using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    #region Variables

    [Header("Scale")]
    [SerializeField]
    [Range(0.5f, 2f)]
    private float _scale = 0;

    [Header("Follow")]
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    [Range(0f, 1f)]
    private float _smoothTime = 0f;

    private Rigidbody2D _rb;
    private Camera _cam;
    private Vector3 _pos;
    private Vector3 _velocity;
    private float _height;

    #endregion

    #region Properties

    private float _ScaledSize
    {
        get { return Screen.height / 100f / _scale; }
    }

    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = GetComponent<Camera>();

        _cam.orthographicSize = _ScaledSize;
        _height = Screen.height;
    }

    private void LateUpdate()
    {
        if (_height != Screen.height)
            _cam.orthographicSize = _ScaledSize;

        _pos = Vector3.SmoothDamp(
            _rb.position,
            _target.position,
            ref _velocity,
            _smoothTime);

        _height = Screen.height;
    }

    private void FixedUpdate()
    {
        var dist = _rb.position - (Vector2)_pos;

        if (dist.sqrMagnitude > 0.00005f)
            _rb.MovePosition(_pos);
    }
}