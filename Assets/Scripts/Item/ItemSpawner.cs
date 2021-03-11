using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FieldBuilder))]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Box _boxTemplate;
    [SerializeField] private List<Item> _allItems;
    
    private FieldBuilder _fieldBuilder;
    private List<Item> _items;
    private List<Box> _boxes;

    private List<(int, int)> _loadingItemsSheet = new List<(int, int)>();
    private List<(int, int)> _loadingBoxesSheet = new List<(int, int)>(); 

    public event UnityAction<int> SuccessMerge;
    public event UnityAction FieldIsFull;
    public event UnityAction FieldHasEmptyCell;
    public event UnityAction<int, int> ItemCreated;
    public event UnityAction<int, int> BoxCreated;
    //public event UnityAction<Vector3> ItemRemoved;

    public List<Item> Items => _items;
    public List<Box> Boxes
    {
        get => _boxes;
        set => _boxes = value;
    }
    
    public List<(int, int)> LoadingItemsSheet
    {
        get => _loadingItemsSheet;
        set => _loadingItemsSheet = value;
    }

    public List<(int, int)> LoadingBoxesSheet
    {
        get => _loadingBoxesSheet;
        set => _loadingBoxesSheet = value;
    }

    private void OnEnable()
    {
        ItemIntersection.Merged += OnMerged;
    }

    private void OnDisable()
    {
        ItemIntersection.Merged -= OnMerged;
    }

    private void Awake()
    {
        //_loadingBoxesSheet = new List<(int, int)>();
        //_loadingItemsSheet = new List<(int, int)>();
        _items = new List<Item>();
        _boxes = new List<Box>();
        _fieldBuilder = GetComponent<FieldBuilder>();
    }

    private void Start()
    {
        if (_loadingBoxesSheet.Count > 0)
        {
            foreach (var (cellIndex, itemInBox) in _loadingBoxesSheet)
            {
                CreateBox(GetCellByIndex(cellIndex), GetItemByIndex(itemInBox));
            }
        } 
        
        if (_loadingItemsSheet.Count > 0)
        {
            foreach (var (cellIndex, itemIndex) in _loadingItemsSheet)
            {
                SpawnItem(GetCellByIndex(cellIndex), GetItemByIndex(itemIndex));
            }
        } else
        {
            SpawnBox();
        }
        
    }

    public void SpawnBox(int index = 1)
    {
        var cell = GetAvailableCell();

        if (cell != null)
        {
            CreateBox(cell, GetItemByIndex(index));
        }
        else
        {
            FieldIsFull?.Invoke();
        }
    }

    public void SpawnItem(Cell cell, Item item)
    {
        CreateItem(cell, item);
    }

    private void CreateBox(Cell cell, Item spawnItem)
    {
        var box = Instantiate(_boxTemplate, cell.transform.position, Quaternion.identity, transform);

        box.Item = spawnItem;
        box.Cell = cell;
        box.Cell.Available = false;
        box.ItemSpawner = this;
        
        _boxes.Add(box);
        
        BoxCreated?.Invoke(cell.Index, (int) spawnItem.GetGrade());
        
        UpdateCellsStatus();
    }

    private void CreateItem(Cell cell, Item spawnItem)
    {
        var item = Instantiate(spawnItem, cell.transform.position, Quaternion.identity, transform);
        
        item.Cell = cell;
        item.Cell.Available = false;
        item.ItemSpawner = this;
        
        _items.Add(item);
        UpdateCellsStatus();
        
        ItemCreated?.Invoke(cell.Index, (int) item.GetGrade());
    }

    private void UpdateCellsStatus()
    {
        if (!HasAvailableCell())
        {
            FieldIsFull?.Invoke();
        }
        else
        {
            FieldHasEmptyCell?.Invoke();
        }
    }
    
    private void OnMerged(int gradeLevel, Cell cell)
    {
        gradeLevel++;

        var item = GetItemByIndex(gradeLevel);
        if (item != null)
        {
            CreateItem(cell, item);

            SuccessMerge?.Invoke(item.GetProgress());
        }
        
        //UpdateCellsStatus();
    }
    
    public bool HasAvailableCell()
    {
        return GetAvailableCell() != null;
    }
    private Cell GetAvailableCell()
    {
        foreach (var cell in _fieldBuilder.Cells)
        {
            if (cell.Available)
            {
                return cell;
            }
        }

        return null;
    }

    private Item GetItemByIndex(int index)
    {
        foreach (var item in _allItems)
        {
            if ((int) item.GetGrade() == index)
            {
                return item;
            }
        }

        return null;
    }

    private Cell GetCellByIndex(int index)
    {
        foreach (var cell in _fieldBuilder.Cells)
        {
            if (cell.Index == index)
            {
                return cell;
            }
        }

        return null;
    }
}
