using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    #region

    // Audio Related
    private FoleyManager foleyManager;
    private AudioClip walkSound;
    private AudioClip jumpStartSound;
    private AudioClip jumpEndSound;
    private AudioClip deathSound;
    private AudioClip idolCollect;
    [SerializeField] private float walkSoundSpeedOffset = .15f;

    // Event Related
    public event Action onDeathEvent;

    // Level Related
    private LevelManager levelManager;
    private int collisionTagIndex;
    private int idolCount = 0;

    // Player Related
    public static Player Instance;

    private CharacterController charController;
    private ControlLayout controlLayout;
    private bool handlingDeath = false;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float playerHealth;
    [SerializeField] private int maxLives;
    [SerializeField] private int livesRemaining = 3;

    // Combat Related
    private KnifeController knife;
    private bool isAttackPressed = false;
    private bool isAttacking = false;

    // Horizontal Movement Related
    private Vector2 horizontalInput;
    private Vector3 horizontalForce;
    private bool isMovePressed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationFactorPerFrame = 1.0f;

    // Vertical Movement Related
    private Vector3 verticalForce;
    private float gravity = -9.8f;
    private float groundedGravity = -.05f;
    private float initialJumpVelocity;
    private bool isJumpPressed = false;
    private bool isJumping = false;

    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime = .5f;

    #endregion

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------
    #region

    public int GetIdolCount(){return idolCount;}
    public void SetIdolCount(int newCount){idolCount = newCount;}

    public bool GetHandlingDeath() {return handlingDeath;}
    public void SetHandlingDeath(bool newValue) {handlingDeath = newValue;}

    #endregion

    //------------------------------------------------------
    //             STANDARD FUNCTIONS
    //------------------------------------------------------
    #region
    private void Awake()
    {
        HandleGeneralSetup();
        HandleMovementSetup();
        HandleJumpSetup();
        HandleAttackSetup();
    }

    private void OnEnable()
    {
        controlLayout.Basics.Move.Enable();

        controlLayout.Basics.Jump.Enable();
        controlLayout.Basics.Jump.performed += OnJump;

        controlLayout.Basics.Attack.Enable();
        controlLayout.Basics.Attack.performed += OnAttack;
    }

    private void Start() {
        Time.timeScale = 1;
        HandleAudioSetup();
    }

    private void Update()
    {
        OutOfLivesCheck();
        HandleRotation();

        // Horizontal Movement handled here
        HandleMovement();

        // Vertical Movement handled here
        HandleGravity();
        HandleJump();

        HandleAttack();

        FallToDeathCheck();
    }
    
    private void OnDisable()
    {
        controlLayout.Basics.Move.Disable();
        controlLayout.Basics.Jump.Disable();

        controlLayout.Basics.Attack.Disable();
    }
    #endregion

    //------------------------------------------------------
    //             COLLISION FUNCTIONS
    //------------------------------------------------------
    #region
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        string collisionTag = hit.gameObject.tag;
        if (collisionTag != null)
        {
            collisionTagIndex = levelManager.SearchTags(collisionTag);
        }

        // Idol collision
        if (collisionTagIndex == levelManager.GetIdolIndex())
        {
            HandleIdolCollision(hit.gameObject);
        }

        // Hazard Collision
        if (collisionTagIndex == levelManager.GetHazardIndex())
        {
            
            HandleHazardCollision(hit.gameObject);
        }
    }

    private void HandleIdolCollision(GameObject obj)
    {
        StartCoroutine(HandleIdolAudio());
        idolCount++;
        Destroy(obj);
    }

    private void HandleHazardCollision(GameObject obj)
    {
        DeathEvent();
        return;
    }
    #endregion

    //------------------------------------------------------
    //          AUDIO FUNCTIONS
    //------------------------------------------------------

    private void HandleAudioSetup() {
        foleyManager = FoleyManager.Instance;
        deathSound = (AudioClip)Resources.Load("deathSound");
        walkSound = (AudioClip)Resources.Load("walk");
        jumpStartSound = (AudioClip)Resources.Load("jumpStart");
        jumpEndSound = (AudioClip)Resources.Load("jumpEnd");
        idolCollect = (AudioClip)Resources.Load("idolPickup");
    }

    IEnumerator HandleMoveAudio(Vector3 currentVelocity) {
        
        yield return new WaitUntil(() => isMovePressed && currentVelocity.magnitude >= walkSoundSpeedOffset && foleyManager.GetAudioSource().isPlaying == false);
        foleyManager.Play(walkSound.name);
        yield return new WaitForSeconds(2);
    }

    IEnumerator HandleJumpAudio() {
        AudioSource audioSource = foleyManager.GetAudioSource();
        yield return new WaitUntil(() => isJumpPressed && !isJumping);
        foleyManager.Play(jumpStartSound.name);
        yield return new WaitUntil(() => !isJumpPressed && !isJumping);
        foleyManager.Play(jumpEndSound.name);
        
    }

    IEnumerator HandleIdolAudio() {
        foleyManager.Play(idolCollect.name);
        yield return new WaitForSeconds(1);
    }

    //------------------------------------------------------
    //          COMBAT FUNCTIONS
    //------------------------------------------------------
    #region
    private void OnAttack(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
    }

    private void HandleAttack()
    {

        if (!isAttacking && isAttackPressed)
        {
            isAttacking = true;
            ShowKnife();
        }
        else if (isAttacking && !isAttackPressed)
        {
            isAttacking = false;
            HideKnife();
        }
    }

    private void ShowKnife()
    {
        knife.transform.gameObject.SetActive(true);
        knife.SetVisible(true);
    }

    private void HideKnife()
    {
        knife.transform.gameObject.SetActive(false);
        knife.SetVisible(false);
    }
    #endregion

    //------------------------------------------------------
    //          MOVEMENT PHYSICS FUNCTIONS
    //------------------------------------------------------
    #region 
    private void OnMove(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>();
        horizontalForce.x = horizontalInput.x;
        horizontalForce.z = horizontalInput.y;
        isMovePressed = horizontalInput.x != 0 || horizontalInput.y != 0;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        isJumpPressed = obj.ReadValueAsButton();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void HandleJump()
    {
        StartCoroutine(HandleJumpAudio());
        if (!isJumping && charController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            verticalForce.y = initialJumpVelocity * .5f;
            horizontalForce.y = initialJumpVelocity * .5f;
        }
        else if (!isJumpPressed && isJumping && charController.isGrounded)
        {
            isJumping = false;
        }
    }

    private void HandleGravity()
    {
        if (charController.isGrounded)
        {
            verticalForce.y = groundedGravity;
            horizontalForce.y = groundedGravity;
        }
        else
        {
            float previousYVelocity = verticalForce.y;
            float newYVelocity = verticalForce.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            verticalForce.y = nextYVelocity;
            horizontalForce.y = nextYVelocity;
        }
    }

    private void HandleRotation()
    {
        Vector3 desiredDirection;

        desiredDirection.x = horizontalForce.x;
        desiredDirection.y = 0f;
        desiredDirection.z = horizontalForce.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovePressed)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(currentRotation, desiredRotation, rotationFactorPerFrame);
        }
    }

    #endregion


    //------------------------------------------------------
    //          GENERAL FUNCTIONS
    //------------------------------------------------------
    #region

    // Public Functions
    public void HurtPlayer(float amount) {
        playerHealth -= amount;

        if(playerHealth <= 0) {
            DeathEvent();
            return;
        }
    }

    public void HandleDeath()
    {
        foleyManager.Play(deathSound.name);
        handlingDeath = true;
        transform.position = levelManager.GetCurrentCheckpoint();
        livesRemaining -= 1;
        playerHealth = maxHealth;
        return;
    }

    public void DeathEvent() {
        if(onDeathEvent != null) {
            onDeathEvent();   
        }
    }

    public void OutOfLivesCheck()
    {
        if(livesRemaining <= 0) {
            levelManager.HandleGameOver();
            livesRemaining = maxLives;
        }
    }

    // Private Functions
    private void HandleGeneralSetup()
    {
        Instance = this;
        levelManager = LevelManager.Instance;
        controlLayout = new ControlLayout();
        charController = GetComponent<CharacterController>();
        livesRemaining = maxLives;
        playerHealth = maxHealth;
        knife = KnifeController.Instance;
        onDeathEvent += HandleDeath;
        HideKnife();
    }

    private void HandleMovementSetup()
    {
        controlLayout.Basics.Move.started += OnMove;
        controlLayout.Basics.Move.canceled += OnMove;
        controlLayout.Basics.Move.performed += OnMove;
    }

    private void HandleJumpSetup()
    {
        controlLayout.Basics.Jump.started += OnJump;
        controlLayout.Basics.Jump.canceled += OnJump;
        SetupJumpVariables();
    }

    private void HandleAttackSetup()
    {
        controlLayout.Basics.Attack.started += OnAttack;
        controlLayout.Basics.Attack.canceled += OnAttack;
    }

    private void HandleMovement() {
        Vector3 currentMovement = horizontalForce * Time.deltaTime * moveSpeed;
        charController.Move(currentMovement);
        StartCoroutine(HandleMoveAudio(currentMovement));
    }

    private void FallToDeathCheck() {
        if (transform.position.y < -10 && levelManager.GetSceneLoaded())
        {
            DeathEvent();
            return;
        }
    }

    #endregion
}
