using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
         if(Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
         if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
         if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        
    }
}
