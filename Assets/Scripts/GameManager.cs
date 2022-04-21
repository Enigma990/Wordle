using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int currentColumn = 0;
    private int currentRow = 0;

    private int columnCount;
    private int rowCount;

    public string selectedWord = "HELLO";


    [SerializeField] TextAsset wordsListAsset = null;

    List<String> selectedWordsList;


    //List<string> guessedWord = new List<string>();

    string guessedWord = "";

    private void Awake()
    {
        instance = this;

        selectedWordsList = new List<string>(wordsListAsset.text.Split(new char[] { ',', ' ', '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));
    }

    // Start is called before the first frame update
    void Start()
    {
        currentColumn = 0;
        currentRow = 0;

        columnCount = TileManager.Instance.ColumnCount;
        rowCount = TileManager.Instance.RowCount;

        selectedWord = selectedWordsList[UnityEngine.Random.Range(0, selectedWordsList.Count)];
        selectedWord = selectedWord.ToUpper();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick_KeyPressed(string key)
    {
        if (currentColumn >= columnCount || currentColumn < 0) return;

        TileManager.Instance.AddDataToTile(currentRow, currentColumn, key);

        // If Not Removing Data, Increase current column number
        if (key != "")
        {
            //guessedWord.Add(key);

            guessedWord += key;
            currentColumn++;
        }

    }

    public void OnClick_EnterPressed()
    {

        Debug.LogError("WORD: " + guessedWord);
        if (currentRow >= rowCount) return;

        if (currentColumn < rowCount - 1)
        {
            // TODO - Display Invalid Warning
            return;
        }

        CheckWord();

        currentRow++;
        currentColumn = 0;

    }

    public void OnClick_BackPressed()
    {
        if (currentColumn <= 0) return; 

        currentColumn--;
        guessedWord = guessedWord.Remove(currentColumn);

        OnClick_KeyPressed("");
    }

    void CheckWord()
    {
        //string tempWord = "";
        //foreach (string s in guessedWord)
        //{
        //    tempWord += s;
        //}

        if (selectedWord.Equals(guessedWord))
        {
            Debug.LogError("EQUAL: " + guessedWord);
        }


        for (int i = 0; i < columnCount; i++)
        {
            if (selectedWord.Contains(guessedWord[i]))
            {
                Debug.Log("WORD FOUND: " + guessedWord[i]);

                if (selectedWord[i] == guessedWord[i])
                {
                    TileManager.Instance.ChangeTileColor(currentRow, i, Color.green);
                    KeyboardManager.Instance.UpdateKeyboardColor(guessedWord[i].ToString(), Color.green);
                }
                else
                {
                    TileManager.Instance.ChangeTileColor(currentRow, i, Color.yellow);
                    KeyboardManager.Instance.UpdateKeyboardColor(guessedWord[i].ToString(), Color.yellow);
                }
            }
            else
            {
                KeyboardManager.Instance.UpdateKeyboardColor(guessedWord[i].ToString(), Color.grey);
            }
        }

        //guessedWord.Clear();

        guessedWord = "";
    }
}
