using Facebook.Unity;
using UnityEngine;
using UnityEngine.Events;

public class FormDialog : MonoBehaviour
{
    public static event UnityAction UserWantsToDoubleIncome;

    public void OnClickYes()
    {
        print("YES");
        UserWantsToDoubleIncome?.Invoke();
        OnClose();
    }
    
    public void OnClose()
    {
        Destroy(gameObject);
    }
}
