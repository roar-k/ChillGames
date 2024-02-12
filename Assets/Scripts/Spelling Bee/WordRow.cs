using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordRow : MonoBehaviour
{
    // Array of the tiles
    public LetterSquare[] squares { get; private set; }

    public string word {
        get
        {
            string word = "";

            for (int i = 0; i < squares.Length; i++) {
                word += squares[i].letter;
            }

            return word;
        }
    }

    private void Awake() {
        squares = GetComponentsInChildren<LetterSquare>();
    }

}
