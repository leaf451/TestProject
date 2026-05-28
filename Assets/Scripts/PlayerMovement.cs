using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f; // How fast the object moves
    private Rigidbody2D rb;
    private Vector2 movementDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    // Update the object’s position and velocity each frame.
    void Update()
    {
        //set movement direction to input direction
        movementDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }

    //updates every 1/60th second
    private void FixedUpdate()
    {
        rb.linearVelocity = movementDir * movementSpeed;
    }

}

