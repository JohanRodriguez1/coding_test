using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;

public class OnEnableFadeIn : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;

    private Coroutine corutine = null;


    private void Awake()
    {
        //Initialize value
        canvasGroup.alpha = 0;
    }
    private void Start()
    {
        
        SceneLoader.instance.EventLoadNewScene += OnLoadNewScene;
        if (corutine != null)
            StopAllCoroutines();
        corutine = StartCoroutine(CanvasFaderIn(canvasGroup, 1f, 1f));
    }

    private void OnLoadNewScene(int scene)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            corutine = StartCoroutine(CanvasFaderOut(canvasGroup, 0f, 1f));
        }
    }

    private IEnumerator CanvasFaderIn(CanvasGroup canvasGroup,  float finalValue, float time)
    {
        float t = 0; 

        while (t < finalValue)
        {
            canvasGroup.alpha =  t / time;
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = finalValue;
    }

    private IEnumerator CanvasFaderOut(CanvasGroup canvasGroup, float finalValue, float time)
    {
        float t = 1;

        while (t > finalValue)
        {
            canvasGroup.alpha = t / time;
            t -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = finalValue;
    }

    private void OnDestroy()
    {
        SceneLoader.instance.EventLoadNewScene -= OnLoadNewScene;
    }

}
