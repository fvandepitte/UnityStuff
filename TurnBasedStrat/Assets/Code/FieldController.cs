using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour {
    public int Rows = 10, Columns = 10;
    public GameObject Blue;
    public Camera MainCamera;

    private GameObject _blue;

	// Use this for initialization
	void Start () {
        Engine.Instance.BootStrap(Rows, Columns, this.GetComponent<Transform>());
        _blue = Instantiate(Blue) as GameObject;
        Engine.Instance.LoadBlue(_blue);
        _blue.SendMessage("MoveInstant");
        _blue.AddComponent<InputListener>();
        MainCamera.transform.position = new Vector3(_blue.transform.position.x, _blue.transform.position.y, MainCamera.transform.position.z);
        MainCamera.SendMessage("SetPlayer", _blue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
