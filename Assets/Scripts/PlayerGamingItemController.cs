using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGamingItemController : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button actionButton;

    private RolesEnum _role;

    public void Init(Color iconColor, RolesEnum role, string name)
    {
        _role = role;
        icon.color = iconColor;
        nameText.text = name;
    }
}
