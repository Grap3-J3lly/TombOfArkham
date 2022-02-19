using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    [SerializeField] private LevelSetup thisLevelSetup;
    [SerializeField] private TPAnchorController tPAnchorController;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject hiddenKey;

    private Player player;
    private Vector3 spawnLocation;
    private Vector3 currentLocation;
    private int milestoneIndex = 0;
    private int anchorOffset = 1;
    private bool teleporting = false;
    

    private List<float> healthMilestones = new List<float>();

    // Combat Related
    private Transform target;
    private bool targetDetected = false;
    private Vector3 lookDirection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float fireRate = 1f;
    private float fireCountdown = 0f;
    private List<GameObject> allProjectiles = new List<GameObject>();


    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public float GetCurrentHealth(){return currentHealth;}
    public void SetCurrentHealth(float newAmount){currentHealth = newAmount;} 

    public bool GetTargetDetected() {return targetDetected;}  
    public void SetTargetDetected(bool newValue) {targetDetected = newValue;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------
    private void Awake()
    {
        HandleSpawn();
        HandleHPMilestones();
    }

    private void Update()
    {
        HandleTeleport();
    }

    private void DeathEvent() {
        foreach(GameObject objs in allProjectiles) {
            Destroy(objs);
        }
        allProjectiles.Clear();
    }

    //------------------------------------------------------
    //                COLLISION FUNCTIONS
    //------------------------------------------------------

    private void OnTriggerEnter(Collider thing) {
        HandlePlayerTrigger(thing.gameObject, true);
    }

    private void OnTriggerStay(Collider thing) {
        HandlePlayerTrigger(thing.gameObject, true);
        AttemptAttack();
        return;
    }

    private void OnTriggerExit(Collider thing) {
        HandlePlayerTrigger(thing.gameObject, false);
        return;
    }

    //------------------------------------------------------
    //                CUSTOM COMBAT FUNCTIONS
    //------------------------------------------------------
    private void AttemptAttack() {
        if(target!=null && targetDetected) {
            // Look at target
            lookDirection = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(lookDirection);

            // Fire
            if(fireCountdown <= 0f) {
                Attack();
                fireCountdown = 1f/fireRate;
            }
            fireCountdown -= Time.deltaTime;
            return;
        }
    }

    private void Attack() {
        GameObject newProjectile = (GameObject)Instantiate(projectile, attackOrigin.position, attackOrigin.rotation);
        allProjectiles.Add(newProjectile);
        ProjectileController currentProjectile = newProjectile.GetComponent<ProjectileController>();

        if(currentProjectile != null) {
            currentProjectile.SetTarget(target);
            currentProjectile.SetDamageAmount(bulletDamage);
            return;
        }
    }

    public void HurtBoss(float damageAmount)
    {
        currentHealth = currentHealth - damageAmount;
        if(currentHealth <= 0) {
            BossDeath();
        }
    }

    //------------------------------------------------------
    //                CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------
    private void BossDeath() {
        hiddenKey.SetActive(true);
        hiddenKey.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void HandlePlayerTrigger(GameObject obj, bool enterTrigger) {
        if(obj == player.gameObject && enterTrigger) {
            targetDetected = true;
            target = obj.transform;
        }
        else if(!enterTrigger) {
            targetDetected = false;
        }
        return;
    }

    private void HandleTeleport()
    {
        if (currentHealth > 0)
        {
            // If not actively teleporting and health below the set milestone, teleport to location of next TP Anchor
            // Health Milestones do NOT include 100%, so milestone[0] is the maxhealth - (maxhealth/TPAnchorCount)
            // Since boss starts on TPAnchor[0], skip over that index with anchorOffset. 
            if (!teleporting && currentHealth < healthMilestones[milestoneIndex])
            {
                teleporting = true;

                Vector3 nextTPLoc = tPAnchorController.GetSpecificAnchor(milestoneIndex + anchorOffset).position;
                currentLocation = new Vector3(nextTPLoc.x, currentLocation.y, nextTPLoc.z);
                
                transform.position = currentLocation;
                milestoneIndex++;
                teleporting = false;
            }
        }
        

    }

    private void HandleSpawn()
    {
        player = Player.Instance;
        player.onDeathEvent += DeathEvent;
        spawnLocation = thisLevelSetup.GetBossSpawnLocation();
        transform.position = spawnLocation;
        currentLocation = spawnLocation;
        currentHealth = maxHealth;
        hiddenKey.SetActive(false);
    }

    private void HandleHPMilestones()
    {
        float mileStoneStep = maxHealth / tPAnchorController.GetChildrenAnchors().Count;
        float nextMilestone = 0f;
        for (int index = 0; index < tPAnchorController.GetChildrenAnchors().Count; index++)
        {
            healthMilestones.Add(nextMilestone);
            nextMilestone += mileStoneStep;
        }
        healthMilestones.Reverse();
    }

}
