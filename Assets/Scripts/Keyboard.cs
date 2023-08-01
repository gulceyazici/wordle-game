using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Keyboard : MonoBehaviour
{
    public List<Button> keyboardCharacterButtons = new List<Button>();
   
    private void Awake()
    {
       
    }


    void Start()
    {
        //foreach (var keyboardButton in keyboardCharacterButtons)
        //{
        //    string letter = keyboardButton.transform.GetChild(0).GetComponent<Text>().text;
        //    keyboardButton.GetComponent<Button>().onClick.AddListener(() => ClickCharacter(letter));
        //}
    }
    void ClickCharacter(string letter)
    {
        // Output to the console for now
        // We will later use this function to add the letters to the wordboxes.
       // Debug.Log(letter);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
