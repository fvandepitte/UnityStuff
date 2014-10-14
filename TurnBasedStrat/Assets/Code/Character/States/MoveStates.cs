using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MoveUpState : IMovementState
{
    public void SetMovement(UnityEngine.Animator anim) {
        anim.SetBool("WalkingDown", false);
        anim.SetBool("WalkingRight", false);
        anim.SetBool("WalkingLeft", false);
        anim.SetBool("WalkingUp", true);
    }
}

public class MoveDownState : IMovementState
{
    public void SetMovement(UnityEngine.Animator anim) {
        anim.SetBool("WalkingDown", true);
        anim.SetBool("WalkingRight", false);
        anim.SetBool("WalkingLeft", false);
        anim.SetBool("WalkingUp", false);
    }
}

public class MoveLeftState : IMovementState
{
    public void SetMovement(UnityEngine.Animator anim) {
        anim.SetBool("WalkingDown", false);
        anim.SetBool("WalkingRight", false);
        anim.SetBool("WalkingLeft", true);
        anim.SetBool("WalkingUp", false);
    }
}

public class MoveRightState : IMovementState
{
    public void SetMovement(UnityEngine.Animator anim) {
        anim.SetBool("WalkingDown", false);
        anim.SetBool("WalkingRight", true);
        anim.SetBool("WalkingLeft", false);
        anim.SetBool("WalkingUp", false);
    }
}