using UnityEngine;

public class HarpoonProjectileController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = transform.eulerAngles.y;
        harpoonMovement(yRotation);
    }

    private void harpoonMovement(float yRotation)
    {
        if (yRotation == 0)
        {
            rb.linearVelocity = new Vector2(speed * 1, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(speed * -1, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            //make timer for this for 5 seconds
            Destroy(this.gameObject, 5);

        }
    }


}
