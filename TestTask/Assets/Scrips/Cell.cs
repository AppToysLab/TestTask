using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Net net;
    public float CellScale;
    private void SetSymbol()
    {

    }
    private void Move()
    {

    }
    
    private void CellDestroy()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        this.net.RestartEvent -= this.CellDestroy;
    }

    void Start()
    {
        this.net = FindObjectOfType<Net>();
        this.net.RestartEvent += this.CellDestroy;
        this.SetSymbol();
       // GetComponent<RectTransform>().localScale = new Vector2(CellScale, CellScale);
    }

}
