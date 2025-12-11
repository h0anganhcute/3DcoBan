using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;
    [SerializeField] GameObject Bullet;   // Kéo prefab Bullet vào đây
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject Checkpoint;
    [SerializeField] GameObject Drop;
    private float shootDelay = 1f;      // delay 1 giây
    private float nextShootTime = 0f;   // thời điểm được bắn tiếp
    [SerializeField] GameObject kick;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // Gọi Dropping và Attackk bằng phím, không cần StartCoroutine mỗi frame
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(Dropping());

        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Attackk());

        if (Input.GetMouseButtonDown(1))
            StartCoroutine(Kickk());

        // Bắn đạn khi click chuột trái và đủ cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootDelay; // cập nhật thời điểm bắn tiếp theo
        }
    }

    public System.Collections.IEnumerator Dropping()
    {
        ani.SetBool("Drop", true);
        Drop.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        ani.SetBool("Drop", false);
        Drop.SetActive(false);
    }

    public System.Collections.IEnumerator Attackk()
    {
        ani.SetBool("Attack", true);
        yield return new WaitForSeconds(0.8f);
        ani.SetBool("Attack", false);
    }
    public System.Collections.IEnumerator Kickk()
    {
        ani.SetBool("Kick", true);
        kick.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        ani.SetBool("Kick", false);
        kick.SetActive(false);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, Checkpoint.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = Checkpoint.transform.forward * bulletSpeed; // đúng là velocity
        }
    }
}
