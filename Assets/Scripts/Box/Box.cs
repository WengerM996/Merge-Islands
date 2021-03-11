using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IPointerClickHandler
{
    private Item _item;
    private Cell _cell;
    private ItemSpawner _itemSpawner;

    public static event UnityAction<int, Vector3> Unpacked;
    public static event UnityAction<int> Destroyed;


    public ItemSpawner ItemSpawner
    {
        get => _itemSpawner;
        set => _itemSpawner = value;
    }

    public Cell Cell
    {
        get => _cell;
        set => _cell = value;
    }

    public Item Item
    {
        get => _item;
        set => _item = value;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Unpack();
    }

    private void Unpack()
    {
        _itemSpawner.SpawnItem(_cell, _item);
        
        Unpacked?.Invoke(_cell.Index, _cell.transform.position);
        Destroyed?.Invoke(_cell.Index);
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _itemSpawner.Boxes.Remove(this);
    }
}
