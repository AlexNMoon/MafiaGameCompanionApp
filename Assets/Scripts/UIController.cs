using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    //start menu elements
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private Button newGameButton;
    
    //create team elements
    [SerializeField] private GameObject createTeamPanel;
    //enter number of players 
    [SerializeField] private GameObject enterNumberOfPlayersPanel;
    [SerializeField] private TMP_InputField numberOfPlayersTMPInputField;
    [SerializeField] private Button confirmNumberOfPlayersButton;
    //show roles distribution 
    [SerializeField] private GameObject showRolesDistributionPanel;
    [SerializeField] private TMP_Text mafiaAmountText;
    [SerializeField] private TMP_Text citizensAmountText;

    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;

    private void Awake()
    {
        newGameButton.onClick.AddListener(OnStartGameButtonClick);
        confirmNumberOfPlayersButton.onClick.AddListener(OnConfirmNumberOfPlayersButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        startMenuPanel.SetActive(false);
        createTeamPanel.SetActive(true);
    }

    private void OnConfirmNumberOfPlayersButtonClick()
    {
        if (numberOfPlayersTMPInputField.text == "") return;
        
        _numberOfPlayers =  Convert.ToInt32(numberOfPlayersTMPInputField.text);
            
        if (_numberOfPlayers >= 3)
        {
            enterNumberOfPlayersPanel.SetActive(false);
            _numberOfMafia = Mathf.FloorToInt(_numberOfPlayers / 2);
            _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
            mafiaAmountText.text = _numberOfMafia.ToString();
            citizensAmountText.text = _numberOfCitizens.ToString();
            showRolesDistributionPanel.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}