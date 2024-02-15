using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterSquare : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    public char letter { get; set; }

    public bool moving;

    private void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Sets the tile to that letter
    public void SetLetter(char letter) {
        this.letter = letter;
        text.text = letter.ToString();
    }

    /* public void AutoMove() {
        if (moving) {

        }
    } */
}
