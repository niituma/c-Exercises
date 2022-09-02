using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TileState
{
    Start = 0,
    Goal = 1,
    None = 2,
    Wall = 3,
    Open = 4,
    Close = 5
}

public class MapTile : MonoBehaviour
{
    [SerializeField]
    TileState _tilestate = TileState.None;


    Color _color;

    public TileState TileState
    {
        get => _tilestate;
        set
        {
            _tilestate = value;
            OnCellStateChanged();
        }
    }
    int _estimatedCost = 0;//壁関係なしでゴールの最短距離
    int _realCost = 0;//今までの移動距離
    public int Score { get; set; }//推定コストと実コストの合計
    public MapTile OpenSorce { get; set; }//自分をオープン状態にしたMapTile

    private void Start()
    {
        _color = GetComponent<Image>().color;
    }
    private void OnValidate()
    {
        OnCellStateChanged();
    }
    void OnCellStateChanged()
    {
        switch (_tilestate)
        {
            case TileState.Start:
                _color = Color.green;
                GetComponent<Image>().color = _color;
                break;
            case TileState.Goal:
                _color = Color.red;
                GetComponent<Image>().color = _color;
                break;
            case TileState.None:
                _color = Color.white;
                GetComponent<Image>().color = _color;
                break;
            case TileState.Wall:
                _color = Color.gray;
                GetComponent<Image>().color = _color;
                break;
            case TileState.Open:
                break;
            case TileState.Close:
                break;
            default:
                break;
        }
    }

    public void Open(int estimatedcost, int realcount, MapTile sorcetile)
    {
        _tilestate = TileState.Open;
        _estimatedCost = estimatedcost;
        _realCost = realcount;
        Score = _estimatedCost + _realCost;
        OpenSorce = sorcetile;
    }
}
