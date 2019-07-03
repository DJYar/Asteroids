using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private bool gameOver = false;
    public void GameOver() => gameOver = true;

    public float BulletSpeed = 50.0f;

    public float LifeTime = 5.0f;
    private float lifeElapsed = 0.0f;

    void Start()
    {
        lifeElapsed = Time.time + LifeTime;
    }

    void Update()
    {
        if (gameOver) return;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + BulletSpeed*Time.deltaTime
            );

        if (Time.time > lifeElapsed)
            Destroy(gameObject);
    }

    public void Hit(AsteroidManager obstacle)
    {
        Destroy(gameObject);
    }
}
