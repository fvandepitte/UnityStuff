using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Map
{
    private Tile[,] _tiles;
    private Dictionary<GameObject, Location> _playerLocation;

    private UnityEngine.Transform _root;

    public Map(int rows, int columns, UnityEngine.Transform root) {
        this.Rows = rows;
        this.Columns = columns;
        this._root = root;
        _tiles = new Tile[rows, columns];
        _playerLocation = new Dictionary<GameObject, Location>();
        GenerateNewRandomMap();

    }

    public void PlacePlayer(GameObject obj) {
        PlacePlayer(obj, UnityEngine.Random.Range(0, Rows), UnityEngine.Random.Range(0, Columns));
    }

    public void PlacePlayer(GameObject obj, Location location) {
        if (location.Row >= Rows)
        {
            throw new IndexOutOfRangeException("Row out of range");
        }
        if (location.Column >= Columns)
        {
            throw new IndexOutOfRangeException("Column out of range");
        }
        _playerLocation[obj] = location;

        obj.SendMessage("SetTarget", _tiles[location.Row, location.Column].Representation.transform.position);
    }


    public void PlacePlayer(GameObject obj, int row, int column) {
        PlacePlayer(obj, new Location { Row = row, Column = column });
    }

    public void GenerateNewRandomMap() {

        List<TileType> tiles = new List<TileType>(Count);
        for (int i = 0; i < Count; i++)
        {
            if (i < (Count) * 1 / 10 )
            {
                tiles.Add(TileType.Water);
            }
            else if (i < (Count) * 4 / 10)
            {
                tiles.Add(TileType.Dirt);
            }
            else
            {
                tiles.Add(TileType.Grass);
            }
        }

        tiles.Shuffle();
        int j = 0;
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                _tiles[row, column] = new Tile(row, column, tiles[j++], _root);
            }
        }
    }

    public int Rows { get; private set; }

    public int Columns { get; private set; }

    public int Count { get { return Rows * Columns; } }

    public Tile this[int row, int column] 
    {
        get 
        {
            return _tiles[row, column];
        }
    }
}
