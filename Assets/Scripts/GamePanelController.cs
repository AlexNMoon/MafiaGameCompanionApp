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
    
    private Color _mafiaColor = Color.red;
    private Color _citizenColor = Color.green;

    public void SetUpPlayers(Dictionary<string, RolesEnum> rolesDistribution)
    {
        foreach (var player in rolesDistribution)
        {
            PlayerGamingItemController playerItem = Instantiate(playerGamingItemPrefab, Vector3.zero, Quaternion.identity, playerItemsParent);
            playerItem.Init(GetRolesColor(player.Value), player.Value, player.Key);
            playerItem.PlayerEliminated += OnPlayerEliminated;
        }
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
            endGamePanel.SetActive(true);
        }
        else
        {
            endGameImage.color = _citizenColor;
            endGameText.text = "Citizens won!";
            endGamePanel.SetActive(true);
        }
    }
    
    protected override void SubscribeEvents()
    {
        
    }

    protected override void UnsubscribeEvents()
    {
        
    }
}
