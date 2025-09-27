using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    // these will be used to determine where to spawn collectables
    //i'll get these values from the empty game objects we created by referencing thier x & y values
    public GameObject lowestYspawn;
    public GameObject highestYspawn;

    //these are public variables to allow me to drag and drop our prefabs
    public GameObject redCollectable;
    public GameObject purpleCollectable;
    public GameObject goldCollectable;

    //random number to determine which collectable to spawn
    private int randomPrefab;

    //which collectable to spawn
    private GameObject collectableToSpawn;

    //need a reference to time so we can determine how often to spawn a collectable
    private float time;

    //how long to wait before spawning collectables
    public float delay;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //add to time. see how much time has passed since last frame
        time += Time.deltaTime;

        if (time >= delay)
        {
            spawnObject();
            //reset time so the delay is set for the next object to spawn
            time = 0f;
        }
    }

    private void spawnObject()
    {
        //get a random number to determine which object to spawn.
        //The max number in Random.Range is exclusive (up to, but not including)
        //(in this case it can give 0, 1, or 2, but not 3)
        randomPrefab = Random.Range(0, 3);

        if (randomPrefab == 0)
        {
            //Instantiate means spawn
            collectableToSpawn = Instantiate(redCollectable);
        }
        else if (randomPrefab == 1)
        {
            collectableToSpawn = Instantiate(purpleCollectable);
        }
        else if (randomPrefab == 2)
        {
            collectableToSpawn = Instantiate(goldCollectable);
        }

        collectableToSpawn.transform.position = new Vector2(lowestYspawn.transform.position.x, Random.Range(lowestYspawn.transform.position.y, highestYspawn.transform.position.y));

    }


   
}
