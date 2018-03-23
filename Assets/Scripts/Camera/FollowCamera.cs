using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    #region Variables

    [Header("Scale")]
    [SerializeField]
    [Range(1f, 5f)]
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
    private float _previousHeight;

    #endregion

    #region Properties

    private float _ScaledSize
    {
        get { return (Screen.height / 100f) / _scale; }
    }

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = GetComponent<Camera>();

        _cam.orthographicSize = _ScaledSize;
        _previousHeight = Screen.height;
    }

    private void LateUpdate()
    {
        if (_previousHeight != Screen.height)
            _cam.orthographicSize = _ScaledSize;

        _pos = Vector3.SmoothDamp(
            _rb.position,
            _target.position,
            ref _velocity,
            _smoothTime);

        _previousHeight = Screen.height;
    }

    private void FixedUpdate()
    {
        Vector2 dist = _rb.position - (Vector2)_pos;

        // Move the camera while avoiding the mini shake

        if (dist.sqrMagnitude > 0.00005f)
            _rb.MovePosition(_pos);
    }
}