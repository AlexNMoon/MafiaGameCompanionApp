using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public event Action OpenMainMenu;

    [SerializeField] private RolesDistributionController rolesDistributionController;
    [SerializeField] private GamePanelController gamePanelController;

    private int _numberOfMafia;
    private int _numberOfCitizens;

    public void StartNewGame()
    {
        rolesDistributionController.StartDistributionProcess();
    }

    private void Awake()
    {
        rolesDistributionController.OpenMainMenu += GoBackToMainMenu;
        rolesDistributionController.DistributionFinished += OnDistributionFinished;
        gamePanelController.PlayerEliminated += OnPlayerEliminated;
        gamePanelController.ContinueButtonClick += OnGameFinished;
    }
    
    private void GoBackToMainMenu()
    {
        OpenMainMenu?.Invoke();
    }

    private void OnDistributionFinished(int citizens, int mafia, Dictionary<string, RolesEnum> roles)
    {
        _numberOfCitizens = citizens;
        _numberOfMafia = mafia;
        gamePanelController.SetUpPlayers(roles);
        gamePanelController.OpenPanel();
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

    private void OnGameFinished()
    {
        gamePanelController.ClosePanel();
        GoBackToMainMenu();
    }
}