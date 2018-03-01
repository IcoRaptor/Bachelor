using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _smoothTime = 0f;
    [SerializeField]
    private Vector3 _offset = new Vector3();

    private Camera _cam;
    private Rigidbody _rb;
    private Vector3 _velocity;

    private const float _SCALE = 4f;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        _cam.orthographicSize = (Screen.height / 100f) / _SCALE;

        Vector3 pos = Vector3.SmoothDamp(_rb.position,
            _target.position, ref _velocity, _smoothTime) +
            _offset;

        _rb.MovePosition(pos);
    }
}