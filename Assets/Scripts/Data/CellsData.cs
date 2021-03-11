using System;
using UnityEngine;

public class CellsData : MonoBehaviour
{
    [SerializeField] private FieldBuilder _fieldBuilder;

    private string _key = "Cell";
    
    private void Awake()
    {
        Load();
    }

    private void OnEnable()
    {
        _fieldBuilder.UnlockedNewCell += OnUnlockedNewCell;
    }

    private void OnDisable()
    {
        _fieldBuilder.UnlockedNewCell -= OnUnlockedNewCell;
    }

    private void Load()
    {
        foreach (var cell in _fieldBuilder.Cells)
        {
            if (PlayerPrefs.HasKey(_key + cell.Index))
            {
                cell.Unlock = Convert.ToBoolean(PlayerPrefs.GetInt(_key + cell.Index));
            }
        }
        
        // for (int i = 0; i < _fieldBuilder.Cells.Count; i++)
        // {
        //     if (PlayerPrefs.HasKey(key + (i + 1)))
        //     {
        //         _fieldBuilder.Cells[i].Unlock = Convert.ToBoolean(PlayerPrefs.GetInt(key + (i + 1)));
        //     }
        // }
    }

    private void OnUnlockedNewCell(int cellIndex)
    {
        Save(cellIndex, 1);
    }

    private void Save(int cellIndex, int unlockStatus)
    {
        PlayerPrefs.SetInt(_key + cellIndex, unlockStatus);
    }
}
