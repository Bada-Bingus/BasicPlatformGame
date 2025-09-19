using Unity.Hierarchy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private float inputHorizontal;
    private int maxNumJumps;
    private int numJumps;
    // because this is public, we have access to it in the Unity Editor
    public float horizontalMoveSpeed;
    public float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // I can only get this component because the RigidBody2D is attached to the Player
        //this script is also attached to the player
        rb = GetComponent<Rigidbody2D>();

        numJumps = maxNumJumps;
        //Debug.Log("Hello From Player Controller");
        //^This shows up in the console when the game is ran to make sure that it works!
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
        //Debug.Log("Loopty Loop & Pull");
    }

    private void movePlayerLateral()
    {
        //if A/D/<-/-> are pressed move the player accordingly
        //"Horizontal" is defined in the input section of the project settings
        // the line below will return the following:
        // 0 - No Button Pressed
        // 1 - -> or D Pressed
        // 2 - <- or A Pressed
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        flipPlayerSprite(inputHorizontal);

        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }
    private void flipPlayerSprite(float inputHorizontal)
    {
        //this will flip the player when they move left or right so they don't walk backwards
        if (inputHorizontal > 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
        else if (inputHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void jump ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            numJumps++;
        }
    }

    //Collisions
    //we can use this with all things we want to collide with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision will contain info about the object that the player collided with
        //Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("Ground"))
        {
            numJumps = maxNumJumps;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TriangleCollectible"))
        {
            string fromTriangleCollectible = collision.gameObject.GetComponent<TriangleCollectible>().getTestString();
            Debug.Log(fromTriangleCollectible);
        }
    }
}
