using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    #region
    public static LevelManager Instance;
    
    // General
    [SerializeField] private Vector3 idlePosition;
    [SerializeField] private Vector3 currentLevelSpawnpoint;
    [SerializeField] private int idolPointValue;
    private Player player;
    private KnifeController knife;
    private Vector3 currentCheckpoint;
    private bool readyToSpawn = false;
    private bool gameComplete = false;
    private int currentScore = 0;

    // Audio Related
    [SerializeField] private AudioSource generalMusicSource;
    private AudioSource currentMusicSource;
    private FoleyManager foleyManager;
    private AudioClip gameOverTransitionSound;
    private AudioClip gameWinSound;

    // Scene Related
    [SerializeField] private int finalLevelIndex = 2;
    private int gameplaySceneIndex = 0;
    private int currentAreaInitialSceneIndex = 1;
    private int currentSceneIndex = 0;
    private bool sceneLoaded = false;

    // Camera Related
    [SerializeField] private Camera backDropCamera;
    [SerializeField] private Camera playerCam;

    // Menu Related
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
        "Environment",
        "Boss"
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

    public int GetCurrentScore() {return currentScore;}
    public void SetCurrentScore(int value) {currentScore = value;}
    
    public int GetIdolPointValue() {return idolPointValue;}
    public void SetIdolPointValue(int amount) {idolPointValue = amount;}

    public Menu GetBackdrop() {return backDrop;}
    public void SetBackdrop(Menu newBackdrop) {backDrop = newBackdrop;}
    public bool GetSceneLoaded() {return sceneLoaded;}
    public void SetSceneLoaded(bool newValue) {sceneLoaded = newValue;}

    public bool GetGameComplete() {return gameComplete;}
    public void SetGameComplete(bool newValue) {gameComplete = newValue;}

    public AudioSource GetCurrentMusicSource() {return currentMusicSource;}
    public void SetMusicSource(AudioSource newSource) {currentMusicSource = newSource;}

    public Menu GetCurrentMenu() {return currentMenu;}
    public void SetCurrentMenu(Menu newMenu) {currentMenu = newMenu;}

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
        currentMusicSource = generalMusicSource;
    }

    private void Start() {
        player = Player.Instance;
        TogglePlayer(readyToSpawn);
        HandleMenuSetup(startUpMenuType);
        HandleFoleySetup();
    }

    private void Update() {
        if(SceneManager.sceneCount == 1) {
            player.transform.position = idlePosition;
        }
    }

    //------------------------------------------------------
    //                  AUDIO FUNCTIONS
    //------------------------------------------------------

    private void HandleFoleySetup() {
        foleyManager = FoleyManager.Instance;
        gameOverTransitionSound = (AudioClip)Resources.Load("gameOverTransition");
        gameWinSound = (AudioClip)Resources.Load("gameWin");
    }

    IEnumerator HandleGameOverAudio() {
        //yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(gameOverTransitionSound.name);
        yield return new WaitForSeconds(1);
    }

    IEnumerator HandleGameWinAudio() {
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(gameWinSound.name);
    }

    public void HandleMusicSource(AudioSource newSource) {
        if(currentMusicSource == generalMusicSource) {
            
            generalMusicSource.Pause();
        }
        currentMusicSource = newSource;
        currentMusicSource.Play();
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
            SceneManager.sceneLoaded += LevelStartSetup;
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
                HandleSpecificLevel(buttonPressed);
                break;
            case ButtonController.ButtonType.NextLevel :
                HandleNextLevel();
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
            currentScore = 0;
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
        if(currentSceneIndex == finalLevelIndex) {
            currentMusicSource.Pause();
            gameComplete = true;
        }
        StartCoroutine(HandleGameWinAudio());
        inMenu = true;
        ChangeMenu(SearchMenus(Menu.MenuType.WinScreen));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        
    }

    public void HandleGameOver() {
        StartCoroutine(HandleGameOverAudio());
        inMenu = true;
        ChangeMenu(SearchMenus(Menu.MenuType.LoseScreen));
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
    }

    private void StartLevel(int nextLevelIndex) {
        sceneLoaded = false;
        ChangeScene(nextLevelIndex);
        inMenu = false;
        if(Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }

    private void ToggleCamera(bool currentlyInMenu) {
        backDropCamera.gameObject.SetActive(currentlyInMenu);
        playerCam.gameObject.SetActive(!currentlyInMenu);
    }

    private void HandleSpecificLevel(ButtonController button) {
        int nextLevelIndex = button.GetToLevelNumber();
        StartLevel(nextLevelIndex);
    }

    private void HandleNextLevel() {
        int nextLevelIndex = currentSceneIndex + 1;
        StartLevel(nextLevelIndex);
    }

    private void TogglePlayer(bool status) {
        player.gameObject.SetActive(status);
    }

    private void LevelStartSetup(Scene scene, LoadSceneMode mode) {
        ResetCheckpoint();
        readyToSpawn = true;
        TogglePlayer(readyToSpawn);
        player.transform.position = currentLevelSpawnpoint;
        ChangeMenu(SearchMenus(Menu.MenuType.GeneralHud));
        StartCoroutine(ResetHudText());
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
        sceneLoaded = true;
    }

    public void RestartGame() {
        sceneLoaded = false;
        gameComplete = false;
        currentScore = 0;
        ChangeScene(currentAreaInitialSceneIndex);
        HandleResume();
        inMenu = false;
        ChangeMenu(SearchMenus(Menu.MenuType.GeneralHud));
        StartCoroutine(ResetHudText());
        ToggleBackdrop(inMenu);
        ToggleCamera(inMenu);
    }

    IEnumerator ResetHudText() {
        yield return new WaitUntil(() => currentMenu.GetMenuType() == Menu.MenuType.GeneralHud);
        currentMenu.HandleTextUpdate(Menu.TextSectionType.LifeCount, player.GetLivesRemaining());
        currentMenu.HandleTextUpdate(Menu.TextSectionType.IdolCount, player.GetIdolsRemaining());
        currentMenu.HandleTextUpdate(Menu.TextSectionType.Score, currentScore);
    }

    private void ResetCheckpoint() {
        currentCheckpoint = currentLevelSpawnpoint;
    }

    public void IncreaseScore() {
        currentScore += idolPointValue;
        currentMenu.HandleTextUpdate(Menu.TextSectionType.Score, currentScore);
        currentMenu.HandleTextUpdate(Menu.TextSectionType.IdolCount, player.GetIdolsRemaining());
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