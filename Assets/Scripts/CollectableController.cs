using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setRandomSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
    }

    private void moveLeft()
    {
        rb.linearVelocity = new Vector2(speed * -1, 0);
    }

    private void setRandomSpeed()
    {
        float minSpeed = 5;
        float maxSpeed = 10;

        speed = Random.Range(minSpeed, maxSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.CompareTag("HarpoonProjectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
