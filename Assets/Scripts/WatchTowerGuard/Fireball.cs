using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform target;
    public float speed = 15f;
    public float lifeTime = 5f;
    public float damage = 20f;
    public System.Action onDestroyed;


    void Start()
    {
        Invoke(nameof(SelfDestruct), lifeTime);
    }

    void SelfDestruct()
    {
        onDestroyed?.Invoke();
        Destroy(gameObject);
    }


    void Update()
    {
        if (!target)
        {
            onDestroyed?.Invoke();
            Destroy(gameObject);

            return;
        }

        Vector3 targetPos = target.position + new Vector3(0, 1.5f, 0);
        Vector3 dir = (targetPos - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.LookAt(target);

        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Example: player health script call
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
            onDestroyed?.Invoke();
            Destroy(gameObject);

            return;
        }

        if (!other.CompareTag("Enemy") && !other.CompareTag("EnemyProjectile"))
        {
        onDestroyed?.Invoke();
        Destroy(gameObject);

        }
    }
}

