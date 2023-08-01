
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z
    };
    // key inputs
    private Row[] rows;
    private Button[] buttons;
    private string[] solutions;
    private string[] validWords;
    private string word;
    private string text;
    private Button button;
    

    private int rowIndex;
    private int columnIndex =0;
    

    [Header("States")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    [Header("UI")]

    public TextMeshProUGUI invalidWordText;

    public void Awake()
    {
        rows = GetComponentsInChildren<Row>();
        buttons = GetComponentsInChildren<Button>();
    }
    private void Start()
    {
        LoadData();
        SetRandomWord();
    }
    private void LoadData()
    {
        // when this loads it loads as generic unity object, to prevent this we said "as textasset"
        TextAsset textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split('\n');

        textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split('\n');
    }

    private void SetRandomWord()
    {
        word = solutions[UnityEngine.Random.Range(0, solutions.Length)];
        word = word.ToLower().Trim();
        
    }
    public void KeyboardActions()
    {
        Row currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // after clicking backspace, column index will be the max number between 0 and columnindex -1
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            // deleting a character in tiles
            currentRow.tiles[columnIndex].setLetter('\0'); // chosen tile will be null/empty
            currentRow.tiles[columnIndex].SetState(emptyState);
            invalidWordText.gameObject.SetActive(false);
            currentRow.InvalidWord(false);


        }
        else if (Button.CheckIfButtonClicked())
        {

            text = Button.ReturnLetter();
            Debug.Log(text.Equals("ENTER"));
            Debug.Log(columnIndex);

            if (text.Equals("BACK"))
            {
                Debug.Log("back içi");
                columnIndex = Mathf.Max(columnIndex - 1, 0);
                // deleting a character in tiles
                currentRow.tiles[columnIndex].setLetter('\0'); // chosen tile will be null/empty
                currentRow.tiles[columnIndex].SetState(emptyState);
                invalidWordText.gameObject.SetActive(false);
                currentRow.InvalidWord(false);
            }

            else if (text.Equals("ENTER") && columnIndex >= currentRow.tiles.Length)
            {
                Debug.Log("enter içi");
                SubmitRow(currentRow);
            }
            else
            {


                currentRow.tiles[columnIndex].setLetter(char.Parse(text));
                currentRow.tiles[columnIndex].SetState(occupiedState);
                columnIndex++;


            }
            Button.setFalse();

        }
        else if (columnIndex >= currentRow.tiles.Length)
        {
            // submitting a row
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(currentRow);
            }
        }
        
        else
        {
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
            {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                {

                    // we need to set that letter to the tile we are on but we need to keep track which tile we are at now

                    currentRow.tiles[columnIndex].setLetter((char)SUPPORTED_KEYS[i]);
                    currentRow.tiles[columnIndex].SetState(occupiedState);

                    columnIndex++;

                    break;

                }
                
            }
            
        }
        
    }
    private void Update()
    {
        KeyboardActions();
    }

    private void SubmitRow(Row row)
    {
        Debug.Log("submit row içi");
        if(!isValidWord(row.word))
        {
            Debug.Log("valid deðil");
            invalidWordText.gameObject.SetActive(true);
            row.InvalidWord(true);
            return;
        }
        string remaining = word;

        for(int i =0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];
            Debug.Log("set title öncesi");
            tile.SetTile(true);
            Debug.Log("set title sonrasý");


            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
                
                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if(!word.Contains(tile.letter))
            {
                tile.SetState(incorrectState);
            }

        }
        for(int i=0; i< row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];
            tile.SetTile(true);           

            if (tile.state != correctState && tile.state != incorrectState)
            {
                if(remaining.Contains(tile.letter)) 
                {
                    tile.SetState(wrongSpotState);

                    int index = remaining.IndexOf(tile.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else
                {
                    tile.SetState(incorrectState);
                }

            
            }
        }
        rowIndex++;
        columnIndex= 0;

        if(rowIndex >= rows.Length)
        {
            // when script is disabled update will not be called
            enabled= false;
        }
    }

    private bool isValidWord(string word)
    {
        for(int i =0; i< validWords.Length; i++)
        {
            if (validWords[i] == word) 
            { 
                return true; 
            }
        }
        return false;
    }
}
