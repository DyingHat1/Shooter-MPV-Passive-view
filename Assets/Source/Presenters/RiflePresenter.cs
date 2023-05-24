using System;
using UnityEngine;

public class RiflePresenter : Presenter
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public new Rifle Model => base.Model as Rifle;

    private void Update()
    {
        Model.Update(Time.deltaTime);

        if (IsNeedFlip())
        {
            Flip();
        }
    }

    private void Flip()
    {
        _spriteRenderer.flipY = !_spriteRenderer.flipY;
    }

    private bool IsNeedFlip()
    {
        return ((Mathf.Abs(Model.Rotation) > 90 && _spriteRenderer.flipY == false) || (Mathf.Abs(Model.Rotation) < 90 && _spriteRenderer.flipY));
    }
}
