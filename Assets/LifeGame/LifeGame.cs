using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int _rows = 10;

    [SerializeField]
    int _cloumns = 10;

    [SerializeField]
    int _maxsize = 50;

    [SerializeField]
    LifeCell _cellPrefab = null;

    LifeCell[,] _cells;

    [SerializeField] InputField _buttonText;

    bool _isloop = false;

    GridLayoutGroup _gridLayoutGroup = null;
    // Start is called before the first frame update
    void Start()
    {
        if(_rows > _maxsize) { _rows = _maxsize; }
        if(_cloumns > _maxsize) { _cloumns = _maxsize; }
        if (_rows <= 0) { _rows = 1; }
        if (_cloumns <= 0) { _cloumns = 1; }
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _cloumns;
        _cells = new LifeCell[_rows, _cloumns];

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                _cells[r, c] = cell;
            }
        }
    }

    public void StartLoop()
    {
        if (_isloop) { return; }
        StartCoroutine(LoopLife());
    }

    public void StopLoop()
    {
        _isloop = false;
    }
    public void ResetCell()
    {
        StopLoop();

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                _cells[r, c].State = LifeCellState.Dead;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            var cell = eventData.pointerCurrentRaycast.gameObject.GetComponent<LifeCell>();
            if (cell)
            {
                cell.State = cell.State == LifeCellState.Live? LifeCellState.Dead: LifeCellState.Live;
            }
        }
    }
    IEnumerator LoopLife()
    {
        _isloop = true;
        while (_isloop)
        {
            yield return new WaitForSeconds(0.1f);
            NextLife();
        }
    }

    public void OnNextLife()
    {
        if (_isloop) { return;}
        NextLife();
    }

    public void OnRandamCell()
    {
        if (_isloop) { StopLoop(); }
        RandamCellGenerate();
    }
    public void OnGeneration()
    {
        if (_buttonText.text == null || _isloop) { return; }
        int s = 0;
        bool result = int.TryParse(_buttonText.text, out s);
        if (!result) { return; }
        int num = int.Parse(_buttonText.text);
        for (int i = 0; i < num; i++)
        {
            NextLife();
        }

        _buttonText.text = null;
    }

    void NextLife()
    {
        LifeCellState[,] _lifeStates = new LifeCellState[_rows, _cloumns];

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                var Lifecount = 0;
                var isLife = false;

                foreach (var cell in GetNeighbourCells(r, c))
                {
                    if (IsLifeCell(cell)) { Lifecount++; }
                }

                if (!IsLifeCell(_cells[r, c]) && Lifecount == 3)
                {
                    isLife = true;
                }
                else if (IsLifeCell(_cells[r, c]) && (Lifecount == 3 || Lifecount == 2))
                {
                    isLife = true;
                }

                _lifeStates[r, c] = isLife ? LifeCellState.Live : LifeCellState.Dead;
            }
        }
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                _cells[r, c].State = _lifeStates[r, c];
            }
        }
    }
    private LifeCell[] GetNeighbourCells(int r, int c)
    {
        var cells = new List<LifeCell>();
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
    bool IsLifeCell(LifeCell cell)
    {
        return cell.State == LifeCellState.Live;
    }
    void RandamCellGenerate()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                _cells[r, c].State
                    = Random.Range(0, 100) < 12 ? LifeCellState.Live : LifeCellState.Dead;
            }
        }
    }
}
