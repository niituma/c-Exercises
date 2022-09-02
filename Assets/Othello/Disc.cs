using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None = 0,
    White = 1,
    Black = 2,
    CanSelect = 3,
}

public class Disc : MonoBehaviour
{
    [SerializeField]
    GameObject _disc;
    [SerializeField]
    GameObject _board;
    [SerializeField]
    Material _selectMaterial;
    [SerializeField]
    Material _normalMaterial;
    [SerializeField]
    Material _canSetMaterial;
    [SerializeField]
    State _state = State.None;


    public State State
    {
        get => _state;
        set
        {
            _state = value;
            DiscChange();
        }
    }
    private void OnValidate()
    {
        DiscChange();
    }

    public void OnSelected(bool selet)
    {
        _board.GetComponent<Renderer>().material = selet ? _selectMaterial : _state == State.CanSelect ? _canSetMaterial : _normalMaterial;
    }

    void DiscChange()
    {
        if (_state == State.CanSelect)
        {
            _board.GetComponent<Renderer>().material = _canSetMaterial;
            _disc.SetActive(false);
            return;
        }

        _board.GetComponent<Renderer>().material = _normalMaterial;
        if (_state == State.None)
        {
            _disc.SetActive(false);
            return;
        }

        if (!_disc.activeSelf) { _disc.SetActive(true); }
        var angles = _state == State.Black ? 0 : 180;
        _disc.transform.rotation = Quaternion.Euler(angles, 0, 0);
    }
}
