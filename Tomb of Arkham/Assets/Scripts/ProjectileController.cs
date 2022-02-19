using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    private Transform target;
    [SerializeField] private float speed = 70f;
    [SerializeField] private float damageAmount = 70f;
    private Vector3 direction;
    private Player player;
    private bool targetDeath;

    //------------------------------------------------------
    //              GETTERS/SETTERS
    //------------------------------------------------------

    public Transform GetTarget() {return target;}
    public void SetTarget(Transform newTarget) {target = newTarget;}

    public float GetDamageAmount() {return damageAmount;}
    public void SetDamageAmount(float newAmount) {damageAmount = newAmount;}

    //------------------------------------------------------
    //              STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        player = Player.Instance;
        //player.onDeathEvent += TargetDeath;
    }

    private void Update() {
        
        LostTarget();
        ApproachTarget();
    }
    
    //------------------------------------------------------
    //          CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------

    private void LostTarget() {
        if(target == null) {
            DestroyImmediate(gameObject);
            return;
        }
    }

    private void ApproachTarget() {
        if(target != null) {
            direction = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if(direction.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
            }
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            return;
        }
    }

    private void HitTarget() {
        
        player.HurtPlayer(damageAmount);
        Destroy(gameObject);
        return;
    }

    private void TargetDeath() {
        Destroy(gameObject);
    }
}
