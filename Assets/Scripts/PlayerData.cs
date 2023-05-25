using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private float[] _position = new float[2];
    private List<ItemInfo> _inventory = new List<ItemInfo>();

    public PlayerData(Player player, Inventory inventory)
    {
        KeyValuePair<string, int> cell;
        _position[0] = player.Position.x;
        _position[1] = player.Position.y;

        for (int i = 0; i < inventory.CellsCount; i++)
        {
            cell = inventory.GetInfoFromCell(i);

            if(cell.Key != Config.EmptyItemSlotName)
            {
                ItemInfo item = new ItemInfo(cell.Key, cell.Value);
                _inventory.Add(item);
            }
        }
    }

    public Vector2 GetPosition()
    {
        return new Vector2(_position[0], _position[1]);
    }

    public Dictionary<string,int> GetInventoryInfo()
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();

        foreach(ItemInfo item in _inventory)
        {
            dictionary.Add(item.Name, item.Count);
        }

        return dictionary;
    }

    [System.Serializable]
    public struct ItemInfo
    {
        public string Name { get; private set; }
        public int Count { get; private set; }

        public ItemInfo(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
