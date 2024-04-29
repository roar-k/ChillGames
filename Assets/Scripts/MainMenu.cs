using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class MainMenu : MonoBehaviour
{
    public void PlayWordle() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Wordle);
    }
    
    public void PlayDino() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.DinoGame);
    }
}
