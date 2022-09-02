using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] private int _count = 5;
    int _index = 0;
    int _start = 0;
    int _end = 0;
    [SerializeField] GameObject[] _cell = new GameObject[50];
    private void Start()
    {
        _end = _count - 1;
        for (var i = 0; i < _count; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            _cell[i] = obj;
            var image = obj.AddComponent<Image>();

            if (i == 0) { image.color = Color.red; }
            else { image.color = Color.white; }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            if (_cell[_index % _count])
            {
                _cell[_index % _count].GetComponent<Image>().color = Color.white;
                if (transform.childCount == 1)
                {
                    transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            for (var i = 0; i < _count; i++)
            {
                if (_index > _start)
                {
                    _index--;
                }
                if (_cell[_index % _count])
                {
                    _cell[_index % _count].GetComponent<Image>().color = Color.red;
                    break;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            if (_cell[_index % _count])
            {
                _cell[_index % _count].GetComponent<Image>().color = Color.white;
                if (transform.childCount == 1)
                {
                    transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            for (var i = 0; i < _count; i++)
            {
                if (_index < _end)
                {
                    _index++;
                }
                if (_cell[_index % _count])
                {
                    _cell[_index % _count].GetComponent<Image>().color = Color.red;
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_cell[_index % _count]);
            var index = _index;
            if (_index % _count == _start)
            {
                for (int i = _start; i <= _end; ++i)
                {
                    _start++;
                    if (_cell[_start])
                    {
                        break;
                    }
                }
            }
            else if (_index % _count == _end)
            {
                for (int i = _end; i >= _start; --i)
                {
                    _end--;
                    if (_cell[_end])
                    {
                        break;
                    }
                }
            }
        }
    }
}
