using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VecUtils
{

    public static Vector2 SX(this Vector2 v, float x)
    {
        v.x = x;
        return v;
    }
    public static Vector2 SY(this Vector2 v, float y)
    {
        v.y = y;
        return v;
    }

    public static Vector3 SX(this Vector3 v, float x)
    {
        v.x = x;
        return v;
    }
    public static Vector3 SY(this Vector3 v, float y)
    {
        v.y = y;
        return v;
    }
    public static Vector3 SZ(this Vector3 v, float z)
    {
        v.z = z;
        return v;
    }
}
