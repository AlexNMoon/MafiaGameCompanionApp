using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : PanelController
{
    public event Action<RolesEnum> PlayerEliminated;
    
    [SerializeField] private PlayerGamingItemController playerGamingItemPrefab;
    [SerializeField] private Transform playerItemsParent;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Image endGameImage;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private Button finishGameButton;
    
    private Color _mafiaColor = Color.red;
    private Color _citizenColor = Color.green;

    private List<PlayerGamingItemController> _playersGamingItems = new List<PlayerGamingItemController>();

    public void SetUpPlayers(Dictionary<string, RolesEnum> rolesDistribution)
    {
        foreach (var player in rolesDistribution)
        {
            PlayerGamingItemController playerItem = GetPlayerGamingItem();
            playerItem.SetUp(GetRolesColor(player.Value), player.Value, player.Key);
        }
    }

    private PlayerGamingItemController GetPlayerGamingItem()
    {
        PlayerGamingItemController playerItem = _playersGamingItems.Find(x => x.gameObject.activeSelf == false);

        if (playerItem == null)
        {
            playerItem = Instantiate(playerGamingItemPrefab, Vector3.zero, Quaternion.identity, playerItemsParent);
            playerItem.PlayerEliminated += OnPlayerEliminated;
            _playersGamingItems.Add(playerItem);
        }

        return playerItem;
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

    private void OnPlayerEliminated(RolesEnum role)
    {
        PlayerEliminated?.Invoke(role);
    }
    
    public void ShowGameEnd(RolesEnum winner)
    {
        if (winner == RolesEnum.Mafia)
        {
            endGameImage.color = _mafiaColor;
            endGameText.text = "Mafia won!";
        }
        else
        {
            endGameImage.color = _citizenColor;
            endGameText.text = "Citizens won!";
        }
        
        endGamePanel.SetActive(true);

        for (int i = 0; i < _playersGamingItems.Count; i++)
        {
            _playersGamingItems[i].HideItem();
        }
    }
    
    protected override void SubscribeEvents()
    {
        finishGameButton.onClick.AddListener(OnFinishGameButtonClick);
    }

    private void OnFinishGameButtonClick()
    {
        endGamePanel.SetActive(false);
        InvokeContinueButtonClick();
    }

    protected override void UnsubscribeEvents()
    {
        finishGameButton.onClick.RemoveListener(OnFinishGameButtonClick);
    }
}
