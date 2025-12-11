using UnityEngine;

public class Bay : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;   // Kéo prefab viên đạn vào đây
    [SerializeField] private Transform firePoint;       // Kéo vị trí bắn (empty GameObject) vào đây
    [SerializeField] private float bulletSpeed = 5f;    // tốc độ bắn

    [SerializeField] private float fireRate = 1f;       // khoảng cách giữa các lần bắn (1 giây)
    private float nextFireTime = 0f;

    void Update()
    {
        // Tự động bắn liên tục theo fireRate
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Tạo viên đạn tại firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Thêm lực hoặc velocity để bắn về phía trước
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}
