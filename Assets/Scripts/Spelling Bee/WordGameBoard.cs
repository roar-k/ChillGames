using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class WordGameBoard : MonoBehaviour
{
    public GameManager_Bee gameManager;
    
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
    public TextMeshProUGUI scrambledText;
    private string scrambledS;
    public ArrayList guess = new ArrayList();
    public string wordS;
    [SerializeField] private TextMeshProUGUI guessText;
    private float t = 11.0f;
    public TextMeshProUGUI timerText;
    public GameObject endScreen;

    [Header("Solution")]
    public string word;

    public int solved;

    private bool moving;

    private void Start() {
        LoadData();
        SetRandomWord();
        SetWord();
        solved = 0;
        endScreen.SetActive(false);
    }

    private void Update() {
        t -= Time.deltaTime;
        timerText.text = ((int)t).ToString();
        if (t< 1) {
            t = 0;
        }
        
        guessWord();
        
        //displays guess
        scrambledText.text = scrambledS.ToUpper();
        guessText.text = wordS;
        //checks if guess is appropriate length before checking for match
        if (guess.Count == word.Length && Input.GetKeyDown(KeyCode.Return)) {
            if (MatchesWord()){
                Debug.Log(solved);
                NextWord();
                
                
            }
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
        if (solved < 4) {
            word = threeLetterWords[Random.Range(0, threeLetterWords.Length)];
            word = word.ToLower().Trim();
        }
        // Gives a 4 letter word to solve once you have solved five 3 letter words
        else if (solved < 9) {
            word = fourLetterWords[Random.Range(0, fourLetterWords.Length)];
            word = word.ToLower().Trim();
        }
        // Gives a 5 letter word to solve once you have solved five 4 letter words
        else if (solved < 14) {
            word = fiveLetterWords[Random.Range(0, fiveLetterWords.Length)];
            word = word.ToLower().Trim();
        }

        else {
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

        for (int i = 0; i < word.Length; i++) {
            //squares[i].SetLetter(scrambledWord[i]);
            scrambledS += scrambledWord[i];
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

    
    public void guessWord(){
        if (t !=0) {
            //removing letters
            if (guess.Count >0 && Input.GetKeyDown(KeyCode.Backspace)) {
                    guess.RemoveAt(guess.Count -1);
                    
                    wordS = wordS.ToString().Substring(0, wordS.Length -1);
                    
                    
                    
                    
            //adding letters                
            } else if (guess.Count < word.Length) {
                for (int i = 0; i<SUPPORTED_KEYS.Length; i++) {
                    if (Input.GetKeyDown(SUPPORTED_KEYS[i])) {
                        guess.Add((SUPPORTED_KEYS[i].ToString()).ToLower());
                        wordS += (SUPPORTED_KEYS[i].ToString());
                        break;

                    } 
                }
            }
            
        }
    }
    public bool MatchesWord() {
        //complares letter by letter
        for(int i = 0; i<word.Length; i++) {
            if (!(guess[i].ToString().Equals(word[i].ToString()))) {
                guess.Clear();
                wordS = "";
                return false;
            }
        }
        
        return true;
    }

    // Goes to the next word
    public void NextWord() {
            wordS = "";
            scrambledS = "";
            guess.Clear();
            SetRandomWord();
            SetWord();
            solved++;
            t = 11.0f;
            gameManager.SubmitScore("shs_bee", solved);
    }

    public void TimeUp() {
        if (t == 0) {
            //Time.timeScale = 0;
            Debug.Log("aaaaaaaa");
            endScreen.SetActive(true);
        }
    }
    public void LoadMenu() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
    }

    public void PlayBee() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.SpellingBee);
    }
}
