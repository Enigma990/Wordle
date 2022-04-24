using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int currentColumn = 0;
    [SerializeField]
    private int currentRow = 0;

    private int columnCount;
    [SerializeField]
    private int rowCount;
    [SerializeField]
    private string selectedWord = "HELLO";
    public string SelecctedWord { get { return selectedWord; } }

    [SerializeField] TextAsset wordsListAsset = null;

    List<String> selectedWordsList;

    [Header("Time Trail Data")]
    [SerializeField] private int timeLeft = 60;
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private TMP_Text correctGuessCountText;
    private int correctGuessCount;

    [Space]
    [SerializeField] private TMP_Text invalidWordText;

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

        invalidWordText.gameObject.SetActive(false);

        if (Constant.currentGamemode == Constant.GameMode.TimeTrail)
        {
            correctGuessCountText.gameObject.SetActive(true);
            timeLeftText.gameObject.SetActive(true);

            correctGuessCount = 0;
            correctGuessCountText.text = $"Correct Guesses: { correctGuessCount }";
            timeLeftText.text = $"Time: { timeLeft }";
            InvokeRepeating(nameof(CheckTimer), 0.0f, 1.0f);
        }
        else
        {
            correctGuessCountText.gameObject.SetActive(false);
            timeLeftText.gameObject.SetActive(false);
        }
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


        //Handheld.Vibrate();
    }

    public void OnClick_EnterPressed()
    {
        if (currentRow >= rowCount) return;

        if (currentColumn < rowCount - 1)
        {
            // TODO - Display Invalid Warning
            return;
        }


        if (!selectedWordsList.Contains(guessedWord.ToLower()))
        {
            Handheld.Vibrate();

            for (int i = 0; i < columnCount; i++)
            {
                TileManager.Instance.ShakeTile(currentRow, i);
            }

            invalidWordText.DOFade(100, 0.1f);
            invalidWordText.gameObject.SetActive(true);
            invalidWordText.DOFade(0, 3.0f);
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
        bool bIsCorrectWord = false;

        if (selectedWord.Equals(guessedWord))
        {
            Debug.LogError("EQUAL: " + guessedWord);

            //ScoreManager.Instance.GameOver(currentRow, rowCount - 1);

            bIsCorrectWord = true;
        }

        // Classic Game mode
        if (Constant.currentGamemode == Constant.GameMode.Classic)
        {
            for (int i = 0; i < columnCount; i++)
            {
                if (selectedWord.Contains(guessedWord[i]))
                {
                    //Debug.Log("WORD FOUND: " + guessedWord[i]);

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
                    TileManager.Instance.ChangeTileColor(currentRow, i, Color.gray);
                    KeyboardManager.Instance.UpdateKeyboardColor(guessedWord[i].ToString(), Color.gray);
                }
            }
        }

        // Blind Game Mode
        else if (Constant.currentGamemode == Constant.GameMode.Blind)
        {
            for (int i = 0; i < columnCount; i++)
            {
                if (selectedWord.Contains(guessedWord[i]))
                {
                    //Debug.Log("WORD FOUND: " + guessedWord[i]);

                    if (selectedWord[i] == guessedWord[i])
                    {
                        TileManager.Instance.ChangeTileColor(currentRow, i, Color.green);
                        TileManager.Instance.AddDataToTile(currentRow, i, "");
                    }
                    else
                    {
                        TileManager.Instance.ChangeTileColor(currentRow, i, Color.yellow);
                        TileManager.Instance.AddDataToTile(currentRow, i, "");
                    }
                }
                else
                {
                    TileManager.Instance.ChangeTileColor(currentRow, i, Color.black);
                    TileManager.Instance.AddDataToTile(currentRow, i, "");
                }
            }
        }

        // Time Trail Game mode
        else if (Constant.currentGamemode == Constant.GameMode.TimeTrail)
        {
            for (int i = 0; i < columnCount; i++)
            {
                if (selectedWord.Contains(guessedWord[i]))
                {
                    //Debug.Log("WORD FOUND: " + guessedWord[i]);

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
                    TileManager.Instance.ChangeTileColor(currentRow, i, Color.gray);
                    KeyboardManager.Instance.UpdateKeyboardColor(guessedWord[i].ToString(), Color.gray);
                }
            }
        }
        guessedWord = "";

        if (currentRow >= rowCount - 1 || bIsCorrectWord && Constant.currentGamemode != Constant.GameMode.TimeTrail)
        {
            StartCoroutine(ScoreManager.Instance.GameOver(currentRow, rowCount - 1));
        }

        if (Constant.currentGamemode == Constant.GameMode.TimeTrail && bIsCorrectWord)
        {
            ScoreManager.Instance.CalculateScore(currentRow, rowCount);

            correctGuessCount += 1;
            correctGuessCountText.text = $"Correct Guesses: { correctGuessCount }";

            Invoke(nameof(ResetData), 1.5f);            
        }
    }

    void ResetData()
    {
        currentRow = 0;
        currentColumn = 0;

        selectedWord = selectedWordsList[UnityEngine.Random.Range(0, selectedWordsList.Count)];
        selectedWord = selectedWord.ToUpper();
        KeyboardManager.Instance.ResetKeys();
        TileManager.Instance.ResetTileColor();

    }

    void CheckTimer()
    {
        timeLeft -= 1;
        timeLeftText.text = $"Time: { timeLeft }";

        if (timeLeft <= 0)
        {
            correctGuessCountText.gameObject.SetActive(false);
            timeLeftText.gameObject.SetActive(false);
            StartCoroutine(ScoreManager.Instance.GameOver(currentRow, rowCount - 1));

            CancelInvoke(nameof(CheckTimer));
        }
    }
}
