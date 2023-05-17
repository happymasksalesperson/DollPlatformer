using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private PlayerActions playerActions;

    public TMP_Text[] menuText;
    public int selectedTextIndex = 0;

    public Dictionary<int, Action> menuActions;

    public TMP_Text startText;
    public TMP_Text optionsText;
    public TMP_Text quitText;

    public Canvas canvas;

    public bool confirmingExit = false;

    public bool confirmingOptions = false;

    public GameObject idlePlayer;

    public CogsManager cogsManager;

    public void Start()
    {
        NPCSFX.PlaySound("Somber");

        menuText = new[] { startText, optionsText, quitText };

        playerActions = new PlayerActions();

        playerActions.MainMenu.Navigate.performed += OnMenuNavigate;

        playerActions.MainMenu.Confirm.performed += OnConfirm;

        playerActions.MainMenu.Cancel.performed += OnCancel;

        playerActions.MainMenu.Enable();

        UpdateSelectionColor();

        menuActions = new Dictionary<int, Action>()
        {
            { 0, StartGame },
            { 1, Options },
            { 2, QuitGame }
        };
        
        cogsManager.StopStartAllCogs();
    }

    public void OnMenuNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float inputDirection = context.ReadValue<float>();

            if (inputDirection > 0)
            {
                selectedTextIndex--;
                
                if (confirmingExit && selectedTextIndex == 1)
                    selectedTextIndex = 0;

                if (selectedTextIndex < 0)
                    selectedTextIndex = menuText.Length - 1;
            }
            else if (inputDirection < 0)
            {
                selectedTextIndex++;
                
                if (confirmingExit && selectedTextIndex == 1)
                    selectedTextIndex = 2;
                
                if (selectedTextIndex >= menuText.Length)
                    selectedTextIndex = 0;
            }
            
            UpdateSelectionColor();
            
            Debug.Log(inputDirection);
        }
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed && menuActions.ContainsKey(selectedTextIndex))
        {
            menuActions[selectedTextIndex]?.Invoke();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NeutralMenu();
        }
    }

    private void NeutralMenu()
    {
        selectedTextIndex = 0;
        UpdateSelectionColor();

        confirmingOptions = false;
        confirmingExit = false;
        
        startText.text = "Start";
        optionsText.enabled = true;
        optionsText.text = "Options";
        quitText.text = "Quit";
    }

    private void StartGame()
    {
        if (confirmingExit || confirmingOptions)
        {
            NeutralMenu();
        }

        else
        {
            canvas.enabled = false;
            
            playerActions.MainMenu.Navigate.performed -= OnMenuNavigate;
            playerActions.MainMenu.Confirm.performed -= OnConfirm;
            playerActions.MainMenu.Cancel.performed -= OnCancel;
            
            playerActions.MainMenu.Disable();
            
            this.enabled = false;
            
            LevelManager.levelManager.SpawnPlayer();
            Destroy(idlePlayer);
        }
    }

    private void Options()
    {
        selectedTextIndex = 0;
        UpdateSelectionColor();
        
        if (!confirmingOptions)
        {
            confirmingOptions = true;
            startText.text = "Come";
            optionsText.text = "Back";
            quitText.text = "Later";
        }
        else
            NeutralMenu();
    }

    private void QuitGame()
    {
        if (confirmingExit)
            FinalQuit();
        
        else if (confirmingOptions)
            NeutralMenu();

        else
            ConfirmingExit();
    }

    private void ConfirmingExit()
    {
        selectedTextIndex = 0;
        UpdateSelectionColor();
        confirmingExit = true;
        startText.text = "I couldn't stop. I had to push on.";
        optionsText.enabled = false;
        quitText.text = "But I was too tired to go on.";
    }

    private void FinalQuit()
    {
        Debug.Log("cya");
        Application.Quit();
    }

    private void UpdateSelectionColor()
    {
        for (int i = 0; i < menuText.Length; i++)
        {
            if (i == selectedTextIndex)
                menuText[i].color = Color.white;
            else
                menuText[i].color = Color.black;
        }
    }
}
