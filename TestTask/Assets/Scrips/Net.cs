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
    public Vector2 [] secondPointCell;

    public void CellsMaker(float x, float y, float scaleSymbol, int counterCell)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        var canvasTransform = canvas.GetComponent<Transform>();
        GameObject Cell = new GameObject("Cell");
        Cell.AddComponent<Cell>();
        
        Font fntCell = Resources.Load<Font>("Font/fntCell");
        Cell.AddComponent<Text>();
        Cell.GetComponent<Cell>().numberOfCell = counterCell;
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
        this.CellSetuper(gameManager.fieldSize.x, gameManager.fieldSize.y, gameManager.numX, gameManager.numY);
    }


    public void CellSetuper( float fieldSizeX, float fieldSizeY, int numX, int numY)
    {
        var numSymbols = numX * numY;
        this.secondPointCell = new Vector2 [numSymbols];//готовим массив под новые координаты ячейки

        float pointZeroX = -(fieldSizeX / numX + 1) * (numX - 1) / 2; // координата Х левой верхней ячейки ( левая граница поля)
        float pointZeroY = 180 + fieldSizeY / 2;// координата Y левой верхней ячейки 
        float stepX = fieldSizeX / numX + 1;// назначаем шаг по горизонтали
        float stepY = fieldSizeY / numY + 1;// назначаем шаг по вертикали
        this.cellScale = 1; // / numX;
        int counter = 0;// счетчик экземпляров Cell.  По нему же заполняется массив новых коррдинат для пеередвижения ячейки
        for (int i = 0; i < numY; i++)
        {
            for (int j = 0; j < numX; j++)
            {
                this.CellsMaker(pointZeroX + stepX * j, pointZeroY - stepY * i , this.cellScale, counter);
                this.secondPointCell[counter] = new Vector2(pointZeroX + stepX * j, pointZeroY - stepY * i);// заполняем массив координатами
                counter++;
            }
        }
        this.secondPointCell = MixerOfCoordinates(secondPointCell);
    }

    public Vector2 [] MixerOfCoordinates(Vector2 [] array)
    {
        System.Random random = new System.Random();
        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = random.Next (i + 1);
            Vector2 tmp = array[j];
            array [j] = array [i];
            array [i] = tmp;
        }
        return array;
    }
}
