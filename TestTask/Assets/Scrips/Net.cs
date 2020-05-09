using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Net : MonoBehaviour
{
    public float cellScale;
    public Vector2 [] secondPointCell;
    public Font fntCell;
    public ContentManager contentManager;
    public GameManager gameManager;

    public delegate void Restart();
    public event Restart RestartEvent;

    public void CellsMaker(float x, float y, float scaleSymbol, int counterCell) // метод создания ячейки
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        var canvasTransform = canvas.GetComponent<Transform>();
        GameObject Cell = new GameObject("Cell");
        Cell.AddComponent<Cell>();
        Cell.AddComponent<Text>();
        this.fntCell = contentManager.fntCell;
        Cell.GetComponent<Cell>().numberOfCell = counterCell;//передаем номер ячейки
        var cellTxt = Cell.GetComponent<Text>();
        cellTxt.font = this.fntCell;
        cellTxt.fontSize = 60; 
        cellTxt.fontStyle = FontStyle.Bold;
        cellTxt.color = Color.gray;
        cellTxt.alignment = TextAnchor.MiddleCenter;
        
        var cellTransform = Cell.GetComponent<RectTransform>();
        cellTransform.SetParent(canvasTransform);// устанавливаем Canvas как родителя
        cellTransform.anchorMax = new Vector2(0, 1);
        cellTransform.anchorMin = new Vector2(0, 1);
        cellTransform.localScale = new Vector2( scaleSymbol, scaleSymbol);
        Cell.transform.localPosition = new Vector2(x, y);// присваиваем координаты

        cellTxt.text = contentManager.content[Random.Range(0, contentManager.content.Length)];// назначаем ячейке букву
    }

    public void CellsGenerate()// метод генерации и расстановки ячеек. Вызывается из UI.Button
    {
        if(GameObject.Find("Cell") != null)
        {
            RestartEvent();// Событие на удаление ранее созданных экземпляров Cell 
        }
        
        this.CellSetuper(gameManager.fieldSize.x, gameManager.fieldSize.y, gameManager.numX, gameManager.numY);
    }


    public void CellSetuper( float fieldSizeX, float fieldSizeY, int numX, int numY)
    {
        var numSymbols = numX * numY;
        this.secondPointCell = new Vector2 [numSymbols];//готовим массив под новые координаты ячейки

        float pointZeroX = -(fieldSizeX / numX + 1) * (numX - 1) / 2; // координата Х левой верхней ячейки ( левая граница поля)
        float pointZeroY = 170 + fieldSizeY / 2;// координата Y левой верхней ячейки 
        float stepX = fieldSizeX / numX + 1;// назначаем шаг по горизонтали
        float stepY = fieldSizeY / numY + 1;// назначаем шаг по вертикали
        this.cellScale = 1;
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

        this.secondPointCell = this.MixerOfCoordinates(secondPointCell);
    }

    public Vector2 [] MixerOfCoordinates(Vector2 [] array)
    {
        var tempArray = new Vector2 [array.Length];
        Vector2 empty = new Vector2(-999999, -999999);// для забивки использованной ячейки массива

        for (int counterTempArray = 0; counterTempArray < tempArray.Length; counterTempArray ++)
        {
            System.Random random = new System.Random();

            while (true)
            {
                int counterArray = random.Next(0, tempArray.Length);

                if ((counterArray != counterTempArray) && (array[counterArray] != empty))
                {
                    tempArray[counterTempArray] = array[counterArray];
                    array[counterArray] = empty;
                    break;
                }
            }
        }
        return tempArray;
    }

    public void ReMixCoord(Vector2[] array)// метод перемешивания. Вызывается из GameManager
    {
        var tempArray = new Vector2[array.Length];
        Vector2 empty = new Vector2(-999999, -999999);// для забивки использованной ячейки массива

        for (int counterTempArray = 0; counterTempArray < tempArray.Length; counterTempArray++)
        {
            System.Random random = new System.Random();

            while (true)
            {
                int counterArray = random.Next(0, tempArray.Length);

                if ((counterArray != counterTempArray) && (array[counterArray] != empty))
                {
                    tempArray[counterTempArray] = array[counterArray];
                    array[counterArray] = empty;
                    break;
                }
            }
        }
        this.secondPointCell = tempArray;
    }

    private void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();
        this.contentManager = FindObjectOfType<ContentManager>();
    }

    // ------ ниже указанный метод перемешивания более быстрый, чем мой, но он не гарантирует смену позиции именно каждой ячейки.

    //System.Random random = new System.Random(); 
    //for (int i = array.Length - 1; i >= 1; i--)
    //{
    //    int j = random.Next (i + 1);
    //    Vector2 tmp = array[j];
    //    array [j] = array [i];
    //    array [i] = tmp;
    //}

}
