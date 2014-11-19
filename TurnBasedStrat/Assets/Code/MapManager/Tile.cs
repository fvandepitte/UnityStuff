using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

public class Tile
{
    public Tile(int row, int column, TileType type, Transform root) {

        Row = row;
        Column = column;

        Sprite s = Engine.Instance.AssetLoader.Load(type);
        Representation = new GameObject(string.Format("Tile{0}-{1}", row, column));
        Representation.transform.parent = root;
        Representation.transform.position = new Vector3(s.bounds.size.x * column, s.bounds.size.y * row);
        var renderer = Representation.AddComponent<SpriteRenderer>();
        renderer.sprite = s;
        Representation.AddComponent<BoxCollider2D>();

        Walkable = type == TileType.Grass;

    }

    public int Row { get; private set; }
    public int Column { get; private set; }

    public bool Walkable { get; private set; }

    public IEnumerable<Tile> Neighbours { 
        get 
        {
            //Upper Row
            Map map = Engine.Instance.Map;
            bool firstColumn = Column == 0, lastColumn = map.Columns == Column + 1;

            if (Row > 0)
            {
                if (!firstColumn)
                {
                    yield return map[Row - 1, Column - 1];
                }

                yield return map[Row - 1, Column];

                if (!lastColumn)
                {
                    yield return map[Row - 1, Column + 1];
                }
            }

            

            //Same Row

            if (!firstColumn)
            {
                yield return map[Row, Column - 1];
            }

            if (!lastColumn)
            {
                yield return map[Row, Column + 1];
            }

            //Lower Row
            if (map.Rows > Row + 1)
            {
                if (!firstColumn)
                {
                    yield return map[Row + 1, Column - 1];
                }

                yield return map[Row + 1, Column];

                if (!lastColumn)
                {
                    yield return map[Row + 1, Column + 1];
                }
            }
            
        } 
    }

    public GameObject Representation { get; private set; }

    public static bool operator ==(Tile a, Tile b) {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.Row == b.Row && a.Column == b.Column;
    }

    public static bool operator !=(Tile a, Tile b) {
        return !(a == b);
    }

    public override bool Equals(object obj) {
        if (typeof(Tile) == obj.GetType())
        {
            return this == (obj as Tile);
        }
        return base.Equals(obj);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
