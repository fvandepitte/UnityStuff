using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class FieldController : MonoBehaviour {
    public int Rows = 10, Columns = 10;
    public GameObject Blue;

    private GameObject _blue;

	// Use this for initialization
	void Start () {
        Engine.Instance.BootStrap(Rows, Columns, this.GetComponent<Transform>());
        _blue = Instantiate(Blue) as GameObject;
        Engine.Instance.LoadBlue(_blue);
        _blue.SendMessage("MoveInstant");
        _blue.AddComponent<InputListener>();
        Camera.main.transform.position = new Vector3(_blue.transform.position.x, _blue.transform.position.y, Camera.main.transform.position.z);
        Camera.main.SendMessage("SetPlayer", _blue);

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTarget(GameObject target) {
        Tile targetTile = Engine.Instance.Map[target];
        Tile origin =  Engine.Instance.Map[Physics2D.OverlapPointAll(_blue.transform.position).First(col => col.gameObject.name.StartsWith("Tile")).gameObject];

        Stack<Vector3> points = gameObject.GetComponent<PathController>().CalculatePath(targetTile, origin);

        _blue.GetComponent<MyCharacterController>().SetTargetPath(points);
    }
}
