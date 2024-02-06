using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGameBoard : MonoBehaviour
{
    public string[] threeLetterWords;
    public string[] fourLetterWords;
    public string[] fiveLetterWords;
    public string[] sixLetterWords;
    public string[] validWords;

    private WordRow[] row;

    private char[] letters;

    private int index;

    public string word;

    private void Start() {
        LoadData();
        SetRandomWord();
    }

    // Loads in all the valid words into the game from a text file
    private void LoadData() {
        TextAsset textFile = Resources.Load("3_letter_words") as TextAsset;
        threeLetterWords = textFile.text.Split('\n');

        textFile = Resources.Load("4_letter_words") as TextAsset;
        fourLetterWords = textFile.text.Split('\n');

        textFile = Resources.Load("5_letter_words") as TextAsset;
        fiveLetterWords = textFile.text.Split('\n');

        textFile = Resources.Load("6_letter_words") as TextAsset;
        sixLetterWords = textFile.text.Split('\n');

        textFile = Resources.Load("valid_words") as TextAsset;
        validWords = textFile.text.Split('\n');
    }

    // Sets the solution to a random word in the valid words list
    private void SetRandomWord() {
        word = threeLetterWords[Random.Range(0, threeLetterWords.Length)];
        word = word.ToLower().Trim();
    }

    // Sets the squares to the word
    private void SetWord() {
        /* for (int i = 0; i < word.Length; i++) {
            letters[i] = word[i];
        }

        for (int i = 0; i < row.Length; i++) {
            row[i].squares.SetLetter(letters[i]);
        }
        */
    }

    // Checks to see if the word matches the solution
    private bool IsValidWord(string word) {
        for (int i = 0; i < validWords.Length; i++) {
            if (validWords[i].ToLower().Trim() == word) {
                return true;
            }
        }
        return false;
    }
}
