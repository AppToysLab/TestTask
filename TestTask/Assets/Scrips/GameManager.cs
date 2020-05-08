using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Vector2 fieldSize; // размер поля, в котором отображаются ячейки 
    public int numX;// количество экземпляров Cell по горизонтали
    public int numY;// количество экземпляров Cell  по вертикали

    public delegate void Remove();
    public event Remove MoveToNextPoint;// событие на перемещение ячейки 

    public void CellMixer()// метод перемешивания
    {
        System.GC.Collect();//сборка мусора 
        Net net = FindObjectOfType<Net>();
        net.MixerOfCoordinates(net.secondPointCell);//повторное перемешивание
        this.MoveToNextPoint();// событие на предвижение. Слушатель в экз Cell
    }

    public void SetFieldSize()// назначение размеров поля
    {
        this.fieldSize = new Vector2(530, 600);
    }
    public void SetNumX()// назначается из UI элемента
    {
        var input = Input.inputString;
        this.numX = int.Parse(input);
    }
    public void SetNumY()// назначается из UI элемента
    {
        var input = Input.inputString;
        this.numY = int.Parse(input);
    }
    void Start()
    {
        this.numX = 3;//задаем значение по умолчанию
        this.numY = 4;
        this.SetFieldSize();
    }
}
