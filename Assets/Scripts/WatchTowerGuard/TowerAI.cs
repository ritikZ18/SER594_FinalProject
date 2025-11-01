using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public Transform player;            // assign player transform in inspector
    public Transform towerHead;
    public Transform firePoint;         // position where fireball spawns
    public GameObject fireballPrefab;   // prefab for fireball
    public float detectRange = 200f;
    public float fireRate = 1.5f;
    public float rotationSpeed = 5f;
    private GameObject currentFireball;


    private float nextFireTime = 0f;

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > detectRange) return;

        AimAtPlayer();

        if (currentFireball == null)
        {
            Shoot();
        }
    }


    void AimAtPlayer()
    {
        Vector3 direction = player.position - towerHead.position;
        direction.y = 0; // keep only horizontal rotation
        Quaternion rot = Quaternion.LookRotation(direction);

        towerHead.rotation = Quaternion.Lerp(towerHead.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Only rotate horizontally (tower)
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        if (currentFireball != null) return; // already exists, do not shoot

        Debug.Log("Tower: Shoot called");

        currentFireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        Fireball fb = currentFireball.GetComponent<Fireball>();
        fb.target = player;
        fb.onDestroyed = () => currentFireball = null; // callback when fireball ends
    }


}
