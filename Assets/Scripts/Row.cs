
using System;
using UnityEngine;

public class Row : MonoBehaviour 
{
   //keep an array of tiles
   public Tile[] tiles { get; private set; }
   
   Animator rowAnimator;

    public string word
    {
        get
        {
            string word = "";

            for(int i=0; i<tiles.Length; i++)
            {
                word += tiles[i].letter;
            }
            return word;
        }
    }

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
        rowAnimator = GetComponent<Animator>();

    }
    public void InvalidWord(bool result)
    {
       
        if(result)
        {
                
            rowAnimator.SetBool("WordIsNotOnTheList", true);
        }
        else
        {
            rowAnimator.SetBool("WordIsNotOnTheList", false);
        }
    }

}
// we need to access these tiles from our board script 
