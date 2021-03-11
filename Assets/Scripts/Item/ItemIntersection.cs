using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemIntersection : MonoBehaviour, IPointerUpHandler
{
    private ItemDrag _itemDrag;
    private Item _item;
    private BoxCollider2D _boxCollider2D;

    private Collider2D _otherItemCollider;

    private Item _otherItem;

    public static event UnityAction<int, Cell> Merged;
    public static event UnityAction MergeSuccess;
    public static event UnityAction MergeFailed;
    public static event UnityAction<int, int> CellChanged;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _item = GetComponent<Item>();
        _itemDrag = GetComponent<ItemDrag>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_otherItem != null)
        {
            if (_boxCollider2D.IsTouching(_otherItemCollider))
            {
                Merging();
            }
        }
    }

    

    private void OnTriggerEnter2D(Collider2D otherCollider2D)
    {
        if (_itemDrag.Active())
        {
            if (otherCollider2D.gameObject.TryGetComponent(out Item item))
            {
                _otherItem = item;
                _otherItemCollider = otherCollider2D;
            } 
            
            if (otherCollider2D.gameObject.TryGetComponent(out Cell cell))
            {
                if (cell.Available)
                {
                    ChangeCell(cell);
                }
            }
        }

        /*if (otherCollider2D.gameObject.TryGetComponent(out Item item))
        {
            Merge(otherCollider2D, item);
        }

        if (otherCollider2D.gameObject.TryGetComponent(out Cell cell))
        {
            if (cell.Available)
            {
                ChangeCell(cell);
            }
        }*/
    }

    private void ChangeCell(Cell cell)
    {
        var oldCellIndex = _item.Cell.Index;
        
        _item.Cell.Available = true;
        cell.Available = false;
        
        _item.Cell = cell;
        CellChanged?.Invoke(oldCellIndex, cell.Index);
    }

    private void Merging()
    {
        if (_otherItem.GetGrade() == _item.GetGrade())
        {
            var index = (int) _item.GetGrade();

            _item.Cell.Available = true;

            _otherItem.Destroy();
            _item.Destroy();
            
            Merged?.Invoke(index, _otherItem.Cell);
            MergeSuccess?.Invoke();
        }
        else
        {
            MergeFailed?.Invoke();
        }
    }
}
