using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float force = 10f; // lực bay
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.linearVelocity = transform.forward * force; // bay về phía trước
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb != null)
        {
            // Lấy hướng ngược lại từ điểm va chạm
            Vector3 bounceDir = (transform.position - collision.contacts[0].point).normalized;

            // Đặt lại velocity để bay ngược lại
            rb.linearVelocity = bounceDir * force;
        }
    }
}
