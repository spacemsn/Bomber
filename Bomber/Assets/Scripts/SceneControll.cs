using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControll : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private GameObject Audio;
    private AudioSource audio;

    [Header("Prefab Enemy")]
    [SerializeField] private GameObject Enemy;

    private float random;

    [Header("SkyBox's")]
    [SerializeField] Material skyBox1;
    [SerializeField] Material skyBox2;
    [SerializeField] Material skyBox3;

    [Header("Create Enemy position")]
    [SerializeField] Vector3 pos1 = new Vector3(10.35f, 11.55f, 40.5f);
    [SerializeField] Vector3 pos2 = new Vector3(23.35f, 11.55f, 40.5f);

    void Start()
    {
        audio = Audio.GetComponent<AudioSource>();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            RenderSettings.skybox = skyBox1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            RenderSettings.skybox = skyBox2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            RenderSettings.skybox = skyBox3;
        }

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            audio.volume += 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            audio.volume -= 0.1f;
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            if (audio.mute == true)
            {
                audio.mute = false;
            } 
            else if(audio.mute == false) 
            { 
                audio.mute = true; 
            }
        }
    }

    public IEnumerator SpawnEnemy()
    {
        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject _enemy = Instantiate(Enemy) as GameObject;
            random = Random.Range(1, 3);
            if (random == 1)
            {
                _enemy.transform.position = pos1;
            }
            else _enemy.transform.position = pos2;
            _enemy.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(5f);
        }
    }
}
