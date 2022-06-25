using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBlock : MonoBehaviour
{
    public bool rayBomb = false;
    [Header("Анимация разрушения камня")]
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rayBomb)
        {
            anim.SetTrigger("Boom");
            Destroy(gameObject, 2f);
        }
    }
}
