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

    protected override void SubscribeEvents()
    {
        getRoleButton.onClick.AddListener(OnGetRoleButtonClick);
    }

    private void OnGetRoleButtonClick()
    {
        InvokeOpenNextPanel();
    }

    protected override void UnsubscribeEvents()
    {
        getRoleButton.onClick.RemoveListener(OnGetRoleButtonClick);
    }
}
