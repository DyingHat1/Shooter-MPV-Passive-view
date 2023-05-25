using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private List<Cell> _cells;

    public int CellsCount => _cells.Count;

    public event Action<Item, bool> ItemCollected;
    public event Action Shot;

    public Inventory(List<Cell> cells) 
    {
        _cells = cells;
    }

    public void DropItemFromCell(int cellNumber, Vector2 position)
    {
        _cells[cellNumber].Drop(position);
    }

    public void CollectItem(Item item)
    {
        ItemCollected?.Invoke(item, TryCollectToStack(item));
    }

    public bool HasAmmo()
    {
        foreach (Cell cell in _cells)
        {
            if (cell.Name == Config.AmmoItemName)
                return true;
        }

        return false;
    }

    public void Shoot()
    {
        Cell cell = _cells.First(p => p.Name == Config.AmmoItemName);
        cell.SpendItem();
        Shot?.Invoke();
    }

    public KeyValuePair<string,int> GetInfoFromCell(int id)
    {
        return new KeyValuePair<string, int>(_cells[id].Name, _cells[id].Count);
    }

    private void AddToFreeSlot(Item item)
    {
        Cell emptyCell = _cells.First(p => p.Name == Config.EmptyItemSlotName);
        emptyCell.AddItem(item);
    }

    private bool TryCollectToStack(Item item)
    {
        foreach(Cell cell in _cells)
        {
            if (cell.Name == item.Name)
            {
                cell.AddItem(item);
                return true;
            }
        }

        AddToFreeSlot(item);
        return false;
    }
}
