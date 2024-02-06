using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterSquare : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    public char letter { get; private set; }

    private void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Sets the tile to that letter
    public void SetLetter(char letter) {
        this.letter = letter;
        text.text = letter.ToString();
    }
}
