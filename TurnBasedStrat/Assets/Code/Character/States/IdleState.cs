using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IdleState :IMovementState
{
    public void SetMovement(UnityEngine.Animator anim) {
        anim.SetBool("WalkingDown", false);
        anim.SetBool("WalkingRight", false);
        anim.SetBool("WalkingLeft", false);
        anim.SetBool("WalkingUp", false);
    }
}