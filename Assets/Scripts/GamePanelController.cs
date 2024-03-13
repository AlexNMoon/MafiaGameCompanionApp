using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : PanelController
{
    [SerializeField] private PlayerGamingItemController playerGamingItemPrefab;
    [SerializeField] private Transform playerItemsParent;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Image endGameImage;
    [SerializeField] private TMP_Text endGameText;

    protected override void SubscribeEvents()
    {
        
    }

    protected override void UnsubscribeEvents()
    {
        
    }
}
