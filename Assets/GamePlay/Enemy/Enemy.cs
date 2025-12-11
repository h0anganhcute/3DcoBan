using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int mau = 10;
    Animator ani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHit"))
        {
            mau -= 5;
            if(mau <= 0)
            {
                ani.SetTrigger("Death");
                Destroy(gameObject,2.5f);
            }
        }
        if (other.gameObject.CompareTag("PhiTieu"))
        {
            mau -= 5;
            if (mau <= 0)
            {
                ani.SetTrigger("Death");
                Destroy(gameObject,2.5f);
            }
        }
    }
}
