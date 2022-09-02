using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class Minesweeper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int _rows = 10;

    [SerializeField]
    int _cloumns = 10;

    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    Cell _cellPrefab = null;

    [SerializeField]
    int _mineCount = 0;

    [SerializeField]
    GameObject _panel = null;

    public bool _finish { set; get; } = false;

    public bool _startCellSet { get; private set; } = false;


    int MaxCurrent;

    public int CurrentCellCount { get; set; } = 0;
    Cell[,] _cells;
    Timer _timer;

    private void Start()
    {
        _timer = GetComponent<Timer>();
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _cloumns;
        _cells = new Cell[_rows, _cloumns];

        MaxCurrent = _rows * _cloumns;

        if (MaxCurrent <= _mineCount)
        {
            Debug.LogWarning("地雷の数が多すぎてゲーム出来ません。 地雷の数を遊べる最大数にします");
            _mineCount = _rows > 3 || _cloumns > 3 ? MaxCurrent - 9 : MaxCurrent - 1;
        }

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                _cells[r, c] = cell;
            }
        }
    }

    private Cell[] GetNeighbourCells(int r, int c)
    {
        var cells = new List<Cell>();
        if (r + 1 < _rows) cells.Add(_cells[r + 1, c]);
        if (c + 1 < _cloumns) cells.Add(_cells[r, c + 1]);
        if (r + 1 < _rows && c + 1 < _cloumns) cells.Add(_cells[r + 1, c + 1]);
        if (r - 1 >= 0) cells.Add(_cells[r - 1, c]);
        if (c - 1 >= 0) cells.Add(_cells[r, c - 1]);
        if (r - 1 >= 0 && c - 1 >= 0) cells.Add(_cells[r - 1, c - 1]);
        if (r + 1 < _rows && c - 1 >= 0) cells.Add(_cells[r + 1, c - 1]);
        if (r - 1 >= 0 && c + 1 < _cloumns) cells.Add(_cells[r - 1, c + 1]);
        return cells.ToArray();
    }
    public void NearCellOpen(int r, int c)
    {
        foreach (var cell in GetNeighbourCells(r, c))
        {
            if (cell.transform.GetChild(1).gameObject.activeSelf && cell.CellState != CellState.Mine)
            {
                cell.OpenCell();
            }
        }
    }
    public void SetMine(int Row, int Column)
    {
        for (int i = 0; i < _mineCount; i++)
        {
            while (true)
            {
                var r = Random.Range(0, _rows);
                var c = Random.Range(0, _cloumns);
                var mine = _cells[r, c];

                if ((Row, Column) == (r, c)) continue;

                if (MaxCurrent > 9)
                {
                    if (Row + 1 < _rows && (Row + 1, Column) == (r, c)) continue;

                    if (Column + 1 < _cloumns && (Row, Column + 1) == (r, c)) continue;

                    if (Row + 1 < _rows && Column + 1 < _cloumns && (Row + 1, Column + 1) == (r, c)) continue;

                    if (Row - 1 >= 0 && (Row - 1, Column) == (r, c)) continue;

                    if (Column - 1 >= 0 && (Row, Column - 1) == (r, c)) continue;

                    if (Row - 1 >= 0 && Column - 1 >= 0 && (Row - 1, Column - 1) == (r, c)) continue;

                    if (Row + 1 < _rows && Column - 1 >= 0 && (Row + 1, Column - 1) == (r, c)) continue;

                    if (Row - 1 >= 0 && Column + 1 < _cloumns && (Row - 1, Column + 1) == (r, c)) continue;
                }


                if (mine.CellState != CellState.Mine)
                {
                    mine.CellState = CellState.Mine;
                    break;
                }
            }
        }

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if (_cells[r, c].CellState == CellState.Mine)
                {
                    foreach (var cell in GetNeighbourCells(r, c))
                    {
                        if (cell.CellState != CellState.Mine)
                        {
                            cell.CellState++;
                        }
                    }
                }
            }
        }
        _startCellSet = true;
    }
    public (int Row, int Column)? SelectCellNum(Cell cell)
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if (_cells[r, c].gameObject == cell.gameObject)
                {
                    return (r, c);
                }
            }
        }
        return null;
    }
    public void OnJudged()
    {
        if (CurrentCellCount == MaxCurrent - _mineCount)
        {
            Debug.Log("GameClear");
            Result(0);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            var cellflag = eventData.pointerCurrentRaycast.gameObject.GetComponent<Text>();
            if (cellflag && cellflag.text == "○") { return; };
            var cell = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Cell>();
            if (!cell || cell.Isopened) { return; }

            cell.StartCell();
            cell.OpenCell();
            cell.OnJudged();
        }
        else if (eventData.pointerId == -2)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name != "Flag")
            {
                return;
            }
            var cellflag = eventData.pointerCurrentRaycast.gameObject.GetComponent<Text>();
            cellflag.text = cellflag.text == "" ? "○" : "";
        }
    }

    /// <summary>
    /// <para>num = 0 GameOver</para> 
    /// <para>num = 1 GameClear</para> 
    /// </summary>
    /// <param name="num"></param>
    public void Result(int num)
    {
        if (num == 0)
        {
            _panel.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            _panel.transform.GetChild(1).gameObject.SetActive(true);
        }
        _panel.transform.GetChild(2).GetComponent<Text>().text
            = $"タイム\n{_timer._minute.ToString("D2")} : {((int)_timer._second).ToString("D2")}";
        _panel.SetActive(true);
        GetComponent<Timer>().enabled = false;
    }

}
