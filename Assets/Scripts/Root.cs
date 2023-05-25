using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private Presenter _weaponPresenter;
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private InventoryPresenter _inventory;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private ScreensMenu _menu;

    private const int StartAmmoCount = 30;

    private Player _playerModel;
    private Weapon _weaponModel;
    private Vector2 _leftBottomBoundry;
    private Vector2 _rightTopBoundry;

    private void Awake()
    {
        _tileMap.CompressBounds();

        _leftBottomBoundry = new Vector2(_tileMap.transform.position.x - _tileMap.size.x/2, 
            _tileMap.transform.position.y - _tileMap.size.y/2);

        _rightTopBoundry = new Vector2(_tileMap.transform.position.x + _tileMap.size.x/2, 
            _tileMap.transform.position.y + _tileMap.size.y/2);

        Inventory inventory = CreateInventory();

        _weaponModel = new Rifle(_weaponPresenter.transform.position, _weaponPresenter.transform.rotation.z, 
            Config.RifleShootDelay, Config.RifleDamage, Config.RifleBulletSpeed);

        _playerModel = new Player(Config.PlayerHealth, _playerPresenter.transform.position, _playerPresenter.transform.rotation.z, 
            Config.PlayerSpeed, _weaponModel, _leftBottomBoundry, _rightTopBoundry, inventory);
        
        _weaponPresenter.Init(_weaponModel);
        _playerPresenter.Init(_playerModel);
        LoadSavedResults();
        _factory.CreateAmmo(_playerModel.Position, StartAmmoCount);
    }

    private void OnEnable()
    {
        _playerModel.Died += OnPlayerDied;
        _enemySpawner.AllEnemiesDied += OnAllEnemiesDied;
        _weaponModel.Shot += OnShot;
    }

    private void OnDisable()
    {
        _playerModel.Died -= OnPlayerDied;
        _enemySpawner.AllEnemiesDied -= OnAllEnemiesDied;
        _weaponModel.Shot -= OnShot;
    }

    private Inventory CreateInventory()
    {
        List<Cell> cells = new List<Cell>();

        for (int i = 0; i < _inventory.MaxItemsCount; i++)
        {
            Cell cell = new Cell();
            cells.Add(cell);
        }

        Inventory inventory = new Inventory(cells);
        _inventory.Init(_factory.CreateInventory(cells), inventory);

        return inventory;
    }

    private void OnShot(Bullet bullet)
    {
        _factory.CreatePlayerBullet(bullet);
    }

    private void OnAllEnemiesDied()
    {
        _playerModel.SaveResult();
        _menu.OpenWinScreen();
    }

    private void OnPlayerDied(Creature player)
    {
        _playerModel.ClearSavedResult();
        _menu.OpenLoseScreen();
    }

    private void LoadSavedResults()
    {
        PlayerData data = GameLoader.Load();

        if(data!=null)
        {
            Dictionary<string, int> dictionary;
            _playerModel.MoveTo(data.GetPosition());
            _weaponModel.MoveTo(data.GetPosition());
            dictionary = data.GetInventoryInfo();

            if (dictionary != null)
                foreach (var pair in dictionary)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        _factory.TryCreateItem(pair.Key, _playerModel.Position);
                    }
                }
        }
    }
}
