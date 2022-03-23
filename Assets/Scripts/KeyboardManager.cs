using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    private static KeyboardManager instance;
    public static KeyboardManager Instance { get { return instance; } }

    [SerializeField] private Button[] keyboardKeys;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeyboardColor(string _key, Color _color)
    {
        for (int i = 0; i < keyboardKeys.Length; i++)
        {
            if (keyboardKeys[i].GetComponentInChildren<TMP_Text>().text == _key)
            {
                keyboardKeys[i].GetComponent<Image>().color = _color;
            }
        }
    }
}
