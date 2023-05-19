using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public Image gamepadEast;
    public Image gamepadSouth;
    
    //sliders for music and SFX
    //save this
        //"PlayerPrefs"
        public float musicVolume = 1f;
    
        public float SFXVolume = 1f;

        public GameObject musicObj;
        public Slider musicSlider;

        public GameObject SFXObj;
    public Slider SFXSlider;

    public Dictionary<float, Slider> sliderSettings;

    public float[] sliderFloats;
    public int selectedSliderIndex = 0;

    public void Start()
    {
        StartMenu();
        StartOptions();

        if(cogsManager)
            cogsManager.StopStartStop();
    }

    public void StartOptions()
    {
        musicObj = musicSlider.gameObject;
        SFXObj = SFXSlider.gameObject;
        
        sliderFloats = new[] { musicVolume, SFXVolume };
        
        /*
        sliderSettings = new Dictionary<float, Slider>()
        {
            { musicVolume, musicSlider },
            { SFXVolume, SFXSlider }
        };
        */
        
        SetSliderValue(musicVolume, musicSlider);
        SetSliderValue(SFXVolume, SFXSlider);

    }

    public void SetSliderValue(float newValue, Slider slider)
    {
        if (slider != null)
        {
            slider.value = newValue;
        }
    }

    private void OnSliderNavigate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float inputDirection = context.ReadValue<float>();
            
            if (inputDirection > 0)
            {
                selectedSliderIndex--;

                if (selectedSliderIndex < 0)
                    selectedSliderIndex = sliderFloats.Length - 1;
            }
            else if (inputDirection < 0)
            {
                selectedSliderIndex++;
                
                if (selectedSliderIndex >= sliderFloats.Length)
                    selectedSliderIndex = 0;
            }
        }
    }
    
    public void StartMenu()
    {
        NPCSFX.PlaySound("Somber");

        menuText = new[] { startText, optionsText, quitText };

        playerActions = new PlayerActions();

        playerActions.MainMenu.NavigateUpDown.performed += OnMenuNavigate;
        
        playerActions.MainMenu.NavigateLeftRight.performed += OnSliderInteract;

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
        
        NeutralMenu();
    }

    private float prevMusVol;
    
    public void Update()
    {
        UpdateSlider(selectedTextIndex);

        musicVolume = musicSlider.value;
        
        if(prevMusVol != musicVolume)
        {
            NPCSFX.ChangeVolume(musicSlider.value);
            prevMusVol = musicVolume;
        }
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
        }
    }

    private void UpdateSlider(float index)
    {
        if (confirmingOptions)
        {
            if (index == 0)
            {
                musicSlider.Select();
            }

            else if (index == 1)
            {
                SFXSlider.Select();
            }


            else
            {
                musicSlider.OnDeselect(new BaseEventData(EventSystem.current));
                SFXSlider.OnDeselect(new BaseEventData(EventSystem.current));
            }
        }
    }

    public void OnSliderInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
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
        musicObj.SetActive(false);
        SFXObj.SetActive(false);

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
        if (confirmingOptions)
        {
            return;
        }
        
        if (confirmingExit)
        {
            NeutralMenu();
        }

        else
        {
            canvas.enabled = false;
            
            playerActions.MainMenu.NavigateUpDown.performed -= OnMenuNavigate;
            playerActions.MainMenu.NavigateLeftRight.performed -= OnSliderInteract;
            playerActions.MainMenu.Confirm.performed -= OnConfirm;
            playerActions.MainMenu.Cancel.performed -= OnCancel;
            
            playerActions.MainMenu.Disable();
            
            LevelManager.levelManager.SpawnNPC();
            
            GameObject playerObj = LevelManager.levelManager.SpawnPlayer();

            CameraController.Instance.cam1TargetTransform = playerObj.transform;
                CameraController.Instance.SetCam1Target();
                
                this.enabled = false;
        }
    }

    private void Options()
    {
        if(confirmingOptions)
            return;
        
        selectedTextIndex = 0;
        UpdateSlider(selectedTextIndex);
        UpdateSelectionColor();

        musicObj.SetActive(true);
        SFXObj.SetActive(true);
        
        if (!confirmingOptions)
        {
            confirmingOptions = true;
            startText.text = "MUSIC VOLUME";
            optionsText.text = "SOUND EFFECTS VOLUME";
            quitText.text = "I'M READY";
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
