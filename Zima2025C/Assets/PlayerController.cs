
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerTransform;
    public Transform cameraTransform;

    public float speed;
    public Vector3 upForce;

    public GameObject prefab;
    public Vector3 distance;

    public float gameTime;
    public float hp;
    public int coins;

    public float SpeedBoostTime;
    public float DefaultMaxVelocity;
    public float SpeedBoostVelocity = 12;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();

        hp = 100;
        DefaultMaxVelocity = rb.maxAngularVelocity;
    }

    void Update()
    {
        gameTime = Time.timeSinceLevelLoad;
        speed = DefaultMaxVelocity;
        rb.maxAngularVelocity = DefaultMaxVelocity;
        if (SpeedBoostTime > 0 )
        {
            SpeedBoostTime -= Time.deltaTime;
            if (SpeedBoostTime < 0)
            {
                SpeedBoostTime = 0;
            }
            speed = SpeedBoostVelocity;
            rb.maxAngularVelocity = SpeedBoostVelocity;
        }
        rb.AddTorque(speed * Time.deltaTime * Input.GetAxis("Vertical") * cameraTransform.right);
        rb.AddTorque(speed * Time.deltaTime * Input.GetAxis("Horizontal") * Vector3.Cross(Vector3.up, cameraTransform.right));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(upForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(prefab, playerTransform.position + distance, Quaternion.identity);

            foreach (var collider in Physics.OverlapSphere(playerTransform.position, 3.0f))
            {
                if (collider.CompareTag("Enemy"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        //if (Time.timeSinceLevelLoad > 20)
        //{
        //    if (coins < 5)
        //    {
        //        SceneManager.LoadScene("Demo");
        //    }
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
        }
    }

    public void AddDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            SceneManager.LoadScene("Demo");
        }
    }

    public void ActivateSpeedBoost()
    {
        SpeedBoostTime = 5;
    }
}