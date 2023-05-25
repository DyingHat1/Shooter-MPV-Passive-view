using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] private List<ItemPresenter> _itemPresenters;
    [SerializeField] private PlayerPresenter _player;

    private List<ItemView> _itemViews;
    private Inventory _inventory;

    public int MaxItemsCount => _itemPresenters.Count;

    private void OnDisable()
    {
        _inventory.ItemCollected -= OnItemCollected;

        foreach (ItemView view in _itemViews)
        {
            view.ItemDropped -= OnItemDropped;
        }
    }

    public void Init(List<ItemView> itemViews, Inventory inventory)
    {
        _inventory = inventory;
        _itemViews = itemViews;
        enabled = true;
        Subscribe();
    }

    private void Subscribe()
    {
        _inventory.ItemCollected += OnItemCollected;

        foreach (ItemView view in _itemViews)
        {
            view.ItemDropped += OnItemDropped;
        }
    }

    private void OnItemCollected(Item item, bool isCollectedToStack)
    {
        if (isCollectedToStack == false)
        {
            ItemView view = _itemViews.First(p => p.Name == item.Name);
            ItemPresenter presenter = _itemPresenters.First(p => p.Name == item.Name);
            view.AddNewItem(presenter.Sprite);
        }
    }

    private void OnItemDropped(ItemView view)
    {
        _inventory.DropItemFromCell(_itemViews.IndexOf(view), _player.Model.Position + Vector2.down * Config.DropForce);
    }
}
