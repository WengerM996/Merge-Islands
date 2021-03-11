using UnityEngine;
using UnityEngine.Events;

public class ShopForm : MonoBehaviour
{
    [SerializeField] private Transform _scrollView;
    
    //public static event UnityAction ShopClosed;

    public void Close()
    {
        //ShopClosed?.Invoke();
        Destroy(gameObject);
    }

    public Transform ScrollView
    {
        get => _scrollView;
    }
}
