using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator ani;


    void Start()
    {

        ani = GetComponent<Animator>();

    }

    void Update()
    {
        // Nếu chết thì không cho làm gì nữa



        StartCoroutine(Dropping());
    }



    public IEnumerator Dropping()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ani.SetBool("Drop",true);
            yield return new WaitForSeconds(0.3f);
            ani.SetBool("Drop", false);
        }
    }

}
