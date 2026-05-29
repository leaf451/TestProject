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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Flip();
        }

        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            Debug.Log("Player detected");
            if (cooldownTimer >= attackCooldown)
            {
                Debug.Log("Attack triggered");
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");

            }

        }
        
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.localScale = new Vector3(
            facingRight ? 1 : -1,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private Vector2 GetFacingDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

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





