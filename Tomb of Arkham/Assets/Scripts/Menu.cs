using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Menu : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------

    public enum MenuType {
        Main,
        Credits,
        Options,
        WinScreen,
        LoseScreen,
        LevelSelect,
        GeneralHud,
        Pause,
        Backdrop
    }

    [SerializeField] private MenuType menuType;
    LevelManager levelManager;
    GameObject thisObject;
    InputController inputController;

    //------------------------------------------------------
    //               GETTERS/SETTERS
    //------------------------------------------------------

    public MenuType GetMenuType() {return menuType;}
    public void SetMenuType(MenuType newType) {menuType = newType;}

    //------------------------------------------------------
    //               SHOW/HIDE FUNCTIONS
    //------------------------------------------------------

    public void ActivateMenu(MenuType showThisType) {
        if(this.menuType == showThisType) {
           thisObject.SetActive(true) ;
        }
    }
    public void DeactivateMenu(MenuType hideThisType) {
        if(this.menuType == hideThisType) {
            thisObject.SetActive(false) ;
        }
    }

    //------------------------------------------------------
    //               CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------

    private void CreateMenuList() {
        if(menuType != MenuType.Backdrop) {
            levelManager.GetMenuList().Add(this);
        }
        else {
            ProvideBackdrop();
        }
    }

    private void ProvideBackdrop() {
        levelManager.SetBackdrop(this);
    }

    //------------------------------------------------------
    //               STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        levelManager = LevelManager.Instance;
        CreateMenuList();
        thisObject = GetComponent<RectTransform>().gameObject;
        inputController = InputController.Instance;
    }

    private void Update() {
        
    }

    //------------------------------------------------------
    //               INPUT CONTROL FUNCTIONS
    //------------------------------------------------------

    
}
