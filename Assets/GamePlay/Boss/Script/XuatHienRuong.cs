using UnityEngine;

public class XuatHienRuong : MonoBehaviour
{
    [SerializeField] private GameObject boss;   // Kéo Boss vào đây
    [SerializeField] private GameObject ruong;  // Kéo Rương vào đây

    void Update()
    {
        // Nếu Boss đã bị destroy (null) thì bật Rương
        if (boss == null && ruong != null)
        {
            ruong.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }
}
