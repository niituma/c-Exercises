using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LifeCellState
{
    Dead = 0,
    Live = 1
}

public class LifeCell : MonoBehaviour
{
    [SerializeField]
    LifeCellState _state = LifeCellState.Dead;
    public LifeCellState State
    {
        get => _state;
        set
        {
            _state = value;
            ChangeColor();
        }
    }
    private void OnValidate()
    {
        ChangeColor();
    }
    void ChangeColor()
    {
        GetComponent<Image>().color = _state == LifeCellState.Live ? Color.black : Color.white;
    }
}
