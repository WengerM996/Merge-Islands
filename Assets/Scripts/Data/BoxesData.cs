using System.Collections.Generic;
using UnityEngine;

public class BoxesData : MonoBehaviour
{
    [SerializeField] private FieldBuilder _fieldBuilder;
    [SerializeField] private ItemSpawner _itemSpawner;

    private string _key = "BoxInCell";

    private void OnEnable()
    {
        Box.Destroyed += OnBoxDestroyed;
        _itemSpawner.BoxCreated += OnBoxCreated;
    }

    private void OnDisable()
    {
        Box.Destroyed -= OnBoxDestroyed;
        _itemSpawner.BoxCreated -= OnBoxCreated;
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
                _itemSpawner.LoadingBoxesSheet.Add((i + 1, PlayerPrefs.GetInt(_key + (i + 1))));
            }
        }
    }

    private void OnBoxCreated(int cellIndex, int itemIndex)
    {
        CreateKey(cellIndex, itemIndex);
    }

    private void OnBoxDestroyed(int cellIndex)
    {
        DeleteKey(cellIndex);
    }

    private void CreateKey(int cellIndex, int itemIndex)
    {
        PlayerPrefs.SetInt(_key + cellIndex, itemIndex);
    }

    private void DeleteKey(int cellIndex)
    {
        PlayerPrefs.DeleteKey(_key + cellIndex);
    }
}
