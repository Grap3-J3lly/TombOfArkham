using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputController : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
     public static InputController Instance;
    private ControlLayout controlLayout;

    public delegate void StartTouchEvent();
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;

    

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        Instance = this;  
        controlLayout = new ControlLayout();
    }

    private void OnEnable() {
        controlLayout.Menus.Enable();
    }

    private void OnDisable() {
        controlLayout.Menus.Disable();
    }

    private void Update() {

    }

    private void Start() {
        controlLayout.Menus.TouchPress.performed += StartTouch;
        controlLayout.Menus.TouchPress.canceled += EndTouch;
    }

    //------------------------------------------------------
    //                   DETECTION FUNCTIONS
    //------------------------------------------------------

    public void StartTouch(InputAction.CallbackContext context) {
        if(OnStartTouch != null) {
            OnStartTouch();
        }
                
    }

     public void EndTouch(InputAction.CallbackContext context) {
         if(OnEndTouch != null) {
             OnEndTouch(controlLayout.Menus.TouchPosition.ReadValue<Vector2>());
         }
    }
}
