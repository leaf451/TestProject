using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float detectionRange = 2f;
    [SerializeField] private Vector2 detectionOffset = new Vector2(3f, 0f);
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private bool facingRight = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //calls flip function
        if (Input.GetKeyDown(KeyCode.F))
        {
            Flip();
        }

        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");

            }

        }
        
    }

    
    //flips enemy along with detection radius
    public void Flip()
    {
        facingRight = !facingRight;

        transform.localScale = new Vector3(
            facingRight ? 1 : -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    
    //ensures enemy is facing the correct direction with detection radius
    private Vector2 GetFacingDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    
    //funtion for detecting a player
    private bool PlayerInSight()
    {
        Vector2 direction = GetFacingDirection();

        Vector2 detectPosition = (Vector2)transform.position;

        // apply X offset based on facing direction
        detectPosition += new Vector2(direction.x * detectionOffset.x, detectionOffset.y);

        Collider2D hit = Physics2D.OverlapCircle(
            detectPosition,
            detectionRange,
            playerLayer
        );

        return hit != null;
    }

    
    //draws detection area
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector2 direction =
            facingRight ? Vector2.right : Vector2.left;

        Vector2 detectPosition = (Vector2)transform.position;

        detectPosition += new Vector2(direction.x * detectionOffset.x, detectionOffset.y);

        Gizmos.DrawWireSphere(detectPosition, detectionRange);
    }
}





