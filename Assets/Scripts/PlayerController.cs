using NUnit.Framework;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private float inputHorizontal;
    private int maxNumJumps;
    private int numJumps;
    private bool hasHarpoonGun = false;
    // because this is public, we have access to it in the Unity Editor
    public float horizontalMoveSpeed;
    public float jumpForce;

    public GameObject doubleJumpHatLocation;
    public GameObject harpoonLocation;

    //Harpoon Projectile Stuff
    public GameObject projectilePrefab;
    public Transform firepoint;
    private GameObject projectile;
    private bool facingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // I can only get this component because the RigidBody2D is attached to the Player
        //this script is also attached to the player
        rb = GetComponent<Rigidbody2D>();

        maxNumJumps = 1;
        numJumps = 1;
        //Debug.Log("Hello From Player Controller");
        //^This shows up in the console when the game is ran to make sure that it works!
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
        shoot();
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

        //velocity is time-based not frame-based, this prevents movement fuckery when framerate differs
        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }
    private void flipPlayerSprite(float inputHorizontal)
    {
        //this will flip the player when they move left or right so they don't walk backwards
        if (inputHorizontal > 0)
        {
            transform.eulerAngles = new Vector3 (0, 0, 0);
            facingRight = true;
        }
        else if (inputHorizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingRight = false;
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

    private void shoot ()
    {
        if (Input.GetKeyDown(KeyCode.P) && hasHarpoonGun)
        {
            Quaternion rotation;

            if (facingRight)
            {
                rotation = Quaternion.Euler(0, 0, 0);
                projectile = Instantiate(projectilePrefab, firepoint.position, rotation);
            }
            else
            {
                rotation = Quaternion.Euler(0, -180, 0);
                projectile = Instantiate(projectilePrefab, firepoint.position, rotation);
            }


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
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Check if the player is landing on top
                if (contact.normal.y > 0.5f)
                {
                    numJumps = 1;
                    break; // No need to check the rest of the contacts
                }
            }
        }
        else if(collision.gameObject.CompareTag("OBbottom"))
        {
            SceneManager.LoadScene("DaScene");
        }
    }

    //triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            GameObject hat = collision.gameObject;
            equipDoubleJumpHat(hat);
            maxNumJumps = 2;
        }
        else if (collision.gameObject.CompareTag("HarpoonLauncher"))
        {
            GameObject harpoon = collision.gameObject;
            equipHarpoonGun(harpoon);
            hasHarpoonGun = true;
        }
    }

    private void equipDoubleJumpHat(GameObject hat)
    {
        hat.transform.position = doubleJumpHatLocation.transform.position;
        hat.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void equipHarpoonGun(GameObject harpoon)
    {
        harpoon.transform.position = harpoonLocation.transform.position;
        harpoon.gameObject.transform.SetParent(this.gameObject.transform);
    }

}
