using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolesDistributionController : MonoBehaviour
{
    public event Action OpenMainMenu;
    public event Action<int, int, Dictionary<string, RolesEnum>> DistributionFinished;

    [SerializeField] private EnterNumberOfPlayersPanelController enterNumberOfPlayersPanelController;
    [SerializeField] private RolesDistributionPanelController rolesDistributionPanelController;
    [SerializeField] private CreateTeamInstructionPanelController createTeamInstructionPanelController;
    [SerializeField] private RolesAssigmentController rolesAssigmentController;
    [SerializeField] private EndOfDistributionInstructionPanelController endOfDistributionInstructionPanelController;

    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    
    private Dictionary<string, RolesEnum> _rolesAssigment;

    public void StartDistributionProcess()
    {
        enterNumberOfPlayersPanelController.OpenPanel();
    }

    private void Awake()
    {
        enterNumberOfPlayersPanelController.ContinueButtonClick += CalculateRolesDistribution;
        enterNumberOfPlayersPanelController.BackButtonClick += GoBackToMainMenu;
        
        rolesDistributionPanelController.ContinueButtonClick += ShowCreateTeamInstructions;
        rolesDistributionPanelController.BackButtonClick += GoBackToEnterNumberOfPlayers;
        
        createTeamInstructionPanelController.ContinueButtonClick += StartRolesAssigment;
        createTeamInstructionPanelController.BackButtonClick += GoBackToRolesDistribution;
        
        rolesAssigmentController.AssigmentFinished += OnAssigmentFinished;
        
        endOfDistributionInstructionPanelController.ContinueButtonClick += FinishDistribution;
    }
    
    private void CalculateRolesDistribution()
    {
        _numberOfPlayers = enterNumberOfPlayersPanelController.GetNumberOfPlayer();
        
        if (_numberOfPlayers < 3)
        {
            enterNumberOfPlayersPanelController.ShowError();
            return;
        }
    
        _numberOfMafia = Mathf.RoundToInt((float)(_numberOfPlayers * 28.0 / 100.0));
        _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
        rolesDistributionPanelController.SetUpRoles(_numberOfMafia, _numberOfCitizens);
        enterNumberOfPlayersPanelController.ClosePanel();
        rolesDistributionPanelController.OpenPanel();
    }

    private void GoBackToMainMenu()
    {
        enterNumberOfPlayersPanelController.ClosePanel();
        OpenMainMenu?.Invoke();
    }

    private void ShowCreateTeamInstructions()
    {
        rolesDistributionPanelController.ClosePanel();
        createTeamInstructionPanelController.OpenPanel();
    }

    private void GoBackToEnterNumberOfPlayers()
    {
        rolesDistributionPanelController.ClosePanel();
        enterNumberOfPlayersPanelController.OpenPanel();
    }

    private void StartRolesAssigment()
    {
        createTeamInstructionPanelController.ClosePanel();
        rolesAssigmentController.StartRolesAssigment(CreateListOfAvailableRoles());
    }

    private List<RolesEnum> CreateListOfAvailableRoles()
    {
        List<RolesEnum> availableRoles = new List<RolesEnum>();

        for (int i = 0; i < _numberOfMafia; i++)
        {
            availableRoles.Add(RolesEnum.Mafia);
        }

        for (int i = 0; i < _numberOfCitizens; i++)
        {
            availableRoles.Add(RolesEnum.Citizen);
        }

        return availableRoles;
    }

    private void GoBackToRolesDistribution()
    {
        createTeamInstructionPanelController.ClosePanel();
        rolesDistributionPanelController.OpenPanel();
    }

    private void OnAssigmentFinished(Dictionary<string, RolesEnum> distribution)
    {
        _rolesAssigment = distribution;
        endOfDistributionInstructionPanelController.OpenPanel();
    }

    private void FinishDistribution()
    {
        endOfDistributionInstructionPanelController.ClosePanel();
        DistributionFinished?.Invoke(_numberOfCitizens, _numberOfMafia, _rolesAssigment);
    }
}
