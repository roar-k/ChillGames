using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;

// Script for handling the "MainMenu" scene
public class MainMenu : MonoBehaviour
{
    public void PlayWordle() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Wordle);
    }
    
    public void PlayDino() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.DinoGame);
    }

    public void PlayBee() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.SpellingBee);
    }

    public void PlayBird() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.FlappyBird);
    }
}
