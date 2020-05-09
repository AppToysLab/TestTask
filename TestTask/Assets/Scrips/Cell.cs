using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Net net;
    private GameManager gameManager;
    public float CellScale;
    public int numberOfCell;// назначается при генерации
    private Vector2  nextPoint;//координаты для передвижения назначаются при генерации
    private float speed = 4;
    private bool startMoving;

    public void MoveToSecondPoint()// вызывается при наступлении события передвижения
    {
        startMoving = false;
        nextPoint = net.secondPointCell[numberOfCell];// берем из перемешанного массива
        this.startMoving = true;
    }

    private void Move(Vector2 nextPoint, float speed)//непосредственно метод передвижения
    {
        if (startMoving == true)
        {
            transform.position = Vector2.Lerp(transform.position, nextPoint, Time.deltaTime * speed);
        }
    }
    private void CellDestroy()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        this.net.RestartEvent -= this.CellDestroy;
        this.gameManager.MoveToNextPoint -= this.MoveToSecondPoint;
    }

    void Start()
    {
        this.net = FindObjectOfType<Net>();
        this.net.RestartEvent += this.CellDestroy;// подписываемся на событие уничтожения

        this.gameManager = FindObjectOfType<GameManager>();
        this.gameManager.MoveToNextPoint += this.MoveToSecondPoint;// подписываемся на событие передвижения
    }
    private void Update()
    {
        this.Move(nextPoint, speed);
    }
}
