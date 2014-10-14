using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Extensions
{
    public static bool IsLeftOf(this Vector3 a, Vector3 target) {
        return a.x < target.x;
    }

    public static bool IsRightOf(this Vector3 a, Vector3 target) {
        return a.x > target.x;
    }

    public static bool IsAbove(this Vector3 a, Vector3 target) {
        return a.y > target.y;
    }

    public static bool IsBeneath(this Vector3 a, Vector3 target) {
        return a.y < target.y;
    }
}
