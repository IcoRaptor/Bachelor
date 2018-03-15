using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    #region Variables

    private const float _SCALE = 3f;

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    [Range(0f, 1f)]
    private float _smoothTime = 0f;

    private Camera _cam;
    private Rigidbody2D _rb;
    private Vector3 _pos;
    private Vector3 _velocity;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        _cam.orthographicSize = (Screen.height / 100f) / _SCALE;

        _pos = Vector3.SmoothDamp(_rb.position,
            _target.position, ref _velocity, _smoothTime);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_pos);
    }
}