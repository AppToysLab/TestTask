using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Net : MonoBehaviour
{
    public void CellsMaker(float x, float y, float scaleSymbol)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        var canvasTransform = canvas.GetComponent<Transform>();
        GameObject Cell = new GameObject("Cell");
        Font fntCell = Resources.Load<Font>("Font/fntCell");
        Cell.AddComponent<Text>();
        var cellTxt = Cell.GetComponent<Text>();
        cellTxt.font = fntCell;
        cellTxt.fontSize = 50;
        cellTxt.color = Color.white;
        cellTxt.text = "C";
        Cell.AddComponent<Cell>();
        var cellTransform = Cell.GetComponent<RectTransform>();
        cellTransform.SetParent(canvasTransform);
        cellTransform.anchorMax = new Vector2(0, 1);
        cellTransform.anchorMin = new Vector2(0, 1);
        Cell.transform.localPosition = new Vector2(x, y);
    }

    public void CellsGenerate()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if ((gameManager.numX != null) && (gameManager.numX != null))
        {
            this.CellSetuper(gameManager.fieldSize.x, gameManager.fieldSize.y, gameManager.numX, gameManager.numY);
        }
        
    }

    public void CellSetuper( float fieldSizeX, float fieldSizeY, int numX, int numY)
    {
        float pointZeroX = - Screen.width / 2 + 150;
        float pointZeroY = 200;
        float stepX = 50;
        float stepY = 70;
        int numCells = numX * numY;
        for (int i = 0; i < numY; i++)
        {
            for (int j = 0; j < numX; j++)
            {
                this.CellsMaker(pointZeroX + stepX * j, pointZeroY + stepY * i , 1);
            }
                
        }
    }
    //void Start()
    //{
         
    //}
}
