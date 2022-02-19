using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    private LevelManager levelManager;
    private Player player;
    
    [SerializeField] private int playerTagIndex;

    //------------------------------------------------------
    //                 STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        levelManager = LevelManager.Instance;
        player = Player.Instance;
    }

    //------------------------------------------------------
    //                 COLLISION FUNCTIONS
    //------------------------------------------------------
    private void OnTriggerEnter(Collider thing) {
        
        // Just in case moving enemies or objects accidentally trigger a collision
        HandlePlayerTrigger(thing.gameObject);
    }

    //------------------------------------------------------
    //                 CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------

    private void HandlePlayerTrigger(GameObject obj) {
        if(obj == player.gameObject) {
            levelManager.SetCurrentCheckpoint(transform.position);
        }
    }
}
