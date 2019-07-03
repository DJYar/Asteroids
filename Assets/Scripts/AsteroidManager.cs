using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    private bool gameOver = false;
    public void GameOver() => gameOver = true;

    public const float MinSpeed = 5.0f;
    public const float MaxSpeed = 30.0f;

    public float Speed = 17.0f;

    void Start()
    {
        Speed = Random.Range(MinSpeed, MaxSpeed);
    }

    void Update()
    {
        if (gameOver) return;

        transform.Translate( Vector3.forward * Speed * Time.deltaTime );
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.SendMessage("Hit", this, SendMessageOptions.DontRequireReceiver);
        GetComponent<ParticleSystem>().Play();        
    }
}
