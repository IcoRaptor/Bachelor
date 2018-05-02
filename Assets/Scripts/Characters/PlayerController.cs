using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float _walkSpeed = 0;

    private Rigidbody2D _rb;
    private Animator _anim;

    private Vector2 _move;

    #endregion

    #region Properties

    public Vector2 RawMove { get; private set; }

    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _move = new Vector2();
        RawMove = new Vector2();
    }

    private void Update()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.y = Input.GetAxisRaw("Vertical");

        bool isWalking = _move != Vector2.zero;
        _anim.SetBool("isWalking", isWalking);

        RawMove = _move;

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