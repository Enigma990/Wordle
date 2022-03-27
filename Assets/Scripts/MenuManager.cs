using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform MainMenu;
    [SerializeField] private RectTransform LevelSelector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_PlayBtn()
    {
        MainMenu.gameObject.SetActive(false);
        MainMenu.DOScale(0, 1);

        LevelSelector.gameObject.SetActive(true);
        LevelSelector.DOScale(1, 1);
    }

    public void OnClick_BackBtn()
    {
        LevelSelector.gameObject.SetActive(false);
        LevelSelector.DOScale(0, 1);

        MainMenu.gameObject.SetActive(true);
        MainMenu.DOScale(1, 1);
    }

    public void OnClick_LevelSelect(int _wordsCount)
    {
        PlayerPrefs.SetInt("WordsCount", _wordsCount);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
