﻿using System;
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

        if (_tiles[location.Row, location.Column].Walkable)
        {
            _playerLocation[obj] = location;
            obj.SendMessage("SetTarget", _tiles[location.Row, location.Column].Representation.transform.position);
        }
        else
        {
            PlacePlayer(obj);
        }

    }


    public void PlacePlayer(GameObject obj, int row, int column) {
        PlacePlayer(obj, new Location { Row = row, Column = column });
    }

    internal enum Direction
    {
        Up,
        Left,
        Right,
        Down
    }

    public void GenerateNewRandomMap() {        
        TileType[,] map = new TileType[Rows, Columns];
        {
            Direction d;
            int row = 0, column = 0;
            //create river
            if (UnityEngine.Random.Range(0, 1) == 1)
            {
                row = UnityEngine.Random.Range(0, Rows - 1);
                column = 0;
                d = Direction.Right;
            }
            else
            {
                row = 0;
                column = UnityEngine.Random.Range(0, Columns - 1);
                d = Direction.Up;
            }

            try
            {
                for (int i = 0; i < i+1; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        map[row, column] = TileType.Water;

                        i++;
                        switch (d)
                        {
                            case Direction.Up:
                                row++;
                                break;
                            case Direction.Left:
                                column--;
                                break;
                            case Direction.Right:
                                column++;
                                break;
                            case Direction.Down:
                                row--;
                                break;
                        }
                    }

                    d = SwitchDirection(d);
                }
            }
            catch { }
        }

        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                _tiles[row, column] = new Tile(row, column, map[row, column], _root);
            }
        }
    }

    private static Direction SwitchDirection(Direction d) {
        int random = UnityEngine.Random.Range(0, 2);
        if (random == 0)
        {
            switch (d)
            {
                case Direction.Up:
                    d = Direction.Left;
                    break;
                case Direction.Left:
                    d = Direction.Down;
                    break;
                case Direction.Right:
                    d = Direction.Up;
                    break;
                case Direction.Down:
                    d = Direction.Right;
                    break;
            }
        }
        else if (random == 1)
        {
            switch (d)
            {
                case Direction.Up:
                    d = Direction.Right;
                    break;
                case Direction.Left:
                    d = Direction.Up;
                    break;
                case Direction.Right:
                    d = Direction.Down;
                    break;
                case Direction.Down:
                    d = Direction.Left;
                    break;
            }
        }
        return d;
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

    public Tile this[GameObject representation] {
        get {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (_tiles[row, column].Representation == representation) {
                        return _tiles[row, column];
                    }
                }
            }

            return null;
        }
    }
}
