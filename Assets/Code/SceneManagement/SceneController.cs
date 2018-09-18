// ----------------------------------------------------------------------------
// Author:  William O'Toole
// Project: BitRivet Framework
// Date:    20 JUNE 2018
// ----------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Controls basic Scene Management functions
/// </summary>
public class SceneController : MonoBehaviour 
{
    public SceneItem[] Scenes;
    public CanvasGroup screenFadeCanvas;
    public float fadeDuration = 1f;

    [SerializeField]
    private string currentScene;
    public string CurrentScene
    {
        get { return currentScene; }
        private set { currentScene = value; }
    }

    private bool isFading;

    public Events.EventFadeComplete OnSceneChangeStart;
    public Events.EventFadeComplete OnSceneChangeComplete;

    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));
        yield return SceneManager.LoadSceneAsync(sceneName);
        OnSceneChangeStart.Invoke(true);
        yield return StartCoroutine(Fade(0f));
        OnSceneChangeComplete.Invoke(true);
        currentScene = sceneName;
    }

    /// <summary>
    /// Adjusts the CanvasGroup Component Alpha creating a "Screen Fade" effect.
    /// </summary>
    /// <param name="finalAlpha"></param>
    /// <returns></returns>
    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        screenFadeCanvas.blocksRaycasts = true; // Blocks player Clicking on other Scene or UI GameObjects
        float fadeSpeed = Mathf.Abs(screenFadeCanvas.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(screenFadeCanvas.alpha, finalAlpha))
        {
            screenFadeCanvas.alpha = Mathf.MoveTowards(screenFadeCanvas.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null; //Lets the Coroutine finish
        }
        isFading = false;
        screenFadeCanvas.blocksRaycasts = false;
    }
}