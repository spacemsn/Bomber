using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [Header("Бомба")]
    [SerializeField] private float damage;
    [SerializeField] private float second;
    [SerializeField] private float maxDistanceRay = 3f;
    private GameObject _character;
    private Ray rayForward;
    private Ray rayRight;
    private Ray rayBack;
    private Ray rayLeft;
    private RaycastHit hitForward;
    private RaycastHit hitBack;
    private RaycastHit hitRight;
    private RaycastHit hitLeft;

    [Header("Partical System")]
    [SerializeField] private GameObject effect;
    [SerializeField] private float waveFall;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _character = GameObject.FindGameObjectWithTag("Player");
        }

        var character = _character.GetComponent<Character>();
        damage = character.hp;
    }

    private void blockBoom()
    {
        if (hitForward.transform != null && hitForward.transform.GetComponent<BrokenBlock>())
        {
            hitForward.transform.GetComponent<BrokenBlock>().rayBomb = true;
        }
        if (hitBack.transform != null && hitBack.transform.GetComponent<BrokenBlock>())
        {
            hitBack.transform.GetComponent<BrokenBlock>().rayBomb = true;
        }
        if (hitRight.transform != null && hitRight.transform.GetComponent<BrokenBlock>())
        {
            hitRight.transform.GetComponent<BrokenBlock>().rayBomb = true;
        }
        if (hitLeft.transform != null && hitLeft.transform.GetComponent<BrokenBlock>())
        {
            hitLeft.transform.GetComponent<BrokenBlock>().rayBomb = true;
        }
    }

    private void CharacterLive()
    {
        if (hitForward.transform != null && hitForward.transform.GetComponent<Character>())
        {
            hitForward.transform.GetComponent<Character>().CharacterHP(damage);
        }
        if (hitBack.transform != null && hitBack.transform.GetComponent<Character>())
        {
            hitBack.transform.GetComponent<Character>().CharacterHP(damage);
        }
        if (hitRight.transform != null && hitRight.transform.GetComponent<Character>())
        {
            hitRight.transform.GetComponent<Character>().CharacterHP(damage);
        }
        if (hitLeft.transform != null && hitLeft.transform.GetComponent<Character>())
        {
            hitLeft.transform.GetComponent<Character>().CharacterHP(damage);
        }
    }

    private void EnemyLive()
    {
        if (hitForward.transform != null && hitForward.transform.GetComponent<Enemy>())
        {
            hitForward.transform.GetComponent<Enemy>().liveEnemy();
        }
        if (hitBack.transform != null && hitBack.transform.GetComponent<Enemy>())
        {
            hitBack.transform.GetComponent<Enemy>().liveEnemy();
        }
        if (hitRight.transform != null && hitRight.transform.GetComponent<Enemy>())
        {
            hitRight.transform.GetComponent<Enemy>().liveEnemy();
        }
        if (hitLeft.transform != null && hitLeft.transform.GetComponent<Enemy>())
        {
            hitLeft.transform.GetComponent<Enemy>().liveEnemy();
        }
    }

    private void Ray()
    {
        rayForward = new Ray(transform.position, Vector3.forward);
        rayBack = new Ray(transform.position, Vector3.back);
        rayRight = new Ray(transform.position, Vector3.right);
        rayLeft = new Ray(transform.position, Vector3.left);
    }

    private void DrawRay()
    {
        if (Physics.Raycast(rayForward, out hitForward, maxDistanceRay))
        {
            Debug.DrawRay(rayForward.origin, rayForward.direction * maxDistanceRay, Color.blue);
        }
        if (Physics.Raycast(rayBack, out hitBack, maxDistanceRay))
        {
            Debug.DrawRay(rayBack.origin, rayBack.direction * maxDistanceRay, Color.blue);
        }
        if (Physics.Raycast(rayRight, out hitRight, maxDistanceRay))
        {
            Debug.DrawRay(rayRight.origin, rayRight.direction * maxDistanceRay, Color.blue);
        }
        if (Physics.Raycast(rayLeft, out hitLeft, maxDistanceRay))
        {
            Debug.DrawRay(rayLeft.origin, rayLeft.direction * maxDistanceRay, Color.blue);
        }
    }

    private void Update()
    {
        Ray();
        DrawRay();
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
   {
        yield return new WaitForSeconds(second);
        Instantiate(effect, transform.position, transform.rotation);
        blockBoom();
        CharacterLive();
        EnemyLive();
        for (int i = 1; i < waveFall; i++)
        {
            if (hitForward.transform == null)
            {
                Instantiate(effect, transform.position + new Vector3(0, 0, i), transform.rotation);
            }
            if (hitRight.transform == null)
            {
                Instantiate(effect, transform.position + new Vector3(i, 0, 0), transform.rotation);
            }
            if (hitBack.transform == null)
            {
                Instantiate(effect, transform.position - new Vector3(0, 0, i), transform.rotation);
            }
            if (hitLeft.transform == null)
            {
                Instantiate(effect, transform.position - new Vector3(i, 0, 0), transform.rotation);
            }
        }
        Destroy(this.gameObject);
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Particle System");
        for(int i = 0; i < effects.Length;i++)
        {
            Destroy(effects[i], 3f);
        }
    }
}
