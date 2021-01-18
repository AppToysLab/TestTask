using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public string[] content;
    public Font fntCell;

    private Font SetFontCell()// подгрузка шрифта из ресурсов
    {
        Font fntCell = Resources.Load<Font>("Font/fntCell");
        return fntCell;
    }
    
    public string [] ContentMaker(int numAlphabet, int numX, int numY) //метод формирования контента
    {
        char[] allSymbols = new char[numAlphabet];//формируем пулл возможных символов
        int firstSymbol = 65;

        for(int i = 0; i < allSymbols.Length; i++)
        {
            allSymbols[i] = (char) (i + firstSymbol); // диапазон символов с 65 по 90
        }

        int numbersOfContent = numX * numY;//  определяем необходимое колмчество символов для сетки
        string [] contentSymbols = new string [numbersOfContent];
        for (int i = 0; i < contentSymbols.Length; i++)
        {
            contentSymbols[i] = allSymbols[Random.Range(0, allSymbols.Length)].ToString() ;// заполняем массив контента случайными значениями
        }

        return contentSymbols;
    }
    
    void Start()
    {
        this.fntCell = this.SetFontCell();
        this.content = this.ContentMaker(26, 5, 5);
    }
}
