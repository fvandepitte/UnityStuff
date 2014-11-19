using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MyCharacterController : MonoBehaviour {

    public float Speed = 1f;

    internal event EventHandler MovementChanged;

    private Animator _anim;
    private IMovementState _movemenstate = new IdleState();
    private Vector3 _target;
    private Stack<Vector3> _targetPath;


    private IMovementState Movementstate {
        get { return _movemenstate; }
        set 
        {
            if (_movemenstate.GetType() != value.GetType())
            {
                _movemenstate = value;
                if (MovementChanged != null)
                {
                    MovementChanged(this, null);
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        _anim = this.GetComponent<Animator>();
        this.MovementChanged += CharacterController_MovementChanged;
        
	}

    void CharacterController_MovementChanged(object sender, EventArgs e) {
        _movemenstate.SetMovement(_anim);
    }
	
	// Update is called once per frame
	void Update () {
        


        if (_target != null)
        {
             


             float step = Speed * Time.deltaTime;
             transform.position = Vector3.MoveTowards(transform.position, _target, step);

             if (_target.IsRightOf(transform.position))
             {
                 Movementstate = new MoveRightState();
             }
             else if (_target.IsLeftOf(transform.position))
             {
                 Movementstate = new MoveLeftState();
             }
             else if (_target.IsAbove(transform.position))
             {
                 Movementstate = new MoveUpState();
             }
             else if (_target.IsBeneath(transform.position))
             {
                 Movementstate = new MoveDownState();
             }
             else
             {
                 if (_targetPath != null && _targetPath.Count > 0)
                 {
                     Vector3 target = _targetPath.Pop();
                     _target = new Vector3(target.x, target.y, -0.1f);
                 }
                 else
                 {
                     Movementstate = new IdleState();
                 }

             }
        }

        
	}

    public void MoveInstant() {
        transform.position = _target;
    }

    public void SetTarget(Vector3 target) {
        _target = new Vector3(target.x, target.y, -0.1f);
    }

    public void SetTargetPath(Stack<Vector3> targetPath) 
    {
        _targetPath = targetPath;
       
    }
}
