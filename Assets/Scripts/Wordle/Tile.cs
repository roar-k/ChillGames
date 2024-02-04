using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script for each tile in Wordle, add to Tile object in Wordle
public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class State 
    {
        public Color fillColor;
        public Color outlineColor;
    }
    private TextMeshProUGUI text;
    private Image fill;
    private Outline outline;
    
    public State state { get; private set; }
    public char letter { get; private set; }

    private void Awake() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    // Sets the tile to that letter
    public void SetLetter(char letter) {
        this.letter = letter;
        text.text = letter.ToString();
    }

    // Sets the state of the tile
    // Green if correct, Yellow if correct but in wrong spot, Nothing if incorrect
    public void SetState(State state) {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }
}
