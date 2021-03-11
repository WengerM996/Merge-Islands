using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Shop))]
public class Unlock : MonoBehaviour
{
    [SerializeField] private int _eachLevel;
    [SerializeField] private LevelSystem _levelSystem;

    private Shop _shop;

    public event UnityAction<int> UnlockedNewItem;

    private void Start()
    {
        _shop = GetComponent<Shop>();
    }

    private void OnEnable()
    {
        _levelSystem.ReachedNextLevel += OnReachedNextLevel;
    }

    private void OnDisable()
    {
        _levelSystem.ReachedNextLevel -= OnReachedNextLevel;
    }

    private void OnReachedNextLevel()
    {
        if (_levelSystem.Level % _eachLevel == 0)
        {
            foreach (var item in _shop.ItemsForSell)
            {
                if (!item.Unlock)
                {
                    item.Unlock = true;
                    UnlockedNewItem?.Invoke(item.GetIndex());
                    break;
                }
            }
        }
    }
}
