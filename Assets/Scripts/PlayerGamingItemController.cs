using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGamingItemController : MonoBehaviour
{
    public event Action<RolesEnum> PlayerEliminated;

    [SerializeField] private GameObject itemObject;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button actionButton;

    private RolesEnum _role;

    public void SetUp(Color iconColor, RolesEnum role, string name)
    {
        _role = role;
        icon.color = iconColor;
        nameText.text = name;
        itemObject.SetActive(true);
    }

    public void HideItem()
    {
        itemObject.SetActive(false);
        actionButton.interactable = true;
    }

    private void Awake()
    {
        actionButton.onClick.AddListener(OnActionButtonClick);
    }

    private void OnActionButtonClick()
    {
        actionButton.interactable = false;
        PlayerEliminated?.Invoke(_role);
    }
}
