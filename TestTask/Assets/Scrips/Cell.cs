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
    public int numberOfCell;
    public Vector2  nextPoint;
    public float speed = 2;
    private bool startMoving;

    public void MoveToSecondPoint()
    {
        startMoving = false;
        nextPoint = net.secondPointCell[numberOfCell];// берем из перемешанного массива
        

        this.startMoving = true;
    }

    private void Move(Vector2 nextPoint, float speed)
    {
        if (startMoving == true)
        {
            transform.position = Vector3.Lerp(transform.position, nextPoint, speed * Time.deltaTime);
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
        this.net.RestartEvent += this.CellDestroy;
        this.gameManager = FindObjectOfType<GameManager>();
        this.gameManager.MoveToNextPoint += this.MoveToSecondPoint;
       // GetComponent<RectTransform>().localScale = new Vector2(CellScale, CellScale);
    }
    private void FixedUpdate()
    {
        this.Move(nextPoint, speed);
    }

}
