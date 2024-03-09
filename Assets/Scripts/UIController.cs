using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    //start menu elements
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button showRulesButton;
    
    //game rules elements
    [SerializeField] private GameObject gameRulesPanel;
    [SerializeField] private Button gameRulesBackButton;
    
    //enter number of players elements
    [SerializeField] private GameObject enterNumberOfPlayersPanel;
    [SerializeField] private TMP_InputField numberOfPlayersTMPInputField;
    [SerializeField] private Button confirmNumberOfPlayersButton;
    [SerializeField] private Button enterNumberOfPlayersBackButton;
    [SerializeField] private GameObject numberOfPlayersErrorText;
    
    //show roles distribution elements
    [SerializeField] private GameObject showRolesDistributionPanel;
    [SerializeField] private TMP_Text mafiaAmountText;
    [SerializeField] private TMP_Text citizensAmountText;
    [SerializeField] private Button showRolesDistributionBackButton;
    [SerializeField] private Button showRolesDistributionContinueButton;
    
    //create team instruction elements
    [SerializeField] private GameObject createTeamInstructionPanel;
    [SerializeField] private Button createTeamInstructionBackButton;
    [SerializeField] private Button createTeamInstructionContinueButton;
    
    //give phone to the next player instruction elements
    [SerializeField] private GameObject playerInstructionPanel;
    [SerializeField] private TMP_Text playerInstructionText;
    [SerializeField] private Button playerInstructionContinueButton;
    private string _playerInstruction = "Please give the phone to the player №{0}";
    
    //enter player name elements
    [SerializeField] private GameObject enterPlayerNamePanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button getRoleButton;
    [SerializeField] private GameObject nameErrorText;
    
    //show role elements
    [SerializeField] private GameObject showRolePanel;
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text roleText;
    private string _roleString = "You are {0}";
    private Color _mafiaColor = Color.red;
    private Color _citizenColor = Color.green;
    [SerializeField] private Button showRoleContinueButton;
    
    //end of roles distribution instruction elements
    [SerializeField] private GameObject endOfDistributionInstructionPanel;
    [SerializeField] private Button endOfDistributionContinueButton;
    
    //game screen elements
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private PlayerGamingItemController playerGamingItemPrefab;
    [SerializeField] private Transform playerItemsParent;

    private int _numberOfPlayers;
    private int _numberOfMafia;
    private int _numberOfCitizens;
    private int _currentPlayerAssigment;
    private List<RolesEnum> _availableRoles;
    private Dictionary<string, RolesEnum> _rolesDistribution;
    
    private void Awake()
    {
        showRulesButton.onClick.AddListener(OnShowRulesButtonClick);
        gameRulesBackButton.onClick.AddListener(OnGameRulesBackButtonClick);
        newGameButton.onClick.AddListener(OnStartGameButtonClick);
        confirmNumberOfPlayersButton.onClick.AddListener(OnConfirmNumberOfPlayersButtonClick);
        enterNumberOfPlayersBackButton.onClick.AddListener(OnEnterNumberOfPlayersBackButtonClick);
        showRolesDistributionBackButton.onClick.AddListener(OnShowRolesDistributionBackButtonClick);
        showRolesDistributionContinueButton.onClick.AddListener(OnShowRolesDistributionContinueButtonClick);
        createTeamInstructionBackButton.onClick.AddListener(OnCreateTeamInstructionBackButtonClick);
        createTeamInstructionContinueButton.onClick.AddListener(OnCreateTeamInstructionContinueButtonClick);
        playerInstructionContinueButton.onClick.AddListener(OnPlayerInstructionContinueButtonClick);
        getRoleButton.onClick.AddListener(OnGetRoleButtonClick);
        showRoleContinueButton.onClick.AddListener(OnShowRoleContinueButtonClick);
        endOfDistributionContinueButton.onClick.AddListener(OnEndOfDistributionContinueButtonClick);
    }

    private void OnShowRulesButtonClick()
    {
        startMenuPanel.SetActive(false);
        gameRulesPanel.SetActive(true);
    }

    private void OnGameRulesBackButtonClick()
    {
        gameRulesPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    private void OnStartGameButtonClick()
    {
        startMenuPanel.SetActive(false);
        enterNumberOfPlayersPanel.SetActive(true);
    }

    private void OnConfirmNumberOfPlayersButtonClick()
    {
        if (numberOfPlayersTMPInputField.text == "")
        {
            numberOfPlayersErrorText.SetActive(true);
            return;
        }
        
        _numberOfPlayers =  Convert.ToInt32(numberOfPlayersTMPInputField.text);
            
        if (_numberOfPlayers < 3)
        {
            numberOfPlayersErrorText.SetActive(true);
            return;
        }
        
        enterNumberOfPlayersPanel.SetActive(false);
        numberOfPlayersErrorText.SetActive(false);
        _numberOfMafia = Mathf.RoundToInt((float)(_numberOfPlayers * 28.0 / 100.0));
        _numberOfCitizens = _numberOfPlayers - _numberOfMafia;
        mafiaAmountText.text = _numberOfMafia.ToString();
        citizensAmountText.text = _numberOfCitizens.ToString();
        showRolesDistributionPanel.SetActive(true);
    }

    private void OnEnterNumberOfPlayersBackButtonClick()
    {
        enterNumberOfPlayersPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }

    private void OnShowRolesDistributionBackButtonClick()
    {
        showRolesDistributionPanel.SetActive(false);
        enterNumberOfPlayersPanel.SetActive(true);
    }

    private void OnShowRolesDistributionContinueButtonClick()
    {
        showRolesDistributionPanel.SetActive(false);
        createTeamInstructionPanel.SetActive(true);
    }

    private void OnCreateTeamInstructionBackButtonClick()
    {
        createTeamInstructionPanel.SetActive(false);
        showRolesDistributionPanel.SetActive(true);
    }

    private void OnCreateTeamInstructionContinueButtonClick()
    {
        createTeamInstructionPanel.SetActive(false);
        StartRolesDistribution();
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
        playerInstructionText.text = String.Format(_playerInstruction, _currentPlayerAssigment);
        playerInstructionPanel.SetActive(true);
    }

    private void OnPlayerInstructionContinueButtonClick()
    {
        playerInstructionPanel.SetActive(false);
        enterPlayerNamePanel.SetActive(true);
    }

    private void OnGetRoleButtonClick()
    {
        if(nameInputField.text == "") return;
        
        if(_rolesDistribution.ContainsKey(nameInputField.text))
        {
            nameErrorText.SetActive(true);
            return;
        }
        
        enterPlayerNamePanel.SetActive(false);
        nameErrorText.SetActive(false);
        int i = Random.Range(0, _availableRoles.Count);
        RolesEnum role = _availableRoles[i];
        _availableRoles.RemoveAt(i);
        _rolesDistribution.Add(nameInputField.text, role);
        roleText.text = String.Format(_roleString, role);
        roleImage.color = GetRolesColor(role);
        nameInputField.text = "";
        showRolePanel.SetActive(true);
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

    private void OnShowRoleContinueButtonClick()
    {
        showRolePanel.SetActive(false);
        _currentPlayerAssigment++;

        if (_currentPlayerAssigment <= _numberOfPlayers)
        {
            ShowPlayerInstructionPanel();
        }
        else
        {
            endOfDistributionInstructionPanel.SetActive(true);
        }
    }

    private void OnEndOfDistributionContinueButtonClick()
    {
        endOfDistributionInstructionPanel.SetActive(false);

        foreach (var player in _rolesDistribution)
        {
            Instantiate(playerGamingItemPrefab, Vector3.zero, Quaternion.identity, playerItemsParent)
                .Init(GetRolesColor(player.Value), player.Value, player.Key);
        }
        
        gamePanel.SetActive(true);
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
