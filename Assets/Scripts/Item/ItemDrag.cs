using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(ItemIntersection))]
[RequireComponent(typeof(Item))]
public class ItemDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Canvas _canvas;
    
    private Item _item;
    private Coroutine _coroutine;
    private Camera _camera;
    private bool _isActive;

    private int _sortingOrder;

    private Vector3 _scaleAvailableItems = new Vector3(1.25f, 1.25f, 1.25f);

    private void Start()
    {
        _item = GetComponent<Item>();
        _camera = Camera.main;
    }

    // public Cell Cells
    // {
    //     get => _item.Cell;
    //     set => _item.Cell = value;
    // }

    public bool Active()
    {
        return _isActive;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isActive = true;

        _sortingOrder = _canvas.sortingOrder;
        _canvas.sortingOrder = 50;
        
        ShowAvailableItems();
        
        if (_coroutine != null) return;
        _coroutine = StartCoroutine(PositionChanger());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isActive = false;

        _canvas.sortingOrder = _sortingOrder;
        
        HideAvailableItems();
        
        SetPosition(_item.Cell.transform.position);
    }

    private IEnumerator PositionChanger()
    {
        while (_isActive)
        {
            DefinePosition();
            yield return new WaitForEndOfFrame();
        }
        
        StopPositionChanger();
    }

    private void StopPositionChanger()
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private void DefinePosition()
    {
        const float reverse = -1f;
        var distance = (_camera.transform.position.z - transform.position.z) * reverse;
        var mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        SetPosition(mousePosition);
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void ShowAvailableItems()
    {
        foreach (var item in _item.ItemSpawner.Items)
        {
            if (_item != item && _item.GetGrade() == item.GetGrade())
            {
                item.GetComponent<ItemAnimation>().SetScale(_scaleAvailableItems);
            }
        }
    }
    
    private void HideAvailableItems()
    {
        foreach (var item in _item.ItemSpawner.Items)
        {
            if (_item != item && _item.GetGrade() == item.GetGrade())
            {
                item.GetComponent<ItemAnimation>().SetDefaultScale();
            }
        }
    }
}
