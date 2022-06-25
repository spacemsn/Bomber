using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private GameObject myPerson;
    private Animator anim;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            myPerson = GameObject.FindGameObjectWithTag("Player");
        }
        var character = myPerson.GetComponent<Character>();
        agent.speed = character.speed;
    }

    void Update()
    {
        if (myPerson != null)
        {
            agent.destination = myPerson.transform.position;
            anim.SetBool("isRun", true);
        }
        else if (myPerson == null) anim.SetBool("isRun", false);
    }

    public void liveEnemy()
    {
        Destroy(gameObject);
    }
}
