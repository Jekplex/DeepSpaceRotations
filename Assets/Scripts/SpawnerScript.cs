using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //float minDistance = 10.5f;
    //float maxDistance = 12.0f;
    //float angle = Random.Range(-Mathf.PI, Mathf.PI);
    //float distance;

    //private void Start()
    //{
    //    distance = Random.Range(minDistance, maxDistance);

    //}


    //private void Update()
    //{
    //    Vector3 spawnPosition = player.Transform.position;
    //    spawnPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
    //    spawnPosition.x = Mathf.Clamp(spawnPosition.x, -spawnValues.x, spawnValues.x);
    //    spawnPosition.y = spawnValues.y;
    //    spawnPosition.z = Mathf.Clamp(spawnPosition.z, -spawnValues.z, spawnValues.z);
    //}

    public float spawnRateInSeconds = 1f;

    public float distanceFromCenter = 10.5f;
    
    public GameObject[] Targets_ToSpawn;
    public int[] Targets_ToSpawn_Percentages;
    private int total;
    [SerializeField] private int currentTargetCount;
    public int maxTargetCount = 25;

    private float spawnRate_;

    private GameObject player;
    private PlayerScript playerScript;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();


        spawnRate_ = spawnRateInSeconds;

        if (Targets_ToSpawn.Length != Targets_ToSpawn_Percentages.Length)
        {
            Debug.Log("ERROR! Array SpawnTargets_Percentages is not the same length as Array spawnTargets");
        }

        foreach (var item in Targets_ToSpawn_Percentages) {
            total += item;
        }

        if (total != 100)
        {
            Debug.Log("ERROR! Total weight of the loot table is not equal to 100");
        }

        //for (int i = 0; i < 100; i++)
        //{
        //    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    float r = 5f;    // distance from center
        //    float angle = Random.Range(0, Mathf.PI * 2);    // Random angle in radians
        //    // sin and cos need value in radians
        //    // full turn aroud circle in radians equal 2*PI ~6.283185 rad
        //    Vector2 pos2d = new Vector2(Mathf.Sin(angle) * r, Mathf.Cos(angle) * r);
        //    sphere.transform.position = new Vector3(pos2d.x, pos2d.y, 0);
        //}
    }

    private void Update()
    {
        currentTargetCount = transform.childCount;
        
        if (!playerScript.playerIsDead)
        {
            spawnRate_ -= Time.deltaTime;

            if (spawnRate_ < 0 && transform.childCount < maxTargetCount)
            {
                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                float r = distanceFromCenter;    // distance from center
                float angle = Random.Range(0, Mathf.PI * 2);    // Random angle in radians
                                                                // sin and cos need value in radians
                                                                // full turn aroud circle in radians equal 2*PI ~6.283185 rad
                Vector2 pos2d = new Vector2(Mathf.Sin(angle) * r, Mathf.Cos(angle) * r);

                // Spawn a random type object.
                //int randomIndex = Random.Range(0, spawnObjs.Length);
                int randomNum = Random.Range(0, 100);
                //Debug.Log(randomNum);

                for (int i = 0; i < Targets_ToSpawn_Percentages.Length; i++)
                {
                    if (randomNum <= Targets_ToSpawn_Percentages[i])
                    {
                        Instantiate(Targets_ToSpawn[i], pos2d, Quaternion.identity, transform);
                        break;
                    }
                    else
                    {
                        randomNum -= Targets_ToSpawn_Percentages[i];
                    }
                }

                // Proposed loot-table.
                // simple, +2, x2, heal// -1 
                // 50 , 43, 2, 5

                // note to self: i feel like something should happen when the player reaches 300 points.
                // maybe implement "a global movement speed" and do a shift somehiow??>? thoughts?


                //if (randomNum >= 0 && randomNum < 50)
                //{
                //    // simple
                //    Instantiate(spawnTargets[0], pos2d, Quaternion.identity, transform);
                //}
                //else if (randomNum >= 50 && randomNum < 93)
                //{
                //    // +2
                //    Instantiate(spawnTargets[1], pos2d, Quaternion.identity, transform);
                //}
                //else if (randomNum > 93 && randomNum < 95)
                //{
                //    //x2
                //    Instantiate(spawnTargets[2], pos2d, Quaternion.identity, transform);
                //}
                //else if (randomNum >= 95)
                //{
                //    // heal
                //    Instantiate(spawnTargets[3], pos2d, Quaternion.identity, transform);
                //}
                

                spawnRateInSeconds = spawnRateInSeconds * 0.997f;

                // Reset SpawnRate
                spawnRate_ = spawnRateInSeconds;
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distanceFromCenter);
    }
}
