using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputController : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    public static InputController Instance;
    private bool isTouched = false;
    private Touch activeTouch = new Touch();
    public string testString = "pressing screen";

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------

    public bool GetIsTouched() {return isTouched;}
    public void SetIsTouched(bool newValue) {isTouched = newValue;}

    public Touch GetTouch() {return activeTouch;}
    public void SetTouch(Touch newTouch) {activeTouch = newTouch;}

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        Instance = this;  
    }

    private void OnEnable() {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable() {
        EnhancedTouchSupport.Disable();
    }

    private void Update() {
        DetectTouch();
    }

    //------------------------------------------------------
    //                   DETECTION FUNCTIONS
    //------------------------------------------------------

    private void DetectTouch() {
        if(Touch.activeFingers.Count == 1) {
            Debug.Log("TOUCH IS FOUND");
            activeTouch = Touch.activeFingers[0].currentTouch;
            if(activeTouch.phase == TouchPhase.Began) {
                isTouched = true;
            } else {
                isTouched = false;
            }
        }
    }
}
