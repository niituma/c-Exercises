using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select2 : MonoBehaviour
{
    [SerializeField]
    int _row = 5;
    [SerializeField]
    int _column = 5;

    GameObject[,] _cells;

    GridLayoutGroup _layout;

    int _selectedrowIndex;
    int _selectedcolumnIndex;
    private void Start()
    {
        _layout = GetComponent<GridLayoutGroup>();
        _layout.constraintCount = _column;
        _cells = new GameObject[_row, _column];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                _cells[r, c] = obj;
            }
        }

        OnSelectedChanged();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            //if (_selectedcolumnIndex > 0 && _cells[_selectedrowIndex, _selectedcolumnIndex - 1])
            //{
            //    _selectedcolumnIndex--;
            //    OnSelectedChanged();
            //}

            if (_selectedcolumnIndex > 0 && _cells[_selectedrowIndex, _selectedcolumnIndex - 1].GetComponent<Image>())
            {
                _selectedcolumnIndex--;
                OnSelectedChanged();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            //if (_selectedcolumnIndex < _column - 1 && _cells[_selectedrowIndex, _selectedcolumnIndex + 1])
            //{
            //    _selectedcolumnIndex++;
            //    OnSelectedChanged();
            //}

            if (_selectedcolumnIndex < _column - 1 && _cells[_selectedrowIndex, _selectedcolumnIndex + 1].GetComponent<Image>())
            {
                _selectedcolumnIndex++;
                OnSelectedChanged();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {
            //if (_selectedrowIndex > 0 && _cells[_selectedrowIndex - 1, _selectedcolumnIndex])
            //{
            //    _selectedrowIndex--;
            //    OnSelectedChanged();
            //}

            if (_selectedrowIndex > 0 && _cells[_selectedrowIndex - 1, _selectedcolumnIndex].GetComponent<Image>())
            {
                _selectedrowIndex--;
                OnSelectedChanged();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            //if (_selectedrowIndex < _row - 1 && _cells[_selectedrowIndex + 1, _selectedcolumnIndex])
            //{
            //    _selectedrowIndex++;
            //    OnSelectedChanged();
            //}

            if (_selectedrowIndex < _row - 1 && _cells[_selectedrowIndex + 1, _selectedcolumnIndex].GetComponent<Image>())
            {
                _selectedrowIndex++;
                OnSelectedChanged();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (_cells[_selectedrowIndex,_selectedcolumnIndex])
            //{
            //    for (int r = _row - 1; r >= 0; r--)
            //    {
            //        for (int c = _column - 1; c >= 0; c--)
            //        {
            //            if (_cells[r,c])
            //            {
            //                Destroy(_cells[r,c]);
            //                return;
            //            }
            //        }
            //    }
            //}

            Destroy(_cells[_selectedrowIndex, _selectedcolumnIndex].GetComponent<Image>());
        }
    }
    private void OnSelectedChanged()
    {
        for (var i = 0; i < _cells.GetLength(0); i++)
        {
            for (var j = 0; j < _cells.GetLength(1); j++)
            {
                var cell = _cells[i, j];
                if (!cell) { continue; } // Destory 済なら無視
                if (!cell.GetComponent<Image>()) { continue; }

                var image = cell.GetComponent<Image>();

                if (i == _selectedrowIndex && j == _selectedcolumnIndex)
                {
                    image.color = Color.red;
                }
                else { image.color = Color.white; }
            }
        }
    }
}
