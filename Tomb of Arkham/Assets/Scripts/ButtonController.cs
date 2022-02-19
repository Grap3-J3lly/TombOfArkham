using UnityEngine;
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
        SoungToggle,
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

    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    private InputController inputController;
    private LevelManager levelManager;
    private GameObject thisObject;
    private Vector3[] buttonCorners = new Vector3[4];
    private float max_X;
    private float max_Y;
    private float min_X;
    private float min_Y;
    [SerializeField] private int toLevelNumber;

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------
    public ButtonType GetButtonType() {return buttonType;}
    public void SetButtonType(ButtonType type) {buttonType = type;}

    public int GetToLevelNumber() {return toLevelNumber;}
    public void SetToLevelNumber(int newLevelNum) {toLevelNumber = newLevelNum;}

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
        HandleWorldCorners();
    }

    private void Update() {
        HandleOptions();
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
            levelManager.ChangeMenuByButton(this);
        }
    }

    private bool IgnoreHudButtons() {
        if(this.buttonType == ButtonType.Attack || this.buttonType == ButtonType.Jump) {
            return true;
        }
        return false;
    }

    private void HandleOptions() {
        
    }


}
