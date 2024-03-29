using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RolesAssigmentController : MonoBehaviour
{
    public event Action<Dictionary<string, RolesEnum>> AssigmentFinished;
    
    [SerializeField] private PlayerInstructionPanelController playerInstructionPanelController;
    [SerializeField] private EnterPlayerNamePanelController enterPlayerNamePanelController;
    [SerializeField] private ShowRolePanelController showRolePanelController;
    
    private int _currentPlayerAssigment;
    private List<RolesEnum> _availableRoles;
    private Dictionary<string, RolesEnum> _rolesAssigment;
    private int _numberOfPlayers;

    public void StartRolesAssigment(List<RolesEnum> roles)
    {
        _availableRoles = roles;
        _numberOfPlayers = _availableRoles.Count;
        _rolesAssigment = new Dictionary<string, RolesEnum>();
        _currentPlayerAssigment = 1;
        ShowPlayerInstructionPanel();
    }

    private void ShowPlayerInstructionPanel()
    {
        playerInstructionPanelController.SetCurrentPlayerIndex(_currentPlayerAssigment);
        playerInstructionPanelController.OpenPanel();
    }

    private void Awake()
    {
        playerInstructionPanelController.ContinueButtonClick += OpenEnterPlayerNamePanel;
        enterPlayerNamePanelController.ContinueButtonClick += GenerateRole;
        showRolePanelController.ContinueButtonClick += ContinueRolesDistribution;
    }

    private void OpenEnterPlayerNamePanel()
    {
        playerInstructionPanelController.ClosePanel();
        enterPlayerNamePanelController.OpenPanel();
    }
    
    private void GenerateRole()
    {
        if(_rolesAssigment.ContainsKey(enterPlayerNamePanelController.GetName()))
        {
            enterPlayerNamePanelController.ShowError();
            return;
        }
        
        int i = Random.Range(0, _availableRoles.Count);
        RolesEnum role = _availableRoles[i];
        _availableRoles.RemoveAt(i);
        _rolesAssigment.Add(enterPlayerNamePanelController.GetName(), role);
        showRolePanelController.ShowRole(role);
        enterPlayerNamePanelController.ClosePanel();
        showRolePanelController.OpenPanel();
    }

    private void ContinueRolesDistribution()
    {
        _currentPlayerAssigment++;
        showRolePanelController.ClosePanel();

        if (_currentPlayerAssigment <= _numberOfPlayers)
        {
            ShowPlayerInstructionPanel();
        }
        else
        {
            AssigmentFinished?.Invoke(_rolesAssigment);
        }
    }
}
