using System;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    [SerializeField] private FieldBuilder _fieldBuilder;
    [SerializeField] private ItemSpawner _itemSpawner;

    private string _key = "ItemInCell";

    private void OnEnable()
    {
        Item.Destroyed += OnItemDestroyed;
        ItemIntersection.CellChanged += OnCellChanged;
        _itemSpawner.ItemCreated += OnItemCreated;
    }

    private void OnDisable()
    {
        Item.Destroyed -= OnItemDestroyed;
        ItemIntersection.CellChanged -= OnCellChanged;
        _itemSpawner.ItemCreated -= OnItemCreated;
    }

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        for (int i = 0; i < _fieldBuilder.Cells.Count; i++)
        {
            if (PlayerPrefs.HasKey(_key + (i + 1)))
            {
                _itemSpawner.LoadingItemsSheet.Add((i + 1, PlayerPrefs.GetInt(_key + (i + 1))));
            }
        }
    }

    private void OnItemCreated(int cellIndex, int itemIndex)
    {
        CreateKey(cellIndex, itemIndex);
    }

    private void OnCellChanged(int oldCellIndex, int newCellIndex)
    {
        var itemIndex = PlayerPrefs.GetInt(_key + oldCellIndex);
        
        DeleteKey(oldCellIndex);
        CreateKey(newCellIndex, itemIndex);
    }

    private void OnItemDestroyed(int cellIndex)
    {
        DeleteKey(cellIndex);
    }

    private void CreateKey(int cellIndex, int itemIndex)
    {
        PlayerPrefs.SetInt(_key + cellIndex, itemIndex);
        //print("Created key " + key + cellIndex + " with value " + itemIndex);
    }

    private void DeleteKey(int cellIndex)
    {
        PlayerPrefs.DeleteKey(_key + cellIndex);
        //print("Deleted key " + key + cellIndex);
    }
}
