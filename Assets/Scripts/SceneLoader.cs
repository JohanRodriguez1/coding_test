using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Action<int> EventLoadNewScene = delegate { };

    [SerializeField] private float delay = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadNewScene(int scene)
    {
        EventLoadNewScene(scene);
        StartCoroutine(LoadNewSceneWait(scene));
    }

    private IEnumerator LoadNewSceneWait(int scene)
    {
        yield return new WaitForSeconds(delay);
        
        if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings)
            yield return null;
        else
            SceneManager.LoadScene(scene);
    }
}
