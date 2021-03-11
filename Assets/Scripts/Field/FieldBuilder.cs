using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldBuilder : MonoBehaviour
{
    [SerializeField] private LevelSystem _levelSystem;
    [SerializeField] private List<Cell> _cells = new List<Cell>();

    public event UnityAction<int> UnlockedNewCell;
    //public event UnityAction CellsUpdated;

    private void OnEnable()
    {
        _levelSystem.ReachedNextLevel += OnReachedNextLevel;
    }

    private void OnDisable()
    {
        _levelSystem.ReachedNextLevel -= OnReachedNextLevel;
    }

    private void Start()
    {
        Build();
    }

    public List<Cell> Cells => _cells;

    private void Build()
    {
        foreach (var cell in _cells)
        {
            var cellRenderer = cell.gameObject.GetComponent<SpriteRenderer>();
            
            if (cell.Unlock)
            {
                cellRenderer.enabled = true;
                cell.Available = true;
            }
            else
            {
                cellRenderer.enabled = false;
                cell.Available = false;
            }
        }
        
        //CellsUpdated?.Invoke();
    }

    private void UnlockCell()
    {
        foreach (var cell in _cells)
        {
            if (!cell.Unlock)
            {
                var cellRenderer = cell.gameObject.GetComponent<SpriteRenderer>();
                cellRenderer.enabled = true;
                
                cell.Unlock = true;
                cell.Available = true;
                
                UnlockedNewCell?.Invoke(cell.Index);
                //CellsUpdated?.Invoke();
                
                break;
            }
        }
    }

    private void OnReachedNextLevel()
    {
        UnlockCell();
    }
}
