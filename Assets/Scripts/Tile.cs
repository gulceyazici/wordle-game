using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    //Serialization is the automatic process of transforming data structures or object states into a format that Unity can store and reconstruct later. 
    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }
    public State state { get; private set; }
    // we also want to store this letter in our tile that way we can access it through our game logic later
    public char letter { get; private set; }

    private TextMeshProUGUI text;

    private Image fill;

    private Outline outline;

    Animator tileAnimator;
    
   

    //this function is called automatically by unity when the script is first initial
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        tileAnimator = GetComponent<Animator>();


    }
    // what letter should show up in that tile
    public void setLetter(char letter)
    {
        this.letter = letter;

        text.text = letter.ToString();   
       
    }
    public void SetState(State state)
    {
        // when we set the state we need to update those colors on our components(image and outline), so we need references of image and outline components
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor= state.outlineColor;
    }

    public void SetTile(bool result)
    {
        if (result)
        {
            tileAnimator.SetBool("SetTile", true);
            
        }
        else
        {
            tileAnimator.SetBool("SetTile", false);
        }
        
    }
    

}
