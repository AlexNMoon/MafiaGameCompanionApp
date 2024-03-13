using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDistributionInstructionPanelController : PanelController
{
    [SerializeField] private Button continueButton;

    protected override void SubscribeEvents()
    {
        continueButton.onClick.AddListener(OnEndOfDistributionContinueButtonClick);
    }

    private void OnEndOfDistributionContinueButtonClick()
    {
        InvokeOpenNextPanel();
        /*endOfDistributionInstructionPanel.SetActive(false);

        foreach (var player in _rolesDistribution)
        {
            PlayerGamingItemController playerItem = Instantiate(playerGamingItemPrefab, Vector3.zero, Quaternion.identity, playerItemsParent);
            playerItem.Init(GetRolesColor(player.Value), player.Value, player.Key);
            playerItem.PlayerEliminated += OnPlayerEliminated;
        }
        
        gamePanel.SetActive(true);*/
    }

    protected override void UnsubscribeEvents()
    {
        continueButton.onClick.AddListener(OnEndOfDistributionContinueButtonClick);
    }
}
