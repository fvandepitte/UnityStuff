using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
    public float Speed = 1f;
    private float _z;

    private GameObject _player;

    void Start() {
        _z = transform.position.z;
    }

	// Update is called once per frame
	void Update () {
        if (_player != null)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y, _z), step);
            
        }
	}

    public void SetPlayer(GameObject player) {
        _player = player;
    }
}
