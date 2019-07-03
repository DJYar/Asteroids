using UnityEngine;

public class ShipMovementController : MonoBehaviour
{
    private bool gameOver = false;

    public float MaxSpeed = 35.0f;
    public float MaxRollAngle = 25.0f;
    public float StartSpeed = 5.0f;
    public float RollSpeed = 10.0f;

    public float Speed = 0.0f;
    public float Shift = 0.0f;
    public float RollAngle = 0.0f;

    public float Velocity = 1.0f;
    public float Fading = 0.5f;
    public float Shifting = 10.0f;

    public GameObject Shell;

    // 2rpm = 0.5 secs delay
    public float ShootRate = 2.0f;
    private float shootTimer = 0.0f;

    void MoveForward()
    {
        if (Speed == 0.0f)
        {
            Speed = StartSpeed;
            return;
        }

        if (Mathf.Abs(Speed) >= MaxSpeed) return;
        Speed += Velocity * Time.deltaTime;
    }

    void MoveBackward()
    {
        if (Speed == 0.0f)
        {
            Speed = -StartSpeed;
            return;
        }

        if (Mathf.Abs(Speed) >= MaxSpeed) return;
        Speed -= Velocity * Time.deltaTime;
    }

    void MoveLeft()
    {
        RollAngle = Mathf.Lerp(RollAngle, 25.0f, Time.deltaTime * RollSpeed);
        Shift = Time.deltaTime * -Shifting;
    }

    void MoveRight()
    {
        RollAngle = Mathf.Lerp(RollAngle, -25.0f, Time.deltaTime * RollSpeed);
        Shift = Time.deltaTime * Shifting;
    }

    void FadeX()
    {
        if (Speed == 0.0f) return;

        float sign = Mathf.Sign(Speed);
        float t = Mathf.Abs(Speed) - Fading * Time.deltaTime;
        float max = Mathf.Max(0, t);
        Speed = sign * max;
    }

    void FadeY()
    {
        RollAngle = Mathf.Lerp(RollAngle, 0, Time.deltaTime * RollSpeed);
        Shift = 0.0f;
    }

    void Start()
    {
    }

    void Update()
    {
        if (gameOver) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        else
        {
            FadeX();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else
        {
            FadeY();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Z)) Hit(null);

        transform.localPosition = new Vector3(transform.localPosition.x + Shift,
            transform.localPosition.y,
            transform.localPosition.z + Speed);

        transform.localRotation = Quaternion.Euler(0, 0, RollAngle);
    }

    private void Shoot()
    {
        if (shootTimer > Time.time) return;

        var newShell = Instantiate(Shell, transform.position, Shell.transform.rotation, transform.parent);
        shootTimer = Time.time + 1 / ShootRate;
    }

    public void Hit(AsteroidManager obstacle)
    {
        gameOver = true;
        GetComponent<ParticleSystem>().Play();
        transform.parent.gameObject.BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
    }
}
