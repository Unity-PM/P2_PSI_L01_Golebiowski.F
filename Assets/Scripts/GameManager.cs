using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private int noEnemy;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private GameObject endGameSound;
    [SerializeField]
    private GameObject PostProcessFX;
    [SerializeField]
    private AudioSource GameMusic;
    public static Action<int> UpdateRemainingEnemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        noEnemy = FindObjectsOfType<EnemyManager>().Length;
        EnemyManager.Death += Death;
        UpdateRemainingEnemies?.Invoke(noEnemy);

        
    }

    // Update is called once per frame
    void Death()
    {
        noEnemy--;
        UpdateRemainingEnemies?.Invoke(noEnemy);
        if (noEnemy == 0)
        {
            StartCoroutine(EndGame());
        }
    }

    private void Update()
    {
        endGameSound.transform.position = player.transform.position;
    }

    IEnumerator EndGame()
    {

        Volume vol = PostProcessFX.GetComponent<Volume>();
        LiftGammaGain lgg;
        if (vol.profile.TryGet<LiftGammaGain>(out lgg))
        {
            endGameSound.GetComponent<AudioSource>().Play();
            for (int i = 0; i <= 100; i++)
            {
                float progress = i / 100f;
                endGameSound.GetComponent<AudioSource>().volume = i / 100f;
                GameMusic.GetComponent<AudioSource>().volume = 1f - progress;
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i <= 100; i++)
            {
                lgg.lift.value = new Vector4(-(i / 100f), -(i / 100f), -(i / 100f), -(i / 100f));
                yield return new WaitForSeconds(0.05f);
            }
            HUD.SetActive(false);
            yield return new WaitForSeconds(5);
        }
        SceneManager.LoadScene(2);

        for (int i = 0; i <= 100; i++)
        {
            float progress = i / 100f;
            endGameSound.GetComponent<AudioSource>().volume = 1f - progress;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(50);
        Application.Quit();
    }
}
