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
    private FoleyManager foleyManager;
    private AudioClip openDoorSound;

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public int GetReqKeyCount() {return requiredKeyCount;}
    public void SetReqKeyCount(int newCount) {requiredKeyCount = newCount;}

    //------------------------------------------------------
    //                 STANDARD FUNCTIONS
    //------------------------------------------------------
    private void Awake()
    {
        levelManager = LevelManager.Instance;
        player = Player.Instance;        
    }

    private void Start() {
        HandleAudioSetup();
    }

    //------------------------------------------------------
    //                 COLLISION FUNCTIONS
    //------------------------------------------------------
    private void OnTriggerEnter(Collider thing) {

        HandlePlayerTrigger(thing.gameObject);
    }

    //------------------------------------------------------
    //                 AUDIO FUNCTIONS
    //------------------------------------------------------

    private void HandleAudioSetup() {
        foleyManager = FoleyManager.Instance;
        openDoorSound = (AudioClip)Resources.Load("openCloseDoor");
    }

    IEnumerator HandleOpenDoorSound() {
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(openDoorSound.name);
    }

    //------------------------------------------------------
    //                 GENERAL FUNCTIONS
    //------------------------------------------------------
    private void Unlock() {
        if(player.GetIdolCount() == requiredKeyCount) {
            StartCoroutine(HandleOpenDoorSound());
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
