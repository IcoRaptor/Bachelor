using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float _walkSpeed = 0;

    private Vector2 _move = new Vector2();
    private Rigidbody2D _rb;
    private Animator _anim;

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        // Timer test

        Timer.StartNew(0, 15, () =>
        {
            Debug.LogFormat(
                "PlayerTimer:\nDay {0} - {1:00}h {2:00}",
                TimeManager.Instance.ScaledDays,
                TimeManager.Instance.ScaledHours,
                TimeManager.Instance.ScaledMinutes);
        });
    }

    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");

        bool isWalking = _move != Vector2.zero;
        _anim.SetBool("isWalking", isWalking);

        if (!isWalking)
            return;

        _anim.SetFloat("inputX", _move.x);
        _anim.SetFloat("inputY", _move.y);

        _move.Normalize();
        _move *= _walkSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move);
    }
}