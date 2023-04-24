using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{

    public GameObject winPopup;

    private void Start()
    {
        winPopup.SetActive(false);

    }
    private void OnBoardCompelted()
    {
        winPopup.SetActive (true);
    }
    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += OnBoardCompelted;
    }
    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= OnBoardCompelted;
    }






}
