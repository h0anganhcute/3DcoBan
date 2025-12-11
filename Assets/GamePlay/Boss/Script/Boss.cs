using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject player; // Kéo Player vào đây
    private Animator ani;
    [SerializeField] private GameObject Bullet;     // Kéo prefab đạn vào đây
    [SerializeField] private GameObject checkPoint; // Kéo điểm bắn vào đây

    private float attackCooldown = 2.5f;
    private float nextAttackTime = 0f;
    public int mau = 100;

    private bool isDead = false; // cờ kiểm tra Boss đã chết chưa

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return; // nếu Boss đã chết thì dừng mọi hoạt động

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
        if (isDead) return; // Boss chết thì không bắn nữa

        if (Bullet != null && checkPoint != null)
        {
            GameObject bullet = Instantiate(Bullet, checkPoint.transform.position, checkPoint.transform.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = checkPoint.transform.forward * 10f; // sửa lại từ linearVelocity -> velocity
            }

            Destroy(bullet, 15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return; // Boss chết rồi thì không nhận sát thương nữa

        if (other.gameObject.CompareTag("PlayerHit"))
        {
            mau -= 50;
        }
        else if (other.gameObject.CompareTag("PhiTieu"))
        {
            mau -= 10;
        }

        if (mau <= 0)
        {
            isDead = true; // đánh dấu Boss đã chết
            ani.SetTrigger("Death");
            Destroy(gameObject, 5f); // hủy Boss sau 5 giây
        }
    }
}
