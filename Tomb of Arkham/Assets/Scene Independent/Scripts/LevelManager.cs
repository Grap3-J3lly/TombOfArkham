using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    #region
    public static LevelManager Instance;
    
    [SerializeField] private Vector3 currentLevelSpawnpoint;
    private Player player;
    private KnifeController knife;
    private Vector3 currentCheckpoint;
    private int gameplaySceneIndex = 0;
    private int currentAreaInitialSceneIndex = 1;
    private int currentSceneIndex = 0;
    private bool sceneLoaded = false;
    [SerializeField] private Camera backDropCamera;
    [SerializeField] private Camera playerCam;
    private bool inMenu;
    [SerializeField] private Menu.MenuType startUpMenuType;
    private Menu previousMenu;
    private Menu currentMenu;
    private Menu backDrop;
    private List<Menu> availableMenus = new List<Menu>();

    private List<string> scenes = new List<string>() {
        "Gameplay",
        "NormalLevel",
        "BossLevel"
    };
    private List<string> customTags = new List<string>(){
        "Player",
        "Idol",
        "Unlockable",
        "Level End",
        "Hazard",
        "Checkpoint",
        "Environment"
    };
    #endregion

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------
    #region

    public Vector3 GetCurrentLevelSpawnpoint() {return currentLevelSpawnpoint;}
    public void SetCurrentLevelSpawnpoint(Vector3 newPoint) {currentLevelSpawnpoint = newPoint;}

    public Vector3 GetCurrentCheckpoint() {return currentCheckpoint;}
    public void SetCurrentCheckpoint(Vector3 newValue) {currentCheckpoint = newValue;}

    public Menu GetBackdrop() {return backDrop;}
    public void SetBackdrop(Menu newBackdrop) {backDrop = newBackdrop;}
    public bool GetSceneLoaded() {return sceneLoaded;}
    public void SetSceneLoaded(bool newValue) {sceneLoaded = newValue;}

    // List of Menus
    public List<Menu> GetMenuList() {return availableMenus;}
    public void SetMenuList(List<Menu> newList) {availableMenus = newList;}

    // List of Scenes
    public List<string> GetScenes() {return scenes;}
    public string GetSpecificScene(int sceneIndex) {return scenes[sceneIndex];}
    public void SetScenes(List<string> newScenes) {scenes = newScenes;}
    public void SetSpecificScene(int index, string newScene) {scenes[index] = newScene;}
    
    // List of Custom Tags
    public List<string> GetCustomTags() {return customTags;}
    public string GetSpecificCustomTag(int tagIndex) {return customTags[tagIndex];}
    public void SetTags(List<string> newTags) {customTags = newTags;}
    public void SetSpecificCustomTag(int index, string newTag) {customTags[index] = newTag;}

    #endregion

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        Instance = this;
        knife = KnifeController.Instance;    
        Debug.Log("GAME OPENED AND RUNNING");
    }

    private void Start() {
        player = Player.Instance;
        HandleMenuSetup(startUpMenuType);
    }

    

    //------------------------------------------------------
    //                  SCENE FUNCTIONS
    //------------------------------------------------------
    #region

    private int ResetSceneToStartScene(int nextScene) {
        if(nextScene == scenes.Count) {
            nextScene = currentAreaInitialSceneIndex;
        }
        return nextScene;
    }

    private void ChangeScene(int nextScene) {
        if(!sceneLoaded) {

            nextScene = ResetSceneToStartScene(nextScene);

            if(currentSceneIndex != gameplaySceneIndex) {
                UnloadScene(scenes[currentSceneIndex]);
            }
            LoadScene(scenes[nextScene]);
            currentSceneIndex = nextScene;

            sceneLoaded = !sceneLoaded;
        }  
    }

    private void LoadScene(string sceneToLoadName) {
        SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
    }
    private void UnloadScene(string sceneToUnloadName) {
        SceneManager.UnloadSceneAsync(sceneToUnloadName);
    }

    #endregion

    //------------------------------------------------------
    //                  MENU FUNCTIONS
    //------------------------------------------------------

    private void HandleMenuSetup(Menu.MenuType nextMenu) {
        currentMenu = SearchMenus(nextMenu);

        availableMenus.Remove(currentMenu);
        foreach(Menu menu in availableMenus) {
            menu.DeactivateMenu(menu.GetMenuType());
        }
        availableMenus.Add(currentMenu);
    }

    private Menu SearchMenus(Menu.MenuType theMenuType) {

        return availableMenus.Find(specificMenu => specificMenu.GetMenuType() == theMenuType);
    }

    private void ChangeMenu(Menu thisMenu) {
        currentMenu.DeactivateMenu(currentMenu.GetMenuType());
        thisMenu.ActivateMenu(thisMenu.GetMenuType());
        previousMenu = currentMenu;
        currentMenu = thisMenu;
    }

    private void ToggleBackdrop(bool currentlyInMenu) {
        if(currentlyInMenu) {
            backDrop.ActivateMenu(backDrop.GetMenuType());
        }
        else {
            backDrop.DeactivateMenu(backDrop.GetMenuType());
        }
    }

    // Need to redesign this so it's based off method call to ButtonController for the specific button Types
    public void ChangeMenuByButton(ButtonController buttonPressed) {
        switch(buttonPressed.GetButtonType()) {
            // "Play" goes to Level Select. Specific level is chosen by SpecificLevelStart
            case ButtonController.ButtonType.Play :
                ChangeMenu(SearchMenus(Menu.MenuType.LevelSelect));
            break;
            // Options
            case ButtonController.ButtonType.Options : 
                ChangeMenu(SearchMenus(Menu.MenuType.Options));
                break;
            // Credits
            case ButtonController.ButtonType.Credits :
                ChangeMenu(SearchMenus(Menu.MenuType.Credits));
                break;
            // Back (PreviousMenu)
            case ButtonController.ButtonType.Back : 
                HandleBackButton();
                break;
            // Start Specific Level / Next Level
            case ButtonController.ButtonType.SpecificLevelStart :
            case ButtonController.ButtonType.NextLevel :
                StartLevel();
                break;
            // Main Menu
            case ButtonController.ButtonType.MainMenu : 
                HandleMainMenuChange();
                break;
            // Pause Menu
            case ButtonController.ButtonType.Pause : 
                HandlePause();
                break;
            // Restart Level
            case ButtonController.ButtonType.Restart :
                RestartGame();
                break;
            case ButtonController.ButtonType.Resume : 
                HandleResume();
                break;
        }
    }

    //------------------------------------------------------
    //                  GAME FUNCTIONS
    //------------------------------------------------------
    #region

    private void HandleMainMenuChange() {
        ChangeMenu(SearchMenus(Menu.MenuType.Main));
        if(SceneManager.sceneCount > 1) {
            UnloadScene(scenes[currentSceneIndex]);
            currentSceneIndex = 0;
        }
    }

    private void HandleResume() {
        inMenu = false;
        ChangeMenu(SearchMenus(Menu.MenuType.GeneralHud)); 
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        Time.timeScale = 1;
    }

    // Documented reference for better way to pause game, need to implement
    private void HandlePause() {
        inMenu = true;
        ChangeMenu(SearchMenus(Menu.MenuType.Pause));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        Time.timeScale = 0;
    }

    private void HandleBackButton() {
        if(previousMenu.GetMenuType() == Menu.MenuType.GeneralHud) {
            HandleResume();
        }
        ChangeMenu(previousMenu);
    }

    public void HandleLevelCompletion() {
        inMenu = true;
        ChangeMenu(SearchMenus(Menu.MenuType.WinScreen));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
    }

    public void HandleGameOver() {
        inMenu = true;
        ChangeMenu(SearchMenus(Menu.MenuType.LoseScreen));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
    }

    private void StartLevel() {
        sceneLoaded = false;
        inMenu = false;
        ChangeMenu(SearchMenus(Menu.MenuType.GeneralHud));
        ChangeScene(currentSceneIndex + 1);
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        PlayerSetup();
        Debug.Log("Starting Level");
    }

    private void ToggleCamera(bool currentlyInMenu) {
        backDropCamera.gameObject.SetActive(currentlyInMenu);
        playerCam.gameObject.SetActive(!currentlyInMenu);
    }

    private void PlayerSetup() {
        Debug.Log("Resetting Player");
        ResetCheckpoint();
        player.transform.position = currentLevelSpawnpoint;
        sceneLoaded = true;
    }

    public void RestartGame() {
        sceneLoaded = false;
        ChangeScene(currentAreaInitialSceneIndex);
        HandleResume();
        inMenu = false;
        ChangeMenu(SearchMenus(Menu.MenuType.GeneralHud));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        PlayerSetup();
    }

    private void ResetCheckpoint() {
        currentCheckpoint = currentLevelSpawnpoint;
    }

    #endregion

    //------------------------------------------------------
    //                  TAG FUNCTIONS
    //------------------------------------------------------
    #region
    public int SearchTags(string tagName) {return customTags.IndexOf(tagName);}
    public int GetPlayerIndex() {return customTags.IndexOf("Player");}
    public int GetIdolIndex() {return customTags.IndexOf("Idol");}
    public int GetHazardIndex() {return customTags.IndexOf("Hazard");}

    #endregion

}