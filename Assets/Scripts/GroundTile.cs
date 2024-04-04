using UnityEngine;

public class GroundTile : MonoBehaviour {

    GroundSpawner groundSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleChance = 0.2f;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] float powerUpChance = 1f;
    [SerializeField] Transform spawnPoint;
    private void Start () {
    groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }


    // Metodo per ottenere un punto casuale all'interno del collider
    Vector3 GetRandomPointInColliderRecursive(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }
        point.y = 1;
        return point;
    }

    private void OnTriggerExit (Collider other) {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }


    public void SpawnObstacle () {

        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance) {
            obstacleToSpawn = tallObstaclePrefab;
        } 
        //Choose a random point to spawn the obstacle (random range include il primo ma esclude l'ultimo - perchè? Boh -)
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        //Spawn the obstacle at the position
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);

    }

    public void SpawnPowerUp()
    {      // Genera casualmente se deve essere spawnato un power-up
        if (Random.value < powerUpChance) {

    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            1, // Altezza desiderata del coin
            transform.position.z
        );

        GameObject temp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity, transform);
        }
        }
        else
        {
            return;
        }
    }
  

    public void SpawnCoins()
    {
    int coinsToSpawn = 3; // Una moneta per colonna

    int gridSizeX = 3; // Numero di colonne nella griglia (1/3 della Tile)
    int gridSizeZ = 3; // Numero di righe nella griglia 

    float tileSizeX = GetComponent<Collider>().bounds.size.x / 3f; // Dimensione della Tile sull'asse X
    float tileSizeZ = GetComponent<Collider>().bounds.size.z / 3f; // Dimensione della Tile sull'asse Z

    float offsetX = -tileSizeX / 2 + coinPrefab.transform.localScale.x / 2 - 2; // Offset sull'asse X per centrare la griglia
    float offsetZ = -tileSizeZ / 2 + coinPrefab.transform.localScale.z / 2 - 2; // Offset sull'asse Z per centrare la griglia

    for (int z = 0; z < gridSizeZ; z++)
    {
        // Scegli casualmente una colonna per ogni riga
        int selectedColumn = Random.Range(0, gridSizeX);

        // Calcola la posizione del coin sulla griglia per ogni riga e colonna
        Vector3 spawnPosition = new Vector3(
            transform.position.x + selectedColumn * tileSizeX + offsetX,
            1, // Altezza desiderata del coin
            transform.position.z + z * tileSizeZ + offsetZ
        );

        // Istantia il coin nella posizione calcolata
        GameObject temp = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
    }


    }






    Vector3 GetRandomPointInCollider (Collider collider) {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        if (point != collider.ClosestPoint(point)) {
            point = GetRandomPointInCollider(collider);
        }
        point.y = 1;
        return point;
    }

}
