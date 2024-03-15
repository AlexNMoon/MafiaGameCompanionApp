using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterPlayerNamePanelController : PanelController
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button getRoleButton;
    [SerializeField] private GameObject nameErrorText;

    public string GetName()
    {
        return nameInputField.text;
    }

    public void ShowError()
    {
        nameErrorText.SetActive(true);
    }

    public override void OpenPanel()
    {
        nameInputField.text = "";
        nameErrorText.SetActive(false);
        base.OpenPanel();
    }

    protected override void SubscribeEvents()
    {
        getRoleButton.onClick.AddListener(OnGetRoleButtonClick);
    }

    private void OnGetRoleButtonClick()
    {
        if(nameInputField.text == "") return;
        
        InvokeOpenNextPanel();
    }

    protected override void UnsubscribeEvents()
    {
        getRoleButton.onClick.RemoveListener(OnGetRoleButtonClick);
    }
}
