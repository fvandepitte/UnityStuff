using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class AssetLoader
{
    private const string SPRITELOCATION = "Sprites/Terrain/";
    private System.Random RND = new System.Random();

    private JSONNode _root;
    private Dictionary<TileType, Sprite[]> _spritesMapping;

    public AssetLoader() {
        _root = JSON.Parse(Resources.Load<TextAsset>(string.Format("{0}{1}", SPRITELOCATION, "TileMapping")).text);
        _spritesMapping = new Dictionary<TileType, Sprite[]>();
    }

    public Sprite Load(TileType tileType) {
        if (!_spritesMapping.ContainsKey(tileType))
        {
            JSONArray arr = _root[tileType.ToString()].AsArray;
            _spritesMapping[tileType] = new Sprite[arr.Count];
            int i = 0;
            foreach (JSONNode item in arr)
            {
                _spritesMapping[tileType][i++] = Resources.Load<Sprite>(string.Format("{0}{1}", SPRITELOCATION, item.Value));
            }
            //_spritesMapping[tileType] = Resources.Load<Sprite>(string.Format("{0}{1}", SPRITELOCATION, _root[tileType.ToString()]));
        }

        return _spritesMapping[tileType][RND.Next(_spritesMapping[tileType].Length)];
    }
}
