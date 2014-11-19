using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Engine
{
    private static Engine _instance = new Engine();

    public static Engine Instance {
        get { return _instance; }
        set { _instance = value; }
    }

    private Engine() {

    }

    private AssetLoader _assetLoader;
    public Map Map { get; private set; }

    public void BootStrap(int rows, int columns, UnityEngine.Transform root = null) {
        _assetLoader = new AssetLoader();
        Map = new Map(rows, columns, root);
    }

    public void LoadBlue(UnityEngine.GameObject blue) {
        Map.PlacePlayer(blue);
    }
    

    public AssetLoader AssetLoader { get { return _assetLoader; } }
}
