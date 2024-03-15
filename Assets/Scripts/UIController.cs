using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    [SerializeField] private StartMenuPanelController startMenuPanelController;
    [SerializeField] private GameRulesPanelController gameRulesPanelController;
    [SerializeField] private EnterNumberOfPlayersPanelController enterNumberOfPlayersPanelController;
    [SerializeField] private RolesDistributionPanelController rolesDistributionPanelController;
    [SerializeField] private CreateTeamInstructionPanelController createTeamInstructionPanelController;
    [SerializeField] private PlayerInstructionPanelController playerInstructionPanelController;
    [SerializeField] private EnterPlayerNamePanelController enterPlayerNamePanelController;
    [SerializeField] private ShowRolePanelController showRolePanelController;
    [SerializeField] private EndOfDistributionInstructionPanelController endOfDistributionInstructionPanelController;
    [SerializeField] private GamePanelController gamePanelController;

    private PanelController _currentPanel;
    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    private int _currentPlayerAssigment;
    private List<RolesEnum> _availableRoles;
    private Dictionary<string, RolesEnum> _rolesDistribution;

    private void Awake()
    {
        startMenuPanelController.StartGame += StartGame;
        startMenuPanelController.OpenGameRules += OpenGameRules;
        gameRulesPanelController.OpenPreviousPanel += OpenPreviousPanel;
        enterNumberOfPlayersPanelController.OpenNextPanel += OpenNextPanel;
        enterNumberOfPlayersPanelController.OpenPreviousPanel += OpenPreviousPanel;
        rolesDistributionPanelController.OpenNextPanel += OpenNextPanel;
        rolesDistributionPanelController.OpenPreviousPanel += OpenPreviousPanel;
        createTeamInstructionPanelController.OpenNextPanel += OpenNextPanel;
        createTeamInstructionPanelController.OpenPreviousPanel += OpenPreviousPanel;
        playerInstructionPanelController.OpenNextPanel += OpenNextPanel;
        enterPlayerNamePanelController.OpenNextPanel += OpenNextPanel;
        showRolePanelController.OpenNextPanel += OpenNextPanel;
        endOfDistributionInstructionPanelController.OpenNextPanel += OpenNextPanel;
        gamePanelController.PlayerEliminated += OnPlayerEliminated;
    }

    private void StartGame()
    {
        ChangePanel(enterNumberOfPlayersPanelController);
    }

    private void ChangePanel(PanelController nextPanel)
    {
        _currentPanel.ClosePanel();
        _currentPanel = nextPanel;
        _currentPanel.OpenPanel();
    }

    private void OpenGameRules()
    {
        ChangePanel(gameRulesPanelController);
    }

    private void Start()
    {
        _currentPanel = startMenuPanelController;
        _currentPanel.OpenPanel();
    }

    private void OpenNextPanel()
    {
        switch (_currentPanel)
        {
            case EnterNumberOfPlayersPanelController:
                CalculateRolesDistribution();
                break;
            case RolesDistributionPanelController:
                ChangePanel(createTeamInstructionPanelController);
                break;
            case CreateTeamInstructionPanelController:
                StartRolesDistribution();
                break;
            case PlayerInstructionPanelController:
                ChangePanel(enterPlayerNamePanelController);
                break;
            case EnterPlayerNamePanelController:
                GenerateRole();
                break;
            case ShowRolePanelController:
                ContinueRolesDistribution();
                break;
            case EndOfDistributionInstructionPanelController:
                ShowGamePanel();
                break;
            case GamePanelController:
                ChangePanel(startMenuPanelController);
                break;
        }
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
        ChangePanel(rolesDistributionPanelController);
    }

    private void StartRolesDistribution()
    {
        _rolesDistribution = new Dictionary<string, RolesEnum>();
        CreateListOfAvailableRoles();
        _currentPlayerAssigment = 1;
        ShowPlayerInstructionPanel();
    }

    private void CreateListOfAvailableRoles()
    {
        _availableRoles = new List<RolesEnum>();

        for (int i = 0; i < _numberOfMafia; i++)
        {
            _availableRoles.Add(RolesEnum.Mafia);
        }

        for (int i = 0; i < _numberOfCitizens; i++)
        {
            _availableRoles.Add(RolesEnum.Citizen);
        }
    }

    private void ShowPlayerInstructionPanel()
    {
        playerInstructionPanelController.SetCurrentPlayerIndex(_currentPlayerAssigment);
        ChangePanel(playerInstructionPanelController);
    }
    
    private void GenerateRole()
    {
        if(_rolesDistribution.ContainsKey(enterPlayerNamePanelController.GetName()))
        {
            enterPlayerNamePanelController.ShowError();
            return;
        }
        
        int i = Random.Range(0, _availableRoles.Count);
        RolesEnum role = _availableRoles[i];
        _availableRoles.RemoveAt(i);
        _rolesDistribution.Add(enterPlayerNamePanelController.GetName(), role);
        showRolePanelController.ShowRole(role);
        ChangePanel(showRolePanelController);
    }

    private void ContinueRolesDistribution()
    {
        _currentPlayerAssigment++;

        if (_currentPlayerAssigment <= _numberOfPlayers)
        {
            ShowPlayerInstructionPanel();
        }
        else
        {
            ChangePanel(endOfDistributionInstructionPanelController);
        }
    }

    private void ShowGamePanel()
    {
        gamePanelController.SetUpPlayers(_rolesDistribution);
        ChangePanel(gamePanelController);
    }

    private void OpenPreviousPanel()
    {
        switch (_currentPanel)
        {
            case GameRulesPanelController:
                ChangePanel(startMenuPanelController);
                break;
            case EnterNumberOfPlayersPanelController:
                ChangePanel(startMenuPanelController);
                break;
            case RolesDistributionPanelController:
                ChangePanel(enterNumberOfPlayersPanelController);
                break;
            case CreateTeamInstructionPanelController:
                ChangePanel(rolesDistributionPanelController);
                break;
            case GamePanelController:
                ChangePanel(startMenuPanelController);
                break;
        }
    }

    private void OnPlayerEliminated(RolesEnum playerRole)
    {
        switch (playerRole)
        {
            case RolesEnum.Mafia:
                _numberOfMafia--;
                break;
            case RolesEnum.Citizen:
                _numberOfCitizens--;
                break;
        }

        if (_numberOfMafia == 0)
        {
            gamePanelController.ShowGameEnd(RolesEnum.Citizen);
        }
        else if (_numberOfCitizens <= _numberOfMafia)
        {
            gamePanelController.ShowGameEnd(RolesEnum.Mafia);
        }
    }
}