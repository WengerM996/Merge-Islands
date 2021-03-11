using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private SettingsForm _formTemplate;

    //private SettingsForm _form;
    
    public void OnOpen()
    {
        if (FormService.CurrentForm != null)
        {
            Destroy(FormService.CurrentForm);
        }
        
        FormService.CurrentForm = Instantiate(_formTemplate, transform.position, Quaternion.identity, transform).gameObject;
    }
}
