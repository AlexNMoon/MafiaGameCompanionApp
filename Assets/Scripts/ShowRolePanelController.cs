using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowRolePanelController : PanelController
{
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text roleText;
    [SerializeField] private Button continueButton;
    
    private Color _mafiaColor = Color.red;
    private Color _citizenColor = Color.green;
    private string _roleString = "You are {0}";

    public void ShowRole(RolesEnum role)
    {
        roleText.text = String.Format(_roleString, role);
        roleImage.color = GetRolesColor(role);
    }

    private Color GetRolesColor(RolesEnum role)
    {
        switch (role)
        {
            case RolesEnum.Mafia:
                return _mafiaColor;
            case RolesEnum.Citizen:
                return _citizenColor;
        }
        
        return Color.gray;
    }

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnShowRoleContinueButtonClick);
    }

    private void OnShowRoleContinueButtonClick()
    {
        InvokeOpenNextPanel();
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(OnShowRoleContinueButtonClick);
    }
}
