using System;
using System.Text;
using UnityEngine;

public class Tile
{
    public Tile(int row, int column, TileType type, Transform root) {
        
        Sprite s = Engine.Instance.AssetLoader.Load(type);
        Representation = new GameObject(string.Format("{0}-{1}", row, column));
        Representation.transform.parent = root;
        Representation.transform.position = new Vector3(s.bounds.size.x * column, s.bounds.size.y * row);
        var renderer = Representation.AddComponent<SpriteRenderer>();
        renderer.sprite = s;
        Representation.AddComponent<BoxCollider2D>();
    }


    public GameObject Representation { get; private set; }
}
