using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGameBoard : MonoBehaviour
{
    public string[] validWords;

    private void Start() {
        LoadData();
    }

    // 
    private void LoadData() {
        TextAsset textFile = Resources.Load("valid_words") as TextAsset;
        validWords = textFile.text.Split('\n');
    }
}
