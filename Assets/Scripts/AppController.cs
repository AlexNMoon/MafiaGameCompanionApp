using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] private StartMenuPanelController startMenuPanelController;
    [SerializeField] private GameRulesPanelController gameRulesPanelController;
    [SerializeField] private GameController gameController;

    private void Awake()
    {
        startMenuPanelController.StartGame += StartGame;
        gameController.OpenMainMenu += OpenMainManu;
        startMenuPanelController.OpenGameRules += OpenGameRules;
        gameRulesPanelController.OpenPreviousPanel += CloseGameRules;
    }

    private void StartGame()
    {
        startMenuPanelController.ClosePanel();
        gameController.StartNewGame();
    }

    private void OpenMainManu()
    {
        startMenuPanelController.OpenPanel();
    }

    private void OpenGameRules()
    {
        startMenuPanelController.ClosePanel();
        gameRulesPanelController.OpenPanel();
    }

    private void CloseGameRules()
    {
        gameRulesPanelController.ClosePanel();
        startMenuPanelController.OpenPanel();
    }

    private void Start()
    {
        OpenMainManu();
    }
}
