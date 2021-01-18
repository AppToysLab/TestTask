using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Vector2 fieldSize; // размер поля, в котором отображаются ячейки 
    public int numX;// количество экземпляров Cell по горизонтали
    public int numY;// количество экземпляров Cell  по вертикали
    public Cell cell;

    public delegate void Remove();
    public event Remove EvMoveToNextPoint;// событие на перемещение ячейки 

    public void SetEvents()
    {
        this.cell = FindObjectOfType<Cell>();
        this.EvMoveToNextPoint += cell.MoveToSecondPoint;
    }

    public void UnSubscribeToEVents()
    {
        this.EvMoveToNextPoint -= cell.MoveToSecondPoint;
    }

    public void CellMixer()// метод перемешивания. Вызывается из UI.Button
    {
        Net net = FindObjectOfType<Net>();
        if ((net.secondPointCell != null) && (GameObject.Find("Cell") != null))
        {
            System.GC.Collect();//сборка мусора 
            net.ReMixCoord(net.secondPointCell);//повторное перемешивание
            this.EvMoveToNextPoint();// событие на предвижение. Подписан  Cell
        }
        
    }

    public void SetFieldSize()// назначение размеров поля
    {
        this.fieldSize = new Vector2(560, 600);
    }

    public void SetNumX()// назначается из UI элемента
    {
        var input = Input.inputString;
        var inputX = int.Parse(input);
        if (inputX != 0)
            {
                this.numX = int.Parse(input);
            }
        
    }

    public void SetNumY()// назначается из UI элемента
    {
        var input = Input.inputString;
        var inputY = int.Parse(input);
        if (inputY != 0)
        {
            this.numY = int.Parse(input);
        }
    }

    void Start()
    {
        this.numX = 3;//задаем значение по умолчанию
        this.numY = 4;
        this.SetFieldSize();
        
       // EvMoveToNextPoint += cell.MoveToSecondPoint;
    }
}
