using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    //  Will need to adjust if more unlockables are added to game

    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    [SerializeField] private int requiredKeyCount = 1;
    private Player player;
    private LevelManager levelManager;    

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public int GetReqKeyCount() {return requiredKeyCount;}
    public void SetReqKeyCount(int newCount) {requiredKeyCount = newCount;}

    //------------------------------------------------------
    //                 STANDARD FUNCTIONS
    //------------------------------------------------------
    void Awake()
    {
        levelManager = LevelManager.Instance;
        player = Player.Instance;        
    }

    //------------------------------------------------------
    //                 COLLISION FUNCTIONS
    //------------------------------------------------------
    private void OnTriggerEnter(Collider thing) {

        HandlePlayerTrigger(thing.gameObject);
    }

    //------------------------------------------------------
    //                 CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------
    private void Unlock() {
        if(player.GetIdolCount() == requiredKeyCount) {
            player.SetIdolCount(0);
            levelManager.HandleLevelCompletion();
        }
    }

    private void HandlePlayerTrigger(GameObject obj) {
        if(obj == player.gameObject) {
            Unlock();
        }
    }

}
