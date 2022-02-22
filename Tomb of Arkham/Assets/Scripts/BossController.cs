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
    private FoleyManager foleyManager;
    private AudioClip attackSound;
    private AudioClip deathSound;
    private AudioClip teleportSound;

    private GameObject cupNoFireObject;
    private GameObject cupFireObject;
    private GameObject currentAnchor;
    private Animator animator;

    private List<float> healthMilestones = new List<float>();

    // Combat Related

    [SerializeField] private float fireRateOffset;
    private Transform target;
    private bool targetDetected = false;
    private Vector3 lookDirection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCountdown = 0f;
    private bool attacking = false;
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

    private void Start() {
        HandleAnchorSetup();
        SpawnLitAnchor();
        HandleAudioSetup();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleTeleport();
        HandleAnimations();
    }

    private void DeathEvent() {
        foreach(GameObject objs in allProjectiles) {
            Destroy(objs);
        }
        allProjectiles.Clear();
        target = null;
        targetDetected = false;
        attacking = false;
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
    //                AUDIO FUNCTIONS
    //------------------------------------------------------

    private void HandleAudioSetup() {
        foleyManager = FoleyManager.Instance;
        attackSound = (AudioClip)Resources.Load("castSpell");
        teleportSound = (AudioClip)Resources.Load("teleport");
        deathSound = (AudioClip)Resources.Load("bossDeath");
    }

    IEnumerator PlayAttackSound() {
        //Debug.Log("Attacking with Sound");
        
        foleyManager.Play(attackSound.name);
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
    }

    IEnumerator PlayTeleportSound() {
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(teleportSound.name);
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
    }

    IEnumerator PlayDeathSound() {
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(deathSound.name);
        yield return new WaitUntil(() => foleyManager.GetAudioSource().isPlaying == false);
    }

    //------------------------------------------------------
    //                COMBAT FUNCTIONS
    //------------------------------------------------------
    private void AttemptAttack() {
        if(target!=null && targetDetected) {
            // Look at target
            lookDirection = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(lookDirection);
            

            // Fire
            if(fireCountdown <= 0f) {
                attacking = true;
                StartCoroutine(AttackAdjustment());
                
                
                
                fireCountdown = 1f/fireRate;
                //Debug.Log(fireCountdown);
            }
            fireCountdown -= Time.deltaTime;
            //Debug.Log(fireCountdown);
            return;
        }
    }

    IEnumerator AttackAdjustment() {
        attacking = true;
        yield return new WaitForSeconds(fireRateOffset);
        Attack();
    }

    private void Attack() {
        StartCoroutine(PlayAttackSound());
        GameObject newProjectile = (GameObject)Instantiate(projectile, attackOrigin.position, attackOrigin.rotation);
        allProjectiles.Add(newProjectile);
        ProjectileController currentProjectile = newProjectile.GetComponent<ProjectileController>();

        if(currentProjectile != null) {
            currentProjectile.SetTarget(target);
            currentProjectile.SetDamageAmount(bulletDamage);
            return;
        }
        attacking = false;
    }

    public void HurtBoss(float damageAmount)
    {
        currentHealth = currentHealth - damageAmount;
        if(currentHealth <= 0) {
            BossDeath();
        }
    }

    //------------------------------------------------------
    //                ANIMATION FUNCTIONS
    //------------------------------------------------------

        private void HandleAnimations() {
        bool isAttacking = animator.GetBool("isAttacking");
        bool isAlive = animator.GetBool("alive");

        if(attacking && !isAttacking) {
            animator.SetBool("isAttacking", true);
        }
        else if(!attacking && isAttacking) {
            animator.SetBool("isAttacking", false);
        }
        if(!isAlive && currentHealth > 0) {
            animator.SetBool("alive", true);
        }
        else if(isAlive && currentHealth <= 0) {
            animator.SetBool("alive", false);
        }
    }

    //------------------------------------------------------
    //                GENERAL FUNCTIONS
    //------------------------------------------------------
    private void BossDeath() {
        StartCoroutine(PlayDeathSound());
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
                int anchorIndex;
                teleporting = true;
                StartCoroutine(SpawnUnlitAnchor());
                
                StartCoroutine(PlayTeleportSound());
                
                anchorIndex = currentAnchor.transform.GetSiblingIndex() + anchorOffset;
                if(anchorIndex == tPAnchorController.GetChildrenAnchors().Count) {
                    anchorIndex = 0;
                }
                // Bug surrounding this, putting off for now
                // Bug Details: anchor @ anchorIndex is being destroyed and not replaced properly, leading to 
                // inconsistent teleporting
                currentAnchor = tPAnchorController.GetSpecificAnchor(anchorIndex).gameObject;
                
                Vector3 nextTPLoc = currentAnchor.transform.position;
                currentLocation = new Vector3(nextTPLoc.x, currentLocation.y, nextTPLoc.z);
                
                transform.position = currentLocation;
                StartCoroutine(SpawnLitAnchor());
                milestoneIndex++;
                teleporting = false;
            }
        }
        

    }

    private void HandleAnchorSetup() {
        int anchorIndex = thisLevelSetup.GetInitialSpawnLocIndex();
        currentAnchor = tPAnchorController.GetSpecificAnchor(anchorIndex).gameObject;
        cupNoFireObject = Resources.Load<GameObject>("Models/cupNoFire");
        cupFireObject = Resources.Load<GameObject>("Models/cupFire");
    }

    IEnumerator SpawnLitAnchor() {
        GameObject newAnchor = (GameObject) Instantiate(cupFireObject, currentAnchor.transform.position, currentAnchor.transform.rotation);
        int anchorIndex = currentAnchor.transform.GetSiblingIndex();
        tPAnchorController.RemoveSpecificAnchor(currentAnchor.transform);
        Destroy(currentAnchor);
        yield return new WaitUntil(() => currentAnchor == null);
        currentAnchor = newAnchor;
        tPAnchorController.AddAnchor(currentAnchor.transform);
        currentAnchor.transform.SetSiblingIndex(anchorIndex);
    }

    IEnumerator SpawnUnlitAnchor() {
        GameObject newAnchor = (GameObject) Instantiate(cupNoFireObject, currentAnchor.transform.position, currentAnchor.transform.rotation);
        int anchorIndex = currentAnchor.transform.GetSiblingIndex();
        tPAnchorController.RemoveSpecificAnchor(currentAnchor.transform);
        Destroy(currentAnchor);
        yield return new WaitUntil(() => currentAnchor == null);
        currentAnchor = newAnchor;
        tPAnchorController.AddAnchor(currentAnchor.transform);
        currentAnchor.transform.SetSiblingIndex(anchorIndex);
            
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
