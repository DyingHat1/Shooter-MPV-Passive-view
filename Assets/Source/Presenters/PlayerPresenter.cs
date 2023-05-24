using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : KillablePresenter
{
    [SerializeField] private Joystick _controller;
    [SerializeField] private ButtonHoldChecker _shootButton;
    [SerializeField] private Animator _animator;

    private const string IsRunningBoolName = "IsRunning";

    private bool _isWatchingRight = true;

    public new Player Model => base.Model as Player;
    private Vector2 _direction => _controller.Direction;

    private void OnEnable()
    {
        _shootButton.Hold += OnShootButtonClick;
    }

    private void OnDisable()
    {
        _shootButton.Hold -= OnShootButtonClick;
    }

    private void Update()
    {
        if (_controller.Direction != Vector2.zero)
        {
            Model.Move(_controller.Direction, Time.deltaTime);
            _animator.SetBool(IsRunningBoolName, true);

            if (IsNeedFlip())
            {
                Flip();
            }
        }
        else
        {
            _animator.SetBool(IsRunningBoolName, false);
        }
    }

    private void OnShootButtonClick()
    {
        Model.Shoot();
    }

    private void Flip()
    {
        SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>();
        _isWatchingRight = !_isWatchingRight;

        foreach (SpriteRenderer bodyPart in childSprites)
        {
            if (bodyPart.gameObject.TryGetComponent(out RiflePresenter rifle) == false)
            {
                bodyPart.flipX = !bodyPart.flipX;
            }
        }
    }

    private bool IsNeedFlip()
    {
        return ((_direction.x < 0 && _isWatchingRight) || (_direction.x > 0 && _isWatchingRight == false));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ItemPresenter item) && item.IsDropped == false)
        {
            Model.AddItem(item.Model);
        }
    }
}
