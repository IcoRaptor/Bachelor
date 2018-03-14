using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    #region Variables

    private const float _SCALE = 3f;

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _smoothTime = 0f;
    [SerializeField]
    private Vector3 _offset = new Vector3();

    private Camera _cam;
    private Rigidbody _rb;
    private Vector3 _pos;
    private Vector3 _velocity;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        _cam.orthographicSize = (Screen.height / 100f) / _SCALE;

        _pos = Vector3.SmoothDamp(_rb.position,
            _target.position, ref _velocity, _smoothTime) +
            _offset;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_pos);
    }
}