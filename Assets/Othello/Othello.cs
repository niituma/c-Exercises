using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Othello : MonoBehaviour
{
    int _rows = 8;

    int _cloumns = 8;

    [SerializeField]
    int _selectR = 0, _selectC = 0;

    [SerializeField]
    Disc _cell;

    Disc _selectDisc;
    enum Turn
    {
        White,
        Black
    }

    List<Disc> _changeCells = new List<Disc>();

    [SerializeField]
    Turn _turn = Turn.White;

    [SerializeField]
    Disc[,] _cells;
    // Start is called before the first frame update
    void Start()
    {
        _cells = new Disc[_rows, _cloumns];
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                var cell = Instantiate(_cell, new Vector3((c - 3.5f) * 1.1f, 0.5f, (-r + 3.5f) * 1.1f), Quaternion.identity, this.transform);
                cell.name = $"cell[{r},{c}]";
                _cells[r, c] = cell;
            }
        }
        _cells[3, 3].State = State.White;
        _cells[3, 4].State = State.Black;
        _cells[4, 3].State = State.Black;
        _cells[4, 4].State = State.White;

        _cells[_selectR, _selectC].OnSelected(true);
        _selectDisc = _cells[_selectR, _selectC];

        SetPredict();
    }



    private void Update()
    {
        Control();
    }

    bool GetNeighbourCells(int r, int c)
    {
        var cellState = _turn == Turn.White ? State.Black : State.White;
        var secondcellState = _turn == Turn.White ? State.White : State.Black;
        var i = 1;
        var canSet = false;
        if (r - 1 >= 0 && _cells[r - 1, c].State == cellState)
        {
            while (r - (i + 1) >= 0)
            {
                i++;
                if (_cells[r - i, c].State == State.None || _cells[r - i, c].State == State.CanSelect) { break; }
                else if (_cells[r - i, c].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r - j, c]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if (r + 1 < _rows && _cells[r + 1, c].State == cellState)
        {
            i = 1;
            while (r + (i + 1) < _rows)
            {
                i++;
                if (_cells[r + i, c].State == State.None || _cells[r + i, c].State == State.CanSelect) { break; }
                else if (_cells[r + i, c].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r + j, c]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if (c - 1 >= 0 && _cells[r, c - 1].State == cellState)
        {
            i = 1;
            while (c - (i + 1) >= 0)
            {
                i++;
                if (_cells[r, c - i].State == State.None || _cells[r, c - i].State == State.CanSelect) { break; }
                else if (_cells[r, c - i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r, c - j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if (c + 1 < _cloumns && _cells[r, c + 1].State == cellState)
        {
            i = 1;
            while (c + (i + 1) < _cloumns)
            {
                i++;
                if (_cells[r, c + i].State == State.None || _cells[r, c + i].State == State.CanSelect) { break; }
                else if (_cells[r, c + i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r, c + j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if ((r - 1 >= 0 && c - 1 >= 0) && _cells[r - 1, c - 1].State == cellState)
        {
            i = 1;
            while (r - (i + 1) >= 0 && c - (i + 1) >= 0)
            {
                i++;
                if (_cells[r - i, c - i].State == State.None || _cells[r - i, c - i].State == State.CanSelect) { break; }
                else if (_cells[r - i, c - i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r - j, c - j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if ((r - 1 >= 0 && c + 1 < _cloumns) && _cells[r - 1, c + 1].State == cellState)
        {
            i = 1;
            while (r - (i + 1) >= 0 && c + (i + 1) < _cloumns)
            {
                i++;
                if (_cells[r - i, c + i].State == State.None || _cells[r - i, c + i].State == State.CanSelect) { break; }
                else if (_cells[r - i, c + i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r - j, c + j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if ((r + 1 < _rows && c - 1 >= 0) && _cells[r + 1, c - 1].State == cellState)
        {
            i = 1;
            while (r + (i + 1) < _rows && c - (i + 1) >= 0)
            {
                i++;
                if (_cells[r + i, c - i].State == State.None || _cells[r + i, c - i].State == State.CanSelect) { break; }
                else if (_cells[r + i, c - i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r + j, c - j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }
        if ((r + 1 < _rows && c + 1 < _cloumns) && _cells[r + 1, c + 1].State == cellState)
        {
            i = 1;
            while (r + (i + 1) < _rows && c + (i + 1) < _cloumns)
            {
                i++;
                if (_cells[r + i, c + i].State == State.None || _cells[r + i, c + i].State == State.CanSelect) { break; }
                else if (_cells[r + i, c + i].State == secondcellState)
                {
                    for (int j = 1; j < i; j++)
                    {
                        _changeCells.Add(_cells[r + j, c + j]);
                    }
                    canSet = true;
                    break;
                }
            }
        }

        return canSet;
    }
    private void SetPredict()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if ((_cells[r, c].State == State.None || _cells[r, c].State == State.CanSelect) && GetNeighbourCells(r, c))
                {
                    _cells[r, c].State = State.CanSelect;
                    _changeCells.Clear();
                }
                else if ((_cells[r, c].State == State.None || _cells[r, c].State == State.CanSelect) && !GetNeighbourCells(r, c))
                {
                    _cells[r, c].State = State.None;
                }
            }
        }
    }

    private void PassCheck()
    {
        var count = 0;

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                count++;
                if (_cells[r, c].State == State.CanSelect) { return; }
                if (count == _rows * _cloumns)
                {
                    _turn = _turn == Turn.White ? Turn.Black : Turn.White;
                    SetPredict();
                    return;
                }
            }
        }
    }

    void Result()
    {
        var noneCount = 0;
        var WhiteCount = 0;
        var BlackCount = 0;
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if (_cells[r, c].State == State.White) { WhiteCount++; }
                else if (_cells[r, c].State == State.Black) { BlackCount++; }
                else { noneCount++; }
            }
        }

        if (noneCount == 0)
        {
            if (WhiteCount - BlackCount > 0) { Debug.Log("White"); }
            else if (WhiteCount - BlackCount < 0) { Debug.Log("Black"); }
            else { Debug.Log("Draw"); }
        }
        else
        {
            if (WhiteCount == 0) { Debug.Log("Black"); }
            else if (BlackCount == 0) { Debug.Log("White"); }
        }
    }
    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_selectR - 1 < 0) { return; }
            _selectR--;
            _selectDisc.OnSelected(false);
            _cells[_selectR, _selectC].OnSelected(true);
            _selectDisc = _cells[_selectR, _selectC];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_selectR + 1 >= _rows) { return; }
            _selectR++;
            _selectDisc.OnSelected(false);
            _cells[_selectR, _selectC].OnSelected(true);
            _selectDisc = _cells[_selectR, _selectC];
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_selectC + 1 >= _cloumns) { return; }
            _selectC++;
            _selectDisc.OnSelected(false);
            _cells[_selectR, _selectC].OnSelected(true);
            _selectDisc = _cells[_selectR, _selectC];
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_selectC - 1 < 0) { return; }
            _selectC--;
            _selectDisc.OnSelected(false);
            _cells[_selectR, _selectC].OnSelected(true);
            _selectDisc = _cells[_selectR, _selectC];
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_cells[_selectR, _selectC].State != State.CanSelect) { return; }

            if (GetNeighbourCells(_selectR, _selectC))
            {
                _cells[_selectR, _selectC].State = _turn == Turn.White ? State.White : State.Black;
                foreach (var cell in _changeCells)
                {
                    cell.State = _turn == Turn.White ? State.White : State.Black;
                }
                _turn = _turn == Turn.White ? Turn.Black : Turn.White;
            }

            _changeCells.Clear();

            Result();
            SetPredict();
            PassCheck();
        }
    }
}
