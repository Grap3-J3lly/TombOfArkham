using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    //------------------------------------------------------
    //                   BUTTON TYPES
    //------------------------------------------------------
    public enum ButtonType {
        Play,
        Options,
        Credits,
        Back,
        GameVolumeSliderControl,
        MusicToggle,
        MusicVolumeSliderControl,
        SoundToggle,
        SoundVolumeSliderControl,
        InvertControlsToggle,
        MainMenu,
        Restart,
        NextLevel,
        SpecificLevelStart,
        Jump,
        Attack,
        Pause,
        Resume
    }
    [SerializeField] private ButtonType buttonType;
    private ButtonType[] optionButtons = {
        ButtonType.GameVolumeSliderControl,
        ButtonType.MusicToggle,
        ButtonType.MusicVolumeSliderControl,
        ButtonType.SoundToggle,
        ButtonType.SoundVolumeSliderControl,
        ButtonType.InvertControlsToggle
    };

    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    private InputController inputController;
    private LevelManager levelManager;
    private GameObject thisObject;
    private AudioSource musicSource;
    private FoleyManager foleyManager;
    private AudioClip buttonClick;
    private Vector3[] buttonCorners = new Vector3[4];
    private float max_X;
    private float max_Y;
    private float min_X;
    private float min_Y;
    [SerializeField] private int toLevelNumber;
    private Sprite toggleOnImage;
    private Sprite toggleOffImage;
    private Sprite slideControlImage;

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------
    public ButtonType GetButtonType() {return buttonType;}
    public void SetButtonType(ButtonType type) {buttonType = type;}

    public int GetToLevelNumber() {return toLevelNumber;}
    public void SetToLevelNumber(int newLevelNum) {toLevelNumber = newLevelNum;}

    public GameObject GetThisObject() {return thisObject;}
    public void SetGameObject(GameObject newObject) {thisObject = newObject;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        levelManager = LevelManager.Instance;
        inputController = InputController.Instance;
        thisObject = GetComponent<RectTransform>().gameObject;
    }

    private void Start() {
        GetComponent<RectTransform>().GetWorldCorners(buttonCorners);
        HandleAudioSetup();
        HandleWorldCorners();
        HandleSpriteSetup();
        HandleOptionSetup();
    }

    private void Update() {
        HideButtonCheck(levelManager.GetGameComplete());
    }

    private void OnEnable() {
        inputController.OnEndTouch += WithinBounds;
    }

    private void OnDisable() {
        inputController.OnEndTouch -= WithinBounds;

    }

    //------------------------------------------------------
    //                  BUTTON FUNCTIONS
    //------------------------------------------------------
    private void HandleOptionSetup() {
        
    }
    private void HandleSpriteSetup() {
        if(Array.IndexOf(optionButtons, this.buttonType) != -1) {
            toggleOnImage = Resources.Load<Sprite>("Sprites/check");
            toggleOffImage = Resources.Load<Sprite>("Sprites/blank");
            slideControlImage = Resources.Load<Sprite>("Sprites/slideControl");
        }
    }

    public void HideButtonCheck(bool complete) {
        if(complete && this.buttonType == ButtonType.NextLevel) {
            this.thisObject.SetActive(false);
        }
    }

    private void HandleAudioSetup() {
        musicSource = levelManager.GetCurrentMusicSource();
        foleyManager = FoleyManager.Instance;
        buttonClick = (AudioClip)Resources.Load("singleButtonClick");
    }

    private void HandleWorldCorners() {
        float[] xVals = new float[buttonCorners.Length];
        float[] yVals = new float[buttonCorners.Length];
        int count = 0;

        foreach(Vector3 point in buttonCorners) {
            xVals[count] = point.x;
            yVals[count] = point.y;
            count++;
        }
        max_X = Mathf.Max(xVals);
        min_X = Mathf.Min(xVals);
        max_Y = Mathf.Max(yVals);
        min_Y = Mathf.Min(yVals);
    }

    private void WithinBounds(Vector2 pointToCheck) {
        if(pointToCheck.x >= min_X && pointToCheck.x <= max_X && pointToCheck.y >= min_Y && pointToCheck.y <= max_Y) 
        {
            HandleButtonPress();
        }
    }

    private void HandleButtonPress() {
        foleyManager.Play(buttonClick.name);
        if(Array.IndexOf(optionButtons, this.buttonType) == -1) {
            levelManager.ChangeMenuByButton(this);
        }
        else {
            HandleOptionButtons();
        }
        
    }

    private void HandleOptionButtons() {
        switch(this.buttonType) {
            case ButtonType.GameVolumeSliderControl:
            break;
            case ButtonType.MusicToggle:
            ToggleAudioSource(musicSource);
            break;
            case ButtonType.MusicVolumeSliderControl:
            break;
            case ButtonType.SoundToggle:
            ToggleAudioSource(foleyManager.GetAudioSource());
            break;
            case ButtonType.SoundVolumeSliderControl:
            break;
        }
    }

    private void ToggleImage(bool status) {
        Image currentButtonImage = GetComponent<Image>();
        if(status) {
            currentButtonImage.sprite = toggleOnImage;
        }
        else {
            currentButtonImage.sprite = toggleOffImage;
        }
    }

    private void ToggleAudioSource(AudioSource thisSource) {
        bool status = thisSource.gameObject.activeSelf;
        status = !status;
        thisSource.gameObject.SetActive(status);
        ToggleImage(status);
    }

}
