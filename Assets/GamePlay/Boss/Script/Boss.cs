using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject player; // Kéo Player vào đây
    private Animator ani;
    [SerializeField] private GameObject Bullet;     // Kéo prefab đạn vào đây
    [SerializeField] private GameObject checkPoint; // Kéo điểm bắn vào đây

    private float attackCooldown = 2.5f;
    private float nextAttackTime = 0f;
    public  int mau = 100;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Boss luôn xoay mặt về phía Player (chỉ xoay ngang, không cúi/ngửa)
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPos);

            // Tấn công mỗi 2.5 giây
            if (Time.time >= nextAttackTime)
            {
                ani.SetTrigger("Kick");
                TanCong(); // gọi hàm bắn ra prefab
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    public void TanCong()
    {
        if (Bullet != null && checkPoint != null)
        {
            // Tạo viên đạn tại vị trí checkPoint, hướng theo forward của nó
            GameObject bullet = Instantiate(Bullet, checkPoint.transform.position, checkPoint.transform.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = checkPoint.transform.forward * 10f; // tốc độ bắn, có thể chỉnh
            }

            // Hủy viên đạn sau 3 giây để tránh rác
            Destroy(bullet, 15f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHit"))
        {
            mau -= 50;
            if (mau <= 0)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("PhiTieu"))
        {
            mau -= 10;
            if (mau <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
