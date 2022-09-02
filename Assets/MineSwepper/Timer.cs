using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float _second { get; private set; } = 0;
    public int _minute { get; private set; } = 0;
    [SerializeField] Text _text;

    // Update is called once per frame
    void Update()
    {
        _second += Time.deltaTime;
        if (_second >= 60f)
        {
            _minute++;
            _second = 0;
        }

        _text.text = _minute.ToString("D2") + ":" + ((int)_second).ToString("D2");
    }
}
