using UnityEngine;
using System.Collections;

public class InputListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5f;

            Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

            Collider2D[] col = Physics2D.OverlapPointAll(v);

            if (col.Length > 0)
            {
                try
                {
                    gameObject.GetComponent<FieldController>().SetTarget(col[0].collider2D.gameObject);
                }
                catch { }
            }
        }
	}
}
