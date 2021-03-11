using UnityEngine;

public class FormService : MonoBehaviour
{
    private static GameObject _currentForm;

    public static GameObject CurrentForm
    {
        get => _currentForm;
        set => _currentForm = value;
    }
}
