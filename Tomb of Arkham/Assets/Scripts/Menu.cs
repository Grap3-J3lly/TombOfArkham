using UnityEngine;
using TMPro;

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

    public enum TextSectionType {
        Score,
        IdolCount,
        LifeCount
    }

    private TextSectionType currentType;

    private LevelManager levelManager;
    private Player player;
    private GameObject thisObject;
    private InputController inputController;
    private GameObject scoreSection;
    private GameObject idolCountSection;
    private GameObject lifeCountSection;

    //------------------------------------------------------
    //               GETTERS/SETTERS
    //------------------------------------------------------

    public MenuType GetMenuType() {return menuType;}
    public void SetMenuType(MenuType newType) {menuType = newType;}

    //------------------------------------------------------
    //               GENERAL FUNCTIONS
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

    public void HandleTextUpdate(TextSectionType thisType, int amount) {
        switch(thisType) {
            case TextSectionType.Score: 
            scoreSection.GetComponent<TMP_Text>().text = "Score: " + amount;
            break;
            case TextSectionType.IdolCount:
            idolCountSection.GetComponent<TMP_Text>().text = "Idols Remaining: " + amount;
            break;
            case TextSectionType.LifeCount:
            lifeCountSection.GetComponent<TMP_Text>().text = "Lives Remaining: " + amount;
            break;
        }
    }

    private void HandleScoreUpdate(int amount) {

    }

    private void HandleIdolUpdate(int idolCount) {

    }

    private void HandleLivesUpdate(int lifeCount) {

    }

    private void GetInfoSection() {
        if(this.menuType == MenuType.GeneralHud) {
            Transform tempTransform = GetComponent<RectTransform>();
            int childCount = tempTransform.childCount;
            for(int index = 0; index < childCount; index++) {
                if(tempTransform.GetChild(index).gameObject.name == "Info") {
                    GetTextSections(tempTransform.GetChild(index));
                }
            }
        }
    }

    private void GetTextSections(Transform parentTransform) {
        int childCount = parentTransform.childCount;
        for(int index = 0; index < childCount; index++) {
            if(parentTransform.GetChild(index).gameObject.name == "Score") {
                scoreSection = parentTransform.GetChild(index).gameObject;
                
            }
            if(parentTransform.GetChild(index).gameObject.name == "IdolCount") {
                idolCountSection = parentTransform.GetChild(index).gameObject;
            }
            if(parentTransform.GetChild(index).gameObject.name == "LifeCount") {
                lifeCountSection = parentTransform.GetChild(index).gameObject;
            }
        }
    }

    //------------------------------------------------------
    //               STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        levelManager = LevelManager.Instance;
        player = Player.Instance;
        CreateMenuList();
        thisObject = GetComponent<RectTransform>().gameObject;
    }

    private void Start() {
        GetInfoSection();
    }

    private void Update() {
        
    }
    
}
