using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private float inputHorizontal;
    // because this is public, we have access to it in the Unity Editor
    public float horizontalMoveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // I can only get this component because the RigidBody2D is attached to the Player
        //this script is also attached to the player
        rb = GetComponent<Rigidbody2D>();

        //Debug.Log("Hello From Player Controller");
        //^This shows up in the console when the game is ran to make sure that it works!
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        //jump();
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

        rb.linearVelocity = new Vector2(horizontalMoveSpeed * inputHorizontal, rb.linearVelocity.y);
    }
}
