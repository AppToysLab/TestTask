using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Net : MonoBehaviour
{
    public delegate void Restart();
    public event Restart RestartEvent;
    public float cellScale;

    public void CellsMaker(float x, float y, float scaleSymbol)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        var canvasTransform = canvas.GetComponent<Transform>();
        GameObject Cell = new GameObject("Cell");
        Cell.AddComponent<Cell>();
        Font fntCell = Resources.Load<Font>("Font/fntCell");
        Cell.AddComponent<Text>();
        var cellTxt = Cell.GetComponent<Text>();
        cellTxt.font = fntCell;
        cellTxt.fontSize = 50;
        cellTxt.color = Color.white;
        cellTxt.alignment = TextAnchor.MiddleCenter;
       
        
        var cellTransform = Cell.GetComponent<RectTransform>();
        cellTransform.SetParent(canvasTransform);
        cellTransform.anchorMax = new Vector2(0, 1);
        cellTransform.anchorMin = new Vector2(0, 1);
        cellTransform.localScale = new Vector2( scaleSymbol, scaleSymbol);
        Cell.transform.localPosition = new Vector2(x, y);

        ContentManager contentManager = FindObjectOfType<ContentManager>();
        cellTxt.text = contentManager.content[Random.Range(0, contentManager.content.Length)];
    }

    public void CellsGenerate()
    {
        if(GameObject.Find("Cell") != null)
        {
            RestartEvent();// Событие на удаление ранее созданных экземпляров Cell 
        }
        
        GameManager gameManager = FindObjectOfType<GameManager>();
        if ((gameManager.numX != null) && (gameManager.numX != null))
        {
            this.CellSetuper(gameManager.fieldSize.x, gameManager.fieldSize.y, gameManager.numX, gameManager.numY);
        }
    }

    public void CellSetuper( float fieldSizeX, float fieldSizeY, int numX, int numY)
    {
        float pointZeroX = -(fieldSizeX / numX + 1) * (numX - 1) / 2; // координата Х левой верхней ячейки ( левая граница поля)
        float pointZeroY = 180 + fieldSizeY / 2;// координата Y левой верхней ячейки 
        float stepX = fieldSizeX / numX + 1;// назначаем шаг по горизонтали
        Debug.Log("numX = " + numX);
        float stepY = fieldSizeY / numY + 1;// назначаем шаг по вертикали
        this.cellScale = 1; // / numX;
        for (int i = 0; i < numY; i++)
        {
            for (int j = 0; j < numX; j++)
            {
                this.CellsMaker(pointZeroX + stepX * j, pointZeroY - stepY * i , cellScale);
            }
                
        }
    }
}
