using UnityEngine;

public class ItemExample : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private int _index;
    [SerializeField] private bool _unlock;

    public int Price
    {
        get => _price;
        set => _price = value;
    }

    public int GetIndex() { return _index; }

    public string GetName() { return _name; }

    public bool Unlock
    {
        get => _unlock;
        set => _unlock = value;
    }
}
