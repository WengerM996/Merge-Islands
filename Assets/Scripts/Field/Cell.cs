using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private bool _isEmpty;
    [SerializeField] private bool _unlock;

    public bool Available
    {
        get => _isEmpty;
        set => _isEmpty = value;
    }

    public bool Unlock
    {
        get => _unlock;
        set => _unlock = value;
    }

    public int Index => _index;
}
