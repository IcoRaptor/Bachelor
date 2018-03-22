using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowCamera : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    [Range(0f, 1f)]
    private float _smoothTime = 0f;

    private Rigidbody2D _rb;
    private Vector3 _pos;
    private Vector3 _velocity;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        _pos = Vector3.SmoothDamp(
            _rb.position,
            _target.position,
            ref _velocity,
            _smoothTime);
    }

    private void FixedUpdate()
    {
        Vector2 dist = _rb.position - (Vector2)_pos;

        // Move the camera while avoiding the mini shake

        if (dist.sqrMagnitude > 0.0001f)
            _rb.MovePosition(_pos);
    }
}