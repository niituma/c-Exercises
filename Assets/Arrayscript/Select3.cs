using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Select3 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    int _row = 5;
    [SerializeField]
    int _column = 5;

    int _count = 0;

    DateTime _dateTime;
    TimeSpan _timeSpan;

    [SerializeField] int _colorcount = 0;

    [SerializeField] TextMeshProUGUI _countText;
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] GameObject ClearPanel;
    GameObject[,] _cells;
    GridLayoutGroup _group;
    private void Start()
    {
        _dateTime = DateTime.Now;
        _group = GetComponent<GridLayoutGroup>();
        _group.constraintCount = _column;
        _cells = new GameObject[_row, _column];
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                var randomcolor = Color.white;
                var f = UnityEngine.Random.value;
                if (_colorcount < _row && f > 0.7f)
                {
                    randomcolor = Color.black;
                    _colorcount++;
                }
                cell.AddComponent<Image>().color = randomcolor;

                _cells[r, c] = cell;
            }
        }
        if ((_colorcount == 1 || _colorcount == 0) && _row == 2 && _column == 2 )
        {
            while (_colorcount <= 1)
            {
                for (var r = 0; r < _row; r++)
                {
                    for (var c = 0; c < _column; c++)
                    {
                        var f = UnityEngine.Random.value;
                        if (_cells[r, c].GetComponent<Image>().color != Color.black && f > 0.7f && _colorcount < _row + 1)
                        {
                            _cells[r, c].GetComponent<Image>().color = Color.black;
                            _colorcount++;
                            if (_colorcount >= 2 && f > 0.9f)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
        else if(_colorcount == 0)
        {
            while (_colorcount == 0)
            {
                for (var r = 0; r < _row; r++)
                {
                    for (var c = 0; c < _column; c++)
                    {
                        var f = UnityEngine.Random.value;
                        if (_cells[r, c].GetComponent<Image>().color != Color.black && f > 0.7f && _colorcount < _row)
                        {
                            _cells[r, c].GetComponent<Image>().color = Color.black;
                            _colorcount++;
                        }
                    }
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _count++;
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var image = cell.GetComponent<Image>();
        if (image.color != Color.black)
        {
            image.color = Color.black;
        }
        else
        {
            image.color = Color.white;
        }

        int row = 0;
        int column = 0;

        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                if (_cells[r, c].gameObject == cell)
                {
                    row = r;
                    column = c;

                    break;
                }
            }
        }
        if (row - 1 >= 0 && _cells[row - 1, column].GetComponent<Image>())
        {
            var cellimage = _cells[row - 1, column].GetComponent<Image>();
            if (cellimage.color != Color.black)
            {
                cellimage.color = Color.black;
            }
            else
            {
                cellimage.color = Color.white;
            }

        }
        if (row + 1 <= _row - 1 && _cells[row + 1, column].GetComponent<Image>())
        {
            var cellimage = _cells[row + 1, column].GetComponent<Image>();
            if (cellimage.color != Color.black)
            {
                cellimage.color = Color.black;
            }
            else
            {
                cellimage.color = Color.white;
            }

        }
        if (column - 1 >= 0 && _cells[row, column - 1].GetComponent<Image>())
        {
            var cellimage = _cells[row, column - 1].GetComponent<Image>();
            if (cellimage.color != Color.black)
            {
                cellimage.color = Color.black;
            }
            else
            {
                cellimage.color = Color.white;
            }

        }
        if (column + 1 <= _column - 1 && _cells[row, column + 1].GetComponent<Image>())
        {
            var cellimage = _cells[row, column + 1].GetComponent<Image>();
            if (cellimage.color != Color.black)
            {
                cellimage.color = Color.black;
            }
            else
            {
                cellimage.color = Color.white;
            }

        }
        if (Judge())
        {
            Debug.Log("gameclear");
            ClearPanel.SetActive(true);
            _timeSpan = DateTime.Now - _dateTime;
            _timeText.text = $"Time {_timeSpan.Hours}:{_timeSpan.Minutes}:{_timeSpan.Seconds}:{_timeSpan.Milliseconds}";
            _countText.text = $"Count {_count}";
        }
    }
    bool Judge()
    {
        for (var r = 0; r < _row; r++)
        {
            for (var c = 0; c < _column; c++)
            {
                if (_cells[r, c].GetComponent<Image>().color == Color.white)
                    return false;
            }
        }
        return true;
    }
}
