using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ChaptersUIManager : MonoBehaviour
{
    public static ChaptersUIManager instance;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color currentSceneColor;
    [SerializeField] private List<Image> timeLineScenes;

    [Header("Scenes GameObjects")]
    [SerializeField] private CanvasGroup scene1Text = null;
    [SerializeField] private CanvasGroup restartBtn = null;
    [SerializeField] private CanvasGroup scene3Image = null;



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

        //Initial values
        
        scene1Text.alpha = 0;
        scene3Image.alpha = 0;
        restartBtn.interactable = false;
        restartBtn.blocksRaycasts = false;
    }

    void Start()
    {
        SceneLoader.instance.EventLoadNewScene += OnLoadNewScene;
        timeLineScenes[0].CrossFadeColor(currentSceneColor, 0.5f, false, false);
        StartCoroutine(CanvasFaderIn(scene1Text, 1f, 1f));
    }

    private void OnDestroy()
    {
        SceneLoader.instance.EventLoadNewScene -= OnLoadNewScene;
    }

    private void OnLoadNewScene(int newScene)
    {
        if (restartBtn == null || scene1Text == null || scene3Image == null)
            return;

        switch (newScene)
        {
            case 0:
                StartCoroutine(CanvasFaderOut(restartBtn, 0f, 1f));
                restartBtn.interactable = false;
                restartBtn.blocksRaycasts = false;
                StartCoroutine(CanvasFaderIn(scene1Text, 1f, 1f));
                StartCoroutine(CanvasFaderOut(scene3Image, 0f, 1f));
                break;
            case 1:
                StartCoroutine(CanvasFaderOut(restartBtn, 0f, 1f));
                restartBtn.interactable = false;
                restartBtn.blocksRaycasts = false;
                StartCoroutine(CanvasFaderOut(scene1Text, 0f, 1f));
                StartCoroutine(CanvasFaderOut(scene3Image, 0f, 1f));
                break;
            case 2:
                StartCoroutine(CanvasFaderIn(restartBtn, 1f, 1f));
                restartBtn.interactable = true;
                restartBtn.blocksRaycasts = true;
                StartCoroutine(CanvasFaderOut(scene1Text, 0f, 1f));
                StartCoroutine(CanvasFaderIn(scene3Image, 1f, 1f));
                break;
            default:
                break;
        }

        foreach (var scene in timeLineScenes)
        {
            scene.CrossFadeColor(normalColor, 0.5f, false, false);
        }

        timeLineScenes[newScene].CrossFadeColor(currentSceneColor, 0.5f, false, false);
    }

    private IEnumerator CanvasFaderIn(CanvasGroup canvasGroup, float finalValue, float time)
    {
        if (canvasGroup.alpha == 1)
            yield break;

        float t = 0;

        while (t < finalValue)
        {
            canvasGroup.alpha = t / time;
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = finalValue;
    }

    private IEnumerator CanvasFaderOut(CanvasGroup canvasGroup, float finalValue, float time)
    {
        if (canvasGroup.alpha == 0)
            yield break;

        float t = 1;

        while (t > finalValue)
        {
            canvasGroup.alpha = t / time;
            t -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = finalValue;
    }



}


