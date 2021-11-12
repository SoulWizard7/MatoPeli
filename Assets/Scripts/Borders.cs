using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    private LineRenderer _line;
    public float offset = -0.5f;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        CreateBorders();
    }

    private void CreateBorders()
    {
        int _boardSize = WormController.boardSize + 1;
        _line.positionCount = 5;
        
        _line.SetPosition(0, new Vector3(_boardSize, offset, _boardSize));
        _line.SetPosition(1, new Vector3(_boardSize, offset, -_boardSize));
        _line.SetPosition(2, new Vector3(-_boardSize, offset, -_boardSize));
        _line.SetPosition(3, new Vector3(-_boardSize, offset, _boardSize));
        _line.SetPosition(4, new Vector3(_boardSize, offset, _boardSize));
        
    }
}
