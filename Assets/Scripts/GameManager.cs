using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int currentColumn = 0;
    private int currentRow = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentColumn = 0;
        currentRow = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick_KeyPressed(string key)
    {
        TileManager.Instance.AddDataToTile(currentRow, currentColumn, key);
        currentColumn++;
    }
}
