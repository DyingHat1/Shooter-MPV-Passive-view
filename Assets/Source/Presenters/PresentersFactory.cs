using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PresentersFactory : MonoBehaviour
{
    [SerializeField] private LayoutGroup _content;
    [SerializeField] private List<Presenter> _defaultEnemyTemplates;
    [SerializeField] private Presenter _playerBulletTemplate;
    [SerializeField] private Presenter _rifleTemplate;
    [SerializeField] private List<ItemPresenter> _itemTemplates;
    [SerializeField] private ItemPresenter _ammo;
    [SerializeField] private ItemView _cellTemplate;

    public void CreateRandomItem(Vector2 position)
    {
        int index = Random.Range(0, _itemTemplates.Count);
        Item item = new Item(position, 0 ,_itemTemplates[index].Name);
        CreatePresenter(_itemTemplates[index], item);
    }

    public void CreateAmmo(Vector2 position, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Item item = new Item(position, 0, _ammo.Name);
            CreatePresenter(_ammo, item);
        }
    }

    public bool CreateItem(string itemName, Vector2 position)
    {
        ItemPresenter presenter = _itemTemplates?.First(p => p.Name == itemName);

        if (presenter == null)
            return false;

        Item item = new Item(position, 0, presenter.Name);
        CreatePresenter(presenter, item);
        return true;
    }

    public List<ItemView> CreateInventory(List<Cell> cells)
    {
        List<ItemView> cellsView = new List<ItemView>();

        foreach(Cell cell in cells)
        {
            ItemView view = Instantiate(_cellTemplate, _content.transform);
            view.Init(cell);
            cellsView.Add(view);
        }

        return cellsView;
    }

    public void CreateDefaultEnemy(DefaultEnemy enemy)
    {
        int index = Random.Range(0, _defaultEnemyTemplates.Count);
        CreatePresenter(_defaultEnemyTemplates[index], enemy);
    }

    public void CreatePlayerBullet(Bullet bullet)
    {
        CreatePresenter(_playerBulletTemplate, bullet);
    }

    public void CreateRifle(Rifle rifle)
    {
        CreatePresenter(_rifleTemplate, rifle);
    }

    private Presenter CreatePresenter(Presenter template, Transformable model)
    {
        Presenter presenter = Instantiate(template);
        presenter.Init(model);
        return presenter;
    }
}
