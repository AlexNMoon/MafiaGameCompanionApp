using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startMenuPanel;
    [SerializeField] private Button startGameButton;
    [SerializeField] private GameObject createTeamPanel;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        startMenuPanel.SetActive(false);
        createTeamPanel.SetActive(true);
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
