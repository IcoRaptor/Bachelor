using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Collider2D))]
public class InteractionController : MonoBehaviour
{
    #region Variables

    private PlayerController _ctrl;
    private Collider2D _collider;

    private Vector2 _offset;

    private LinkedList<Interactable> _interactables =
        new LinkedList<Interactable>();

    #endregion

    private void Start()
    {
        _ctrl = GetComponent<PlayerController>();
        _collider = GetComponent<Collider2D>();

        _offset = new Vector2();
    }

    private void Update()
    {
        UpdateCollider(_ctrl.RawMove);

        if (_interactables.Count == 0)
            return;

        if (_Input_E_Down)
            _interactables.First.Value.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iActable = other.GetComponent<Interactable>();

        if (!iActable)
            return;

        _interactables.AddLast(iActable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var iActable = other.GetComponent<Interactable>();

        if (!iActable)
            return;

        _interactables.Remove(iActable);
    }

    private void UpdateCollider(Vector2 move)
    {
        if (move == Vector2.zero)
            return;

        bool diagonal = move.x != 0 && move.y != 0;

        _offset.y = move.y * 1.5f;
        _offset.x = diagonal ? 0 : move.x * 1.5f;

        _collider.offset = _offset;
    }

    #region Input

    private bool _Input_E_Down
    {
        get { return Input.GetKeyDown(KeyCode.E); }
    }

    #endregion
}