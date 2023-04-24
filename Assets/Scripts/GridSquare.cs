using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public int number = 0;
    public int correctNumber = 0;


    private bool selected = false;
    private int selected_index = -1;


    private bool hasDefaultValue = false;
    private bool hasWrongValue = false;

    public bool isSetCompleted() { return number == correctNumber; }
    public bool HasWrongValue() { return hasWrongValue; }


    public void SetHasDefaultValue(bool has_default_value)
    {
        hasDefaultValue = has_default_value;
    }
    public bool GetHasDefaultValue()
    {
        return hasDefaultValue;
    }




    public bool isSelected() { return selected; }
    public void SetSqureIndex(int index)
    {
        selected_index = index;
    }


    public void SetCorrectNumber(int rightNumber)
    {
        correctNumber = rightNumber;
        hasWrongValue = false;
    }
    public void SetCorrectNumber()
    {
        number = correctNumber;
        
    }



    protected override void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DisplayText()
    {
        if (number <= 0)
        {
            number_text.GetComponent<Text>().text = " ";
        }
        else
            number_text.GetComponent<Text>().text = number.ToString();
    }
    public void SetNumber(int incNumber)
    {
        number = incNumber;
        DisplayText();



    }


    public void OnPointerClick(PointerEventData eventData)
    {
        selected = true;
        GameEvents.SquareSelectedMethod(selected_index);
    }
    public void OnSubmit(BaseEventData eventData)
    {

    }
    private void OnEnable()
    {
        GameEvents.onUpdateSquareNumber += OnSetNumber;
        GameEvents.onSquareSelected += OnSquareSelected;
    }
    private void OnDisable()
    {
        GameEvents.onUpdateSquareNumber -= OnSetNumber;
        GameEvents.onSquareSelected -= OnSquareSelected;

    }
    public void OnSetNumber(int number)
    {


        if (selected && hasDefaultValue == false)
        {
            SetNumber(number);
            if (number != correctNumber)
            {
                hasWrongValue = true;
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

            }
            else
            {
                hasWrongValue = false;
                hasDefaultValue = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
            }
        }
    }
    public void OnSquareSelected(int square_Index)
    {
        if (square_Index != selected_index)
        {
            selected = false;
        }
    }
    public void SetSquareColor(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }
}

