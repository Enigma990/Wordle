using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text wordText;
    [SerializeField] private TMP_Text totalGuessText;

    private int completeScore = 100;
    private int currentScore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        gameoverPanel.transform.localScale = new Vector3(0, 0, 0);
        gameoverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GameOver(int _currentRow, int _rowCount)
    {
        yield return new WaitForSeconds(1.5f);

        gameoverPanel.SetActive(true);
        gameoverPanel.transform.DOScale(1, 1);

        if (_currentRow >= _rowCount)
        {
            scoreText.text = $"";
            wordText.text = $"Word: {GameManager.Instance.SelecctedWord}";
            totalGuessText.text = $"Guesses: {_currentRow + 1}";
        }
        else
        {
            CalculateScore(_currentRow, _rowCount);
            scoreText.text = $"Score: {currentScore}";
            wordText.text = $"Word: {GameManager.Instance.SelecctedWord}";
            totalGuessText.text = $"Correct Guesses: {_currentRow + 1}";
        }
    }

    public void CalculateScore(int _currentRow, int _rowCount)
    {
        currentScore += completeScore * (_rowCount - _currentRow);
    }

    public void OnClick_MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
