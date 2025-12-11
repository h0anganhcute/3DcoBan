using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;
    [SerializeField] GameObject Bullet;   // Kéo prefab Bullet vào đây
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject Checkpoint;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        StartCoroutine(Dropping());
        StartCoroutine(Attackk());

        // Bắn đạn khi click chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public IEnumerator Dropping()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ani.SetBool("Drop", true);
            yield return new WaitForSeconds(0.3f);
            ani.SetBool("Drop", false);
        }
    }

    public IEnumerator Attackk()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = chuột trái
        {
            ani.SetBool("Attack", true);
            yield return new WaitForSeconds(1.5f);
            ani.SetBool("Attack", false);
        }
    }

    void Shoot()
    {
        // Tạo viên đạn tại vị trí Checkpoint, hướng theo forward của nó
        GameObject bullet = Instantiate(Bullet, Checkpoint.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false; // nếu không muốn đạn bị rơi
            rb.linearVelocity = Checkpoint.transform.forward * bulletSpeed;
        }
    }
}
