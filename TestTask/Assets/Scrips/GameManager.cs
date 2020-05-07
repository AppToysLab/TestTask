using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 fieldSize;
    public int numX;
    public int numY;

    public void SetFieldSize()
    {
        this.fieldSize = new Vector2(530, 600);
    }
    public void SetNumX()
    {
        var input = Input.inputString;
        this.numX = int.Parse(input);
    }
    public void SetNumY()
    {
        var input = Input.inputString;
        this.numY = int.Parse(input);
    }
    void Start()
    {
        this.SetFieldSize();
    }
}
