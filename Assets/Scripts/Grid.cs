using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    public int columns = 0;
    public int rows = 0;
    public float squareOffset = 0.0f;
    public GameObject grid_Square;
    public Vector2 start_Position = new Vector2(0.0f, 0.0f);
    public float square_Scale = 1.0f;
    public Color lineHighlightColor = Color.red;




    private List<GameObject> grid_Squares = new List<GameObject>();
    public SudokuData DataStorage;



    void Start()
    {
        if (grid_Square.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("No GridSquare Script attached to game object!");
        }
        CreateGrid();
        SetGridNumbers();
        DataStorage = FindObjectOfType<SudokuData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DataStorage = FindObjectOfType<SudokuData>();
            Debug.Log(DataStorage.name);
        }
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPositions();

    }
    private void SpawnGridSquares()
    {
        int square_index = 0;


        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                grid_Squares.Add(Instantiate(grid_Square) as GameObject);
                grid_Squares[grid_Squares.Count - 1].GetComponent<GridSquare>().SetSqureIndex(square_index);
                grid_Squares[grid_Squares.Count - 1].transform.parent = this.transform;
                grid_Squares[grid_Squares.Count - 1].transform.localScale = new Vector3(square_Scale, square_Scale, square_Scale);
                square_index++;
            }
        }
    }
    private void SetSquaresPositions()
    {
        var square_rectangle = grid_Squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rectangle.rect.width * square_rectangle.transform.localScale.x + squareOffset;
        offset.y = square_rectangle.rect.height * square_rectangle.transform.localScale.y + squareOffset;

        int column_number = 0;
        int row_number = 0;
        foreach (GameObject square in grid_Squares)
        {
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;
            }
            var pos_x_offset = offset.x * column_number;
            var pos_y_offset = offset.y * row_number;
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_Position.x + pos_x_offset, start_Position.y - pos_y_offset);
            column_number++;
        }

    }
    private void SetGridNumbers()
    {
        //Debug.LogError(DataStorage.name);
        //var data = DataStorage.sudoku_game;
        var data = SudokuData.Instance.sudoku_game;
        SetGridSquareData(data[0]);
    }
    private void SetGridSquareData(SudokuData.sudokuBoardData data)
    {
        for (int i = 0; i < grid_Squares.Count; i++)
        {
            grid_Squares[i].GetComponent<GridSquare>().SetNumber(data.unsolvedData[i]);
            grid_Squares[i].GetComponent<GridSquare>().SetCorrectNumber(data.solvedData[i]);
            grid_Squares[i].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolvedData[i] != 0 && data.unsolvedData[i] == data.solvedData[i]);
        }
    }
    private void OnEnable()
    {
        GameEvents.onSquareSelected += OnSquareSelected;
        GameEvents.onUpdateSquareNumber += CheckBoardCompletion;
    }
    private void OnDisable()
    {
        GameEvents.onSquareSelected -= OnSquareSelected;
        GameEvents.onUpdateSquareNumber -= CheckBoardCompletion;

    }
    private void SetSqColor(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = grid_Squares[index].GetComponent<GridSquare>();
            if (comp.HasWrongValue() == false && comp.isSelected() == false)
            {
                comp.SetSquareColor(col);
            }
        }
    }


    public void OnSquareSelected(int square_Index)
    {
        var horizontal_line = AreaIndicator.Instance.GetHorizontalLine(square_Index);
        var vertical_line = AreaIndicator.Instance.GetVerticalLine(square_Index);
        var square = AreaIndicator.Instance.GetSquareArea(square_Index);


        SetSqColor(AreaIndicator.Instance.GetAllSquares(), Color.white);
        SetSqColor(horizontal_line, lineHighlightColor);
        SetSqColor(vertical_line, lineHighlightColor);
        SetSqColor(square, lineHighlightColor);

    }
    private void CheckBoardCompletion(int number)
    {
        foreach (var square in grid_Squares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.isSetCompleted() == false)
            {
                return;
            }
        }
        GameEvents.OnBoardCompletedMethod();
    }
    public void SolveGame()
    {
        foreach(var square in grid_Squares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }
        CheckBoardCompletion(0);
    }

}
