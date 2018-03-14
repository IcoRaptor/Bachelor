using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float _walkSpeed = 0;

    private Rigidbody2D _rb;
    private Animator _anim;
    private Vector2 _move = new Vector2();

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");

        bool isWalking = _move != Vector2.zero;

        _anim.SetBool("IsWalking", isWalking);

        if (!isWalking)
            return;

        _anim.SetFloat("Input_X", _move.x);
        _anim.SetFloat("Input_Y", _move.y);

        _move.Normalize();
        _move *= _walkSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move);
    }
}