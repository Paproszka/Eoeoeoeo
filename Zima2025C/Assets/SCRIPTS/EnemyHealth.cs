using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject deathParticlePrefRef;
    public GameObject coinPrefRef;

    public float hp = 100f;
    public bool dead = false;

    public void TakeDamage(float damage)
    {
        if (dead)
            return;

        hp -= damage;

        if (hp <= 0.0f)
        {
            dead = true;
            Instantiate(deathParticlePrefRef, transform.position, transform.rotation);
            Instantiate(coinPrefRef, transform.position, transform.rotation);
            GetComponent<CapsuleCollider>().enabled = false;
            Invoke("DestroyOnDeath", 5);
        }
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject);
    }
}
