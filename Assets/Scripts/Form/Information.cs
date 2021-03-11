using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    [SerializeField] private List<Sprite> _allImages;
    [SerializeField] private Form _form;
    [Space] 
    [SerializeField] private FieldBuilder _fieldBuilder;
    [SerializeField] private Unlock _unlock;
    

    private void OnEnable()
    {
        _fieldBuilder.UnlockedNewCell += OnUnlockedNewCell;
        _unlock.UnlockedNewItem += OnUnlockedNewItem;
    }

    private void OnDisable()
    {
        _fieldBuilder.UnlockedNewCell -= OnUnlockedNewCell;
        _unlock.UnlockedNewItem -= OnUnlockedNewItem;
    }

    private void OnUnlockedNewCell(int cellIndex)
    {
        CreateForm(_form, _allImages[0], "New land has been discovered!", 6, 7);
    }

    private void OnUnlockedNewItem(int itemIndex)
    {
        CreateForm(_form, _allImages[itemIndex], "Unlocked in the shop!", 5, 6);
    }

    private void CreateForm(Form formTemplate, Sprite logo, string text, int spriteOrder, int canvasOrder)
    {
        //var form = Instantiate(formTemplate, transform.position, Quaternion.identity, transform);
        var form = Instantiate(formTemplate, transform);
        form.SetText(text);
        form.SetLayouts(spriteOrder, canvasOrder);
        form.SetLogo(logo);
    }
}
