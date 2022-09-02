using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    Mine = -1,//’n—‹

    None = 0,//‹óƒZƒ‹

    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
}

public class Cell : MonoBehaviour
{
    [SerializeField]
    Text _view = null;
    [SerializeField]
    CellState _cellstate = CellState.None;
    public bool Isopened { get; private set; } = false;
    Minesweeper _gameManager;
    public CellState CellState
    {
        get => _cellstate;
        set
        {
            _cellstate = value;
            OnCellStateChanged();
        }
    }

    void Start()
    {
        _gameManager = FindObjectOfType<Minesweeper>();
        OnCellStateChanged();
    }
    private void OnValidate()
    {
        OnCellStateChanged();
    }

    void OnClick()
    {
        OpenCell();
        OnJudged();
    }
    void OnCellStateChanged()
    {
        if (!_view) { return; }

        if (_cellstate == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else if (_cellstate == CellState.None)
        {
            _view.text = "";
        }
        else
        {
            _view.text = ((int)_cellstate).ToString();
            _view.color = Color.blue;
        }
    }
    public void StartCell()
    {
        if (_gameManager._startCellSet) { return; }

        var row = _gameManager.SelectCellNum(GetComponent<Cell>()).Value.Row;
        var column = _gameManager.SelectCellNum(GetComponent<Cell>()).Value.Column;
        _gameManager.SetMine(row, column);
    }

    public void OpenCell()
    {
        Isopened = true;
        _gameManager.CurrentCellCount++;

        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if (_cellstate == CellState.None)
        {
            this.ThisNearCell();
        }
    }
    void ThisNearCell()
    {
        var row = _gameManager.SelectCellNum(GetComponent<Cell>()).Value.Row;
        var column = _gameManager.SelectCellNum(GetComponent<Cell>()).Value.Column;
        _gameManager.NearCellOpen(row, column);
    }
    public void OnJudged()
    {
        if (_cellstate == CellState.Mine)
        {
            Debug.Log("GameOver");
            _gameManager.Result(1);
            return;
        }
        _gameManager.OnJudged();
    }
}
