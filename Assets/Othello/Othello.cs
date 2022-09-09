using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Othello : MonoBehaviour
{
    int _rows = 8;

    int _cloumns = 8;

    [SerializeField]
    string _record;

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
    GameObject _resultPanel;
    [SerializeField]
    Text _result;
    [SerializeField]
    Text _whiteScore;
    [SerializeField]
    Text _blackScore;
    int WhiteCount = 0;
    int BlackCount = 0;
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
        Record();
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

    void CheckResult()
    {
        var noneCount = 0;
        WhiteCount = 0;
        BlackCount = 0;
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
            if (WhiteCount - BlackCount > 0) { PushResult("White Win"); }
            else if (WhiteCount - BlackCount < 0) { PushResult("Black Win"); }
            else { PushResult("Draw"); }
        }
        else
        {
            if (WhiteCount == 0) { PushResult("Black Win"); }
            else if (BlackCount == 0) { PushResult("White Win"); }
        }
    }
    void PushResult(string winner)
    {
        _resultPanel.SetActive(true);
        _result.text = winner;
        _whiteScore.text = $"White\n{WhiteCount}";
        _blackScore.text = $"Black\n{BlackCount}";
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
            PushCell(_selectR, _selectC);
        }
    }

    private void PushCell(int r, int c)
    {
        if (_cells[r, c].State != State.CanSelect) { return; }
        RecordWrite(r, c);
        
        if (GetNeighbourCells(r, c))
        {
            _cells[r, c].State = _turn == Turn.White ? State.White : State.Black;
            for (int row = 0; row < _rows; row++)
            {
                for (int cloumn = 0; cloumn < _cloumns; cloumn++)
                {
                    if (_cells[row, cloumn].State == State.CanSelect)
                        _cells[row, cloumn].State = State.None;
                }
            }
            StartCoroutine(CellTrun());
        }
    }
    public void GameReset()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if (r == 3 && c == 3 || r == 4 && c == 4) { _cells[r, c].State = State.White; }
                else if (r == 3 && c == 4 || r == 4 && c == 3) { _cells[r, c].State = State.Black; }
                else { _cells[r, c].State = State.None; }
            }
        }
        _cells[4, 4].OnSelected(true);
        _selectDisc = _cells[4, 4];

        SetPredict();
        _record = "";
    }
    void RecordWrite(int r, int c)
    {
        char j = 'A';
        int count = 0;
        while (count < c)
        {
            count++;
            j++;
        }

        _record += $"{j}{r + 1}";
    }
    void Record()
    {
        if (string.IsNullOrEmpty(_record) || _record.Length % 2 != 0) { return; }
        string num = "";
        for (int i = 0; i < _record.Length - 1; i += 2)
        {
            num = _record.Substring(i, 2);

            if (!char.IsLetter(num[0])) { return; }
            if (!char.IsNumber(num[1])) { return; }
            var row = int.Parse(num[1].ToString());
            var count = 1;
            var index = 'A';
            if (char.IsLower(num[0])) { _record = _record.Replace(num[0], char.ToUpper(num[0])); }
            while (index != char.ToUpper(num[0]))
            {
                count++;
                index++;
            }
            var col = count;
            PushCell(row - 1, col - 1);
        }
        _record = _record.Substring(0, _record.Length / 2);
    }
    IEnumerator CellTrun()
    {
        foreach (var cell in _changeCells.ToArray())
        {
            cell.State = _turn == Turn.White ? State.White : State.Black;
            yield return new WaitForSeconds(0.2f);
        }
        _turn = _turn == Turn.White ? Turn.Black : Turn.White;
        _changeCells.Clear();
        CheckResult();
        SetPredict();
        PassCheck();
    }
}
