using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RolesDistributionPanelController : PanelController
{
    public event Action<bool, RolesEnum> ChangeRoleAmmount;

    [SerializeField] private TMP_Text mafiaAmountText;
    [SerializeField] private TMP_Text citizensAmountText;
    [SerializeField] private Button addMafia;
    [SerializeField] private Button removeMafia;
    [SerializeField] private Button addCitizen;
    [SerializeField] private Button removeCitizen;
    [SerializeField] private Button backButton;
    [SerializeField] private Button continueButton;

    public void SetUpRoles(int mafia, int citizens)
    {
        mafiaAmountText.text = mafia.ToString();
        citizensAmountText.text = citizens.ToString();
    }

    public void SetContinueButtonInteractivity(bool isInteractable)
    {
        continueButton.interactable = isInteractable;
    }

    public void SetChangeRolesAmountButtonsInteractivity(bool isAddMafiaInteractable, bool isRemoveMafiaInteractable,
        bool isAddCitizenInteractable, bool isRemoveCitizenInteractable)
    {
        addMafia.interactable = isAddMafiaInteractable;
        removeMafia.interactable = isRemoveMafiaInteractable;
        addCitizen.interactable = isAddCitizenInteractable;
        removeCitizen.interactable = isRemoveCitizenInteractable;
    }

    protected override void SubscribeEvents()
    {
        addMafia.onClick.AddListener(OnAddMafiaButtonClick);
        removeMafia.onClick.AddListener(OnRemoveMafiaButtonClick);
        addCitizen.onClick.AddListener(OnAddCitizenButtonClick);
        removeCitizen.onClick.AddListener(OnRemoveCitizenButtonClick);
        continueButton.onClick.AddListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.AddListener(OnShowRolesDistributionBackButtonClick);
    }

    private void OnAddMafiaButtonClick()
    {
        ChangeRoleAmmount?.Invoke(true, RolesEnum.Mafia);
    }

    private void OnRemoveMafiaButtonClick()
    {
        ChangeRoleAmmount?.Invoke(false, RolesEnum.Mafia);
    }

    private void OnAddCitizenButtonClick()
    {
        ChangeRoleAmmount?.Invoke(true, RolesEnum.Citizen);
    }

    private void OnRemoveCitizenButtonClick()
    {
        ChangeRoleAmmount?.Invoke(false, RolesEnum.Citizen);
    }

    private void OnShowRolesDistributionContinueButtonClick()
    {
        InvokeContinueButtonClick();
    }

    private void OnShowRolesDistributionBackButtonClick()
    {
        InvokeBackButtonClick();
    }

    protected override void UnsubscribeEvents()
    {
        addMafia.onClick.RemoveListener(OnAddMafiaButtonClick);
        removeMafia.onClick.RemoveListener(OnRemoveMafiaButtonClick);
        addCitizen.onClick.RemoveListener(OnAddCitizenButtonClick);
        removeCitizen.onClick.RemoveListener(OnRemoveCitizenButtonClick);
        continueButton.onClick.RemoveListener(OnShowRolesDistributionContinueButtonClick);
        backButton.onClick.RemoveListener(OnShowRolesDistributionBackButtonClick);
    }
}
