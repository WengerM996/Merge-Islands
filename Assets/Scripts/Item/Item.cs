using System;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public enum Grade
    {
        Level_1 = 1, Level_2 = 2, Level_3 = 3, Level_4 = 4, Level_5 = 5,
        Level_6 = 6, Level_7 = 7, Level_8 = 8, Level_9 = 9, Level_10 = 10,
        Level_11 = 11, Level_12 = 12, Level_13 = 13, Level_14 = 14, Level_15 = 15,
        Level_16 = 16, Level_17 = 17
    }

    [SerializeField] private Grade grade;
    [SerializeField] private int _progressAfterMerge;

    private Cell _cell;
    private ItemSpawner _itemSpawner;

    public static event UnityAction<int> Destroyed;


    public Cell Cell
    {
        get => _cell;
        set => _cell = value;
    }

    public ItemSpawner ItemSpawner
    {
        get => _itemSpawner;
        set => _itemSpawner = value;
    }

    public Grade GetGrade() { return grade; }

    public int GetProgress() { return _progressAfterMerge; }

    public void Destroy()
    {
        _itemSpawner.Items.Remove(this);
        Destroyed?.Invoke(_cell.Index);
        Destroy(gameObject);
    }
}
