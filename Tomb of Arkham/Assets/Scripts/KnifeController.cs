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
    
    [SerializeField] private float knifeDamage;

    //------------------------------------------------------
    //                  CUSTOM FUNCTIONS
    //------------------------------------------------------

    public bool GetIsVisible() {
        return isVisible;
    }

    public void SetVisible(bool newValue) {
        isVisible = newValue;
    }

    private void Awake() {
        levelManager = LevelManager.Instance;
        Instance = this;
    }

    private void OnTriggerEnter(Collider thing) {
        if(thing.gameObject.name == "Boss Object") {
            thing.gameObject.GetComponent<BossController>().HurtBoss(knifeDamage);
        }
    }

}
