using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WordGameBoard : MonoBehaviour
{
    
    [Header("Text Files")]
    public string[] threeLetterWords;
    public string[] fourLetterWords;
    public string[] fiveLetterWords;
    public string[] sixLetterWords;
    public string[] validWords;
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, 
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, 
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, 
        KeyCode.Y, KeyCode.Z, 
    };

    [Header("Game Objects")]
    public LetterSquare[] squares;
    public Row[] row;
    public ArrayList guess = new ArrayList();
    public string wordS;
    [SerializeField] private TextMeshProUGUI guessText;

    [Header("Solution")]
    public string word;

    private int solved;

    private bool moving;

    private string temp;
    private void Start() {
        LoadData();
        SetRandomWord();
        SetWord();
        solved = 0;
    }

    private void Update() {
        foreach(string s in guess) {
            wordS += s;
        }
        guessWord();
        guessText.text = wordS;
        if (guess.Count == word.Length) {
            MatchesWord();
        }
        
        // NextWord();
        
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
        // Gives a 3 letter word to solve until you have solved 5
        if (solved < 5) {
            word = threeLetterWords[Random.Range(0, threeLetterWords.Length)];
            word = word.ToLower().Trim();
        }
        // Gives a 4 letter word to solve once you have solved five 3 letter words
        else if (solved < 10) {
            word = fourLetterWords[Random.Range(0, fourLetterWords.Length)];
            word = word.ToLower().Trim();
        }
        // Gives a 5 letter word to solve once you have solved five 4 letter words
        else if (solved < 15) {
            word = fiveLetterWords[Random.Range(0, fiveLetterWords.Length)];
            word = word.ToLower().Trim();
        }

        else if (solved < 20) {
            word = sixLetterWords[Random.Range(0, sixLetterWords.Length)];
            word = word.ToLower().Trim();
        }
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
    private static string ScrambleWord(string str) {
        char[] array = str.ToCharArray();
        int length = array.Length;
        while (length > 1) {
            length--;
            int rand = Random.Range(0, length + 1);
            var value = array[rand];
            array[rand] = array[length];
            array[length] = value;
        }

        return new string(array);
    }

    // adds letter to word guessed
    public void guessWord(){
        
        if (guess.Count >0 && Input.GetKeyDown(KeyCode.Backspace)) {
                guess.RemoveAt(guess.Count -1);
                
                wordS = wordS.ToString().Substring(0, wordS.Length -1);
                
                
                
                
                
        } else {
            for (int i = 0; i<SUPPORTED_KEYS.Length; i++) {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i])) {
                    guess.Add((SUPPORTED_KEYS[i].ToString()).ToLower());
                    wordS += (SUPPORTED_KEYS[i].ToString());
                    break;

                } 
            }
        }
        
    }
    public bool MatchesWord() {
        for(int i = 0; i<word.Length; i++) {
            if (!(guess[i].Equals(word[i]))) {
                
                return false;
            }
        }
        return true;
    }

    // Goes to the next word
    /* private void NextWord() {
        if (MatchesWord()) {
            SetRandomWord();
            SetWord();
            solved++;
        }
    }

    /* private void AddSquare() {
        squares(Instantiate(square));
    } */
}
