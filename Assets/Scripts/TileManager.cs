using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance { get { return instance; } }

    [Header("Grid")]
    [SerializeField] private int rowCount = 6;
    [SerializeField] private int columnCount = 3;
    [SerializeField] private GridLayoutGroup tilesGrid;

    [Header("Tile")]
    [SerializeField] private GameObject tilePrefab = null;

    private GameObject[,] tilesList;


    public int RowCount { get { return rowCount; } }
    public int ColumnCount { get { return columnCount; } }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tilesList = new GameObject[rowCount, columnCount];

        tilesGrid.constraintCount = columnCount;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                GameObject tile = Instantiate(tilePrefab, this.transform);

                tilesList[i, j] = tile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDataToTile(int _row, int _column, string _Data)
    {
        tilesList[_row, _column].GetComponentInChildren<TMP_Text>().text = _Data;
    }

    public void ChangeTileColor(int _row, int _column, Color _color)
    {
        tilesList[_row, _column].GetComponent<Image>().color = _color;
    }
}
