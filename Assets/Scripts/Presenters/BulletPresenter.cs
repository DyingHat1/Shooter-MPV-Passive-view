using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletPresenter : Presenter
{
    private new Bullet Model => base.Model as Bullet;

    private void Update()
    {
        Model.Update(Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DefaultEnemyPresenter enemy))
        {
            Model.HitTarget(enemy.Model);
            Model.Destroy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Tilemap map))
        {
            Model.Destroy();
        }
    }
}
