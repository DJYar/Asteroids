using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    private bool gameOver = false;
    public void GameOver() => gameOver = true;

    public GameObject SpawnPoint;
    public GameObject Player;
    public GameObject Obstacle;

    public float SpawnRate = 1.0f;
    public float SpawnRadius = 5.0f;

    public float DirectionSpan = 25.0f;

    private float spawnTimer = 0.0f;

    void Start()
    {
        spawnTimer = Time.time + 1/SpawnRate;
    }

    void Update()
    {
        if (gameOver) return;

        if (spawnTimer > Time.time) return;

        Vector2 spawnSpan = Random.insideUnitCircle * SpawnRadius;
        Vector3 spawnPoint = new Vector3(
                spawnSpan.x + SpawnPoint.transform.position.x,
                0.0f,
                spawnSpan.y + SpawnPoint.transform.position.z
            );

        GameObject newObstacle = Instantiate(Obstacle, spawnPoint, Quaternion.identity, transform.parent);
        newObstacle.transform.LookAt(Player.transform.position);
        Vector3 rot = newObstacle.transform.rotation.eulerAngles;
        rot.y = Random.Range(rot.y - DirectionSpan, rot.y + DirectionSpan);
        newObstacle.transform.rotation = Quaternion.Euler(rot);

        spawnTimer = Time.time + 1/SpawnRate;
    }
}
