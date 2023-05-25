using UnityEngine;

public class ItemPresenter : Presenter
{
    [SerializeField] private string _name;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

    public new Item Model => base.Model as Item;
    public string Name => _name;
    public Sprite Sprite => _spriteRenderer.sprite;
    public bool IsDropped { get; private set; }

    private void OnEnable()
    {
        IsDropped = false;
        Model.Dropped += OnDropped;
        Model.Collected += OnCollected;
    }

    private void OnDisable()
    {
        Model.Dropped -= OnDropped;
        Model.Collected -= OnCollected;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerPresenter player))
        {
            IsDropped = false;
        }
    }

    private void OnCollected()
    {
        Disable();
    }

    private void OnDropped()
    {
        IsDropped = true;
        Enable();
    }

    private void Enable()
    {
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }

    private void Disable()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
    }
}
