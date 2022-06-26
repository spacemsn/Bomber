using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject prefabBomb;
    [SerializeField] private GameObject text;
    private Text healthText;

    private GameObject _bomb;
    private Animator anim;

    [Header("Эффект взыра врагов")]
    [SerializeField] private GameObject bombDetonation;

    [Header("Характеристики персонажа")]
    public float hp = 3;
    public float speed;
    private bool live = true;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        healthText = text.GetComponent<Text>();
        healthText.text = hp.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection = transform.forward * speed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = transform.forward * speed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = transform.forward * speed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection = transform.forward * speed;
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            moveDirection = Vector3.zero;
        }

        anim.SetFloat("Speed", Vector3.ClampMagnitude(moveDirection, 1).magnitude);
        characterController.SimpleMove(moveDirection);


        if (Input.GetKeyUp(KeyCode.Space))
        {
            _bomb = Instantiate(prefabBomb) as GameObject;
            _bomb.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
            _bomb.transform.rotation = this.transform.rotation;
        }
        if (hp <= 0)
        {
            live = false;
            if (!live)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(bombDetonation, other.gameObject.transform.position, other.gameObject.transform.rotation);
            CharacterHP(1);
            Destroy(other.gameObject);
            GameObject[] effects = GameObject.FindGameObjectsWithTag("Particle System");
            for (int i = 0; i < effects.Length; i++)
            {
                Destroy(effects[i], 3f);
            }
        }

        if(other.gameObject.tag == "LevelUP")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        healthText.text = hp.ToString();
    }

    public void CharacterHP(float damage)
    {
        if(hp > 0)
        {
            hp -= damage;
        }
    }
}
