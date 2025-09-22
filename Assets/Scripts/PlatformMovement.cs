using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float movementSpeed;
    public bool horizontalMovement;
    public bool verticalMovement;
    private bool moveLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveLeft = true;
    }
    // Update is called once per frame
    void Update()
    {
        movePlatform();
    }

    private void movePlatform()
    {
        if (horizontalMovement)
        {
            if(moveLeft)
            {
                //this needs to be multiplied by time.deltatime so that we are moving the object
                //based on time, NOT framerate. We don't do this when moving the player
                //because velocity is already in a time-metric
                transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            }
            else
            {
                //this needs to be multiplied by time.deltatime so that we are moving the object
                //based on time, NOT framerate. We don't do this when moving the player
                //because velocity is already in a time-metric
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("WallMoveLeftBound"))
        {
            moveLeft = false;
        }
        else if (collision.gameObject.CompareTag("WallMoveRightBound"))
        {
            moveLeft = true;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //basically if the player contacts the platform, the player is parented to the platform so they move with it
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //if the play stops touching the platform, they are unparented, and are no longer tied to it's movement
            collision.gameObject.transform.SetParent(null);
        }
    }
}
