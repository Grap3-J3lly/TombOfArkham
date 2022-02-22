using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    public static KnifeController Instance;
    private LevelManager levelManager;
    private bool isVisible = true;
    private FoleyManager foleyManager;
    private AudioClip swordHitSound;
    private AudioClip swordSwingSound;
    
    [SerializeField] private float knifeDamage;

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public bool GetIsVisible() {
        return isVisible;
    }

    public void SetVisible(bool newValue) {
        isVisible = newValue;
    }

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        levelManager = LevelManager.Instance;
        Instance = this;
    }

    private void Start() {
        HandleAudioSetup();
    }

    private void Update() {
        CheckForSwing();
    }

    //------------------------------------------------------
    //                  COLLIDER FUNCTIONS
    //------------------------------------------------------

    private void OnTriggerEnter(Collider thing) {
        if(thing.gameObject.tag == "Boss") {
            StartCoroutine(HandleSwordHit());
            thing.gameObject.GetComponent<BossController>().HurtBoss(knifeDamage);
        }
    }
    //------------------------------------------------------
    //                  AUDIO FUNCTIONS
    //------------------------------------------------------

    private void HandleAudioSetup() {
        foleyManager = FoleyManager.Instance;
        swordHitSound = (AudioClip) Resources.Load("swordHit");
        swordSwingSound = (AudioClip) Resources.Load("swordSwing");
    }

    private void CheckForSwing() {
        if(isVisible) {
            StartCoroutine(HandleSwordSwing());
        }
    }

    IEnumerator HandleSwordSwing() {
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(swordSwingSound.name);
    }

    IEnumerator HandleSwordHit() {
        foleyManager.Play(swordHitSound.name);
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
    }
}
