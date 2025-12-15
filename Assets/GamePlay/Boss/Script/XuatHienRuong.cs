using System.Collections;
using UnityEngine;

public class XuatHienRuong : MonoBehaviour
{
    [SerializeField] private GameObject boss;   // Kéo Boss vào đây
    [SerializeField] private GameObject ruong;  // Kéo Rương vào đây
    [SerializeField] private GameObject Wingame; // Kéo Wingame vào đây
    private void Start()
    {
       
    }
    void Update()
    {
        // Nếu Boss đã bị destroy (null) thì bật Rương
        if (boss == null && ruong != null)
        {
            ruong.SetActive(true);
        }
        if (ruong == null)
        {
            StartCoroutine(DelayWinGame());
            Time.timeScale = 0f;
            Wingame.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }
    IEnumerator DelayWinGame()
    {
        yield return new WaitForSeconds(2f);       
    }
}
