using UnityEngine;
using System;

    public static class TransformHelper
    {
        public static void Move(this Transform transform, Vector3 vector3)
        {
            transform.localPosition = vector3;
        }

        public static void Scale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
        }

        public static void Scale(this Transform transform, float x = -1000, float y = -1000, float z = -1000)
        {
            transform.localScale = new Vector3(
                x == -1000 ? transform.localScale.x : x,
                y == -1000 ? transform.localScale.y : y,
                z == -1000 ? transform.localScale.z : z);
        }

        public static Vector3 Size(this Transform transform)
        {
            return transform.GetComponent<MeshFilter>().mesh.bounds.size;
        }
    }

    public static class ScriptHelper
    {
        public static T GetScript<T>(this Transform transform)
        {
            return (T)transform.GetComponent<T>();
        }
    }