using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class AStar : MonoBehaviour
{
    const int _rows = 6;

    const int _cloumns = 6;

    public int _moveCount { get; private set; } = 0;

    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    MapTile _cellPrefab = null;

    MapTile[,] _tiles;

    [SerializeField]
    List<MapTile> _openMapTiles = new List<MapTile>();

    int _goalRow, _goalCloumn;

    int _startrow, _startcloumns;

    // Start is called before the first frame update
    void Start()
    {
        _gridLayoutGroup.constraint
            = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _cloumns;

        MapCreate();
        OpenTile(_startrow, _startcloumns);

        var waytile = _openMapTiles.Where(t => t.TileState == TileState.Close).Last();
        waytile.GetComponent<Image>().color = Color.yellow;
        while (true)
        {
            if (waytile.OpenSorce.TileState == TileState.Start) { break; }

            waytile = waytile.OpenSorce;
            waytile.GetComponent<Image>().color = Color.yellow;
        }

    }

    private void MapCreate()
    {
        _tiles = new MapTile[_rows, _cloumns];
        var _tileStates = new int[_rows, _cloumns]
        {
            {2,2,2,3,2,2},
            {2,0,2,3,2,1},
            {2,2,2,3,2,2},
            {2,3,2,3,2,2},
            {2,3,2,3,2,2},
            {2,2,2,2,2,2},
        };
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                _tiles[r, c] = cell;
                _tiles[r, c].TileState = (TileState)_tileStates[r, c];
                if (_tiles[r, c].TileState == TileState.Goal)
                {
                    (_goalRow, _goalCloumn) = (r, c);
                }
                else if (_tiles[r, c].TileState == TileState.Start)
                {
                    (_startrow, _startcloumns) = (r, c);
                }
            }
        }
    }
    MapTile[] GetNeighbourTiles(int r, int c)
    {
        var tiles = new List<MapTile>();
        if (r + 1 < _rows) tiles.Add(_tiles[r + 1, c]);
        if (r - 1 >= 0) tiles.Add(_tiles[r - 1, c]);
        if (c + 1 < _cloumns) tiles.Add(_tiles[r, c + 1]);
        if (c - 1 >= 0) tiles.Add(_tiles[r, c - 1]);
        return tiles.ToArray();
    }

    int GoalDis(int r, int c)
    {
        var dis = Mathf.Abs(r - _goalRow) + Mathf.Abs(c - _goalCloumn);
        return dis;
    }

    (int row, int cloumn)? SarchTile(MapTile tile)
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cloumns; c++)
            {
                if (tile == _tiles[r, c])
                {
                    return (r, c);
                }
            }
        }
        return null;
    }

    void OpenTile(int r, int c)
    {
        _moveCount++;
        foreach (var tile in GetNeighbourTiles(r, c))
        {
            if (tile.TileState == TileState.Goal)
            {
                _tiles[r, c].TileState = TileState.Close;
                return;
            }
            else if (tile.TileState == TileState.None)
            {
                if (SarchTile(tile) is { } a)
                    tile.Open(GoalDis(a.row, a.cloumn), _moveCount, _tiles[r, c]);
                _openMapTiles.Add(tile);
            }
        }
        if (_tiles[r, c].TileState != TileState.Start)
        {
            _tiles[r, c].TileState = TileState.Close;
        }
        var nexttile = _openMapTiles.Where(t => t.TileState == TileState.Open).OrderBy(t => t.Score).FirstOrDefault();
        _openMapTiles.Add(nexttile);
        if (SarchTile(nexttile) is { } t)
            OpenTile(t.row, t.cloumn);
    }
}

