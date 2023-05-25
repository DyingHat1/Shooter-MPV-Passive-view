using UnityEngine;

public class DefaultEnemyPresenter : KillablePresenter
{
    [SerializeField] private Animator _animator;

    private const string IsRunningBoolName = "IsRunning";
    private const string AttackTriggerName = "Attack";
    private const string IsWatchingRightBoolName = "IsWatchingRight";

    private bool _isWatchingRight = true;

    public new DefaultEnemy Model => base.Model as DefaultEnemy;

    private void OnEnable()
    {
        Model.Attacking += OnAttacking;
    }

    private void OnDisable()
    {
        Model.Attacking -= OnAttacking;
    }

    private void Update()
    {
        if (IsNeedFlip())
            Flip();

        Model.Update(Time.deltaTime);
        _animator.SetBool(IsRunningBoolName, Model.IsMooving);
        _animator.SetBool(IsWatchingRightBoolName, _isWatchingRight);
    }

    private void OnAttacking()
    {
        _animator.SetTrigger(AttackTriggerName);
    }

    private void Flip()
    {
        SpriteRenderer[] childSprites = GetComponentsInChildren<SpriteRenderer>();
        _isWatchingRight = !_isWatchingRight;

        foreach (SpriteRenderer bodyPart in childSprites)
        {
            bodyPart.flipX = !bodyPart.flipX;
        }
    }

    private bool IsNeedFlip()
    {
        return ((Model.Direction.x < 0 && _isWatchingRight) || (Model.Direction.x > 0 && _isWatchingRight == false));
    }
}
