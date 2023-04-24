using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEvents : MonoBehaviour
{
    public delegate void UpdateSquareNumber(int number);
    public static event UpdateSquareNumber onUpdateSquareNumber;
    public static void UpdateSquareNumberMethod(int number)
    {
        if (onUpdateSquareNumber != null)
            onUpdateSquareNumber(number);
    }



    public delegate void SquareSelected(int square_index);
    public static event SquareSelected onSquareSelected;

    public static void SquareSelectedMethod(int square_index)
    {
        if (onSquareSelected != null)
            onSquareSelected(square_index);
    }





    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;
    public static void OnBoardCompletedMethod()
    {
        if (OnBoardCompleted != null)
            OnBoardCompleted();
    }


}



