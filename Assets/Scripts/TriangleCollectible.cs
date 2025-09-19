using UnityEngine;

public class TriangleCollectible : MonoBehaviour
{
    string test;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public string getTestString()
    {
        test = "Hello from Triangle Collectible";

        return test;
    }
}
