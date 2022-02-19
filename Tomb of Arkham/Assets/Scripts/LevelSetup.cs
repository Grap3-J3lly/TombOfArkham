using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    public enum LevelType {
        Normal,
        Boss
    }
    
    Player player;
    LevelManager levelManager;
    [SerializeField] LevelType thisLevelType;
    [SerializeField] Vector3 playerSpawnLocation;
    
    // Normal Related

    // Boss Related
    private Vector3 bossSpawnLocation;
    [SerializeField] BossController bossController;
    [SerializeField] TPAnchorController tPAnchorController;
    [SerializeField] int initialSpawnLocIndex;
    [SerializeField] float floatHeight = 2.5f;

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public LevelType GetThisLevelType() {return thisLevelType;}
    public void SetThisLevelType(LevelType newType) {thisLevelType = newType;}

    // Normal Related

    // Boss Related
    public BossController GetBossController() {return bossController;}
    public void SetBossController(BossController newController) {bossController = newController;}
    public TPAnchorController GetTPAnchorController() {return tPAnchorController;}
    public void SetTPAnchorController(TPAnchorController newController) {tPAnchorController = newController;}
    public int GetInitialSpawnLocIndex() {return initialSpawnLocIndex;}
    public void SetInitialSpawnLocIndex(int newValue) {initialSpawnLocIndex = newValue;}
    public Vector3 GetBossSpawnLocation() {return bossSpawnLocation;}
    public void SetBossSpawnLocation(Vector3 newLoc) {bossSpawnLocation = newLoc;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        player = Player.Instance;
        levelManager = LevelManager.Instance;
        HandlePlayerSpawn();

        HandleBossLevelType();
    }
    
    //------------------------------------------------------
    //                  CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------
    // Player Related
    private void HandlePlayerSpawn() {
        levelManager.SetCurrentLevelSpawnpoint(playerSpawnLocation);
        levelManager.SetCurrentCheckpoint(playerSpawnLocation);
        player.transform.position = playerSpawnLocation;
    }

    // Boss Related
    private void HandleBossSpawn() {
        Transform firstAnchor = tPAnchorController.GetSpecificAnchor(initialSpawnLocIndex);
        Vector3 firstAnchorLoc = firstAnchor.position;
        
        bossSpawnLocation = new Vector3(firstAnchorLoc.x, floatHeight, firstAnchorLoc.z);
    }

    private void HandleBossLevelType() {
        if(thisLevelType == LevelType.Boss) {
            tPAnchorController.Awake();
            HandleBossSpawn();
        }
    }

}
