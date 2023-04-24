using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NumberData : MonoBehaviour
{
    public static List<SudokuData.sudokuBoardData> getData()
    {
        List<SudokuData.sudokuBoardData> data = new List<SudokuData.sudokuBoardData>();
        data.Add(new SudokuData.sudokuBoardData(
            new int[81] { 0, 1, 4, 0, 0, 0, 0, 3, 0, 3, 0, 0, 5, 1, 0, 8, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 6, 0, 0, 1, 8, 0, 0, 6, 0, 0, 0, 0, 3, 2, 5, 6, 4, 0, 0, 0, 0, 6, 0, 0, 7, 2, 0, 0, 9, 0, 0, 7, 0, 0, 0, 4, 0, 0, 0, 5, 0, 8, 4, 0, 0, 2, 0, 4, 0, 0, 0, 0, 7, 1, 0, },
            new int[81] { 2, 1, 4, 6, 7, 8, 9, 3, 5, 3, 6, 9, 5, 1, 2, 8, 7, 4, 5, 8, 7, 4, 3, 9, 1, 2, 6, 4, 2, 1, 8, 9, 3, 6, 5, 7, 7, 9, 3, 2, 5, 6, 4, 8, 1, 8, 5, 6, 1, 4, 7, 2, 9, 3, 9, 3, 2, 7, 6, 1, 5, 4, 8, 1, 7, 5, 9, 8, 4, 3, 6, 2, 6, 4, 8, 3, 2, 5, 7, 1, 9, }));
        return data;
    }
}
//For more difficluties we can add more classes here. We would also need to add more data and correct in the "Grid" Script to call for the current data set instead of this specific one.


public class SudokuData : MonoBehaviour
{
    public static SudokuData Instance;
    public struct sudokuBoardData
    {
        public int[] unsolvedData;
        public int[] solvedData;
        public sudokuBoardData(int[] unsolved, int[] solvedData) : this()
        {
            this.unsolvedData = unsolved;
            this.solvedData = solvedData;
        }
    };

    public List<sudokuBoardData> sudoku_game = new List<sudokuBoardData>();
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
            Instance = this;

        else
            Destroy(this);
    }
    private void Start()
    {
        sudoku_game = NumberData.getData();
    }

}
