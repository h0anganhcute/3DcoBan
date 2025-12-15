using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Animator ani;
    [SerializeField] GameObject Bullet;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject Checkpoint;
    [SerializeField] GameObject Drop;
    [SerializeField] GameObject kick;
    AudioSource audi;
    private float shootDelay = 1f;
    private float nextShootTime = 0f;
    [SerializeField] private int mau = 10;
    private bool isDead = false;

    [SerializeField] Transform startPosition; // vị trí ban đầu
    [SerializeField] MonoBehaviour PlayerScript; // script khác để tắt khi chết
    [SerializeField] GameObject Ruong;

    [SerializeField] GameObject panel; // Panel UI sẽ hiện khi rương bị phá

    void Start()
    {
        ani = GetComponent<Animator>();
        audi = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return; // nếu đã chết thì dừng mọi hoạt động

        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(Dropping());

        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Attackk());

        if (Input.GetMouseButtonDown(1))
            StartCoroutine(Kickk());

        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootDelay;
        }

        // Kiểm tra nếu rương đã bị phá hủy
        if (Ruong == null && panel != null && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    IEnumerator Dropping()
    {
        PlayerScript.enabled = false;
        ani.SetBool("Drop", true);
        Drop.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        
        ani.SetBool("Drop", false);
        Drop.SetActive(false);
        PlayerScript.enabled = true;
    }

    IEnumerator Attackk()
    {
        PlayerScript.enabled = false;
        ani.SetBool("Attack", true);
        audi.Play();
        yield return new WaitForSeconds(1f);
        
        ani.SetBool("Attack", false);
        PlayerScript.enabled = true;

    }

    IEnumerator Kickk()
    {
        PlayerScript.enabled = false;
        ani.SetBool("Kick", true);
        kick.SetActive(true);
        audi.Play();
        yield return new WaitForSeconds(1.6f);
        
        ani.SetBool("Kick", false);
        kick.SetActive(false);
        PlayerScript.enabled = true;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, Checkpoint.transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = Checkpoint.transform.forward * bulletSpeed; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("DameBoss"))
        {
            ani.SetTrigger("NhanSt");
            mau -= 1;
            if (mau <= 0)
            {
                isDead = true;
                ani.SetTrigger("Death"); // gọi animation chết nếu có

                // Tắt script khác nếu có
                if (PlayerScript != null)
                    PlayerScript.enabled = false;
                StartCoroutine(Respawn());
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.6f);
        mau = 10;
        isDead = false;
        transform.position = startPosition.position;
        if (PlayerScript != null)
            PlayerScript.enabled = true;
    }
}
