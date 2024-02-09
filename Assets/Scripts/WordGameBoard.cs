using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordGameBoard : MonoBehaviour
{
    [Header("Text Files")]
    public string[] threeLetterWords;
    public string[] fourLetterWords;
    public string[] fiveLetterWords;
    public string[] sixLetterWords;
    public string[] validWords;

    [Header("Game Objects")]
    public LetterSquare[] squares;

    [Header("Solution")]
    public string word;

    private void Start() {
        LoadData();
        SetRandomWord();
        SetWord();
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

    // Sets the squares to the word and scrambles the letters
    private void SetWord() {
        string scrambledWord = ScrambleWord(word);
        // Makes sure the scrambled word does not match the solution
        while (scrambledWord == word) {
            ScrambleWord(word);
        }

        for (int i = 0; i < squares.Length; i++) {
            squares[i].SetLetter(scrambledWord[i]);
        }
    }

    // Shuffles the letters in the word
    public static string ScrambleWord(string str) {
        char[] array = str.ToCharArray();
        int n = array.Length;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
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
