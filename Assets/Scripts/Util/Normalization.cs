using UnityEngine;
using System;

public static class Normalization
{
    
    public static float Sigmoid(float val, float scale = 1f)
    {
        val *= scale;
        return val / (1f + Mathf.Abs(val));
    }

    public static Vector3 Sigmoid(Vector3 v3, float scale = 1f)
    {
        v3.x = Sigmoid(v3.x, scale);
        v3.y = Sigmoid(v3.y, scale);
        v3.z = Sigmoid(v3.z, scale);
        return v3;
    }


    public static float Tanh(float val, float scale = 1f)
    {
        return (float)Math.Tanh(val * scale);
    }

    public static Vector3 Tanh(Vector3 v3, float scale = 1f)
    {
        v3.x = Tanh(v3.x, scale);
        v3.y = Tanh(v3.y, scale);
        v3.z = Tanh(v3.z, scale);
        return v3;
    }


    public static float Log(float val, float e = (float)Math.E)
    {
        return 1f / (1f + Mathf.Pow(e, -val)) * 2f - 1f;
    }

    public static Vector3 Log(Vector3 v3, float e = (float)Math.E)
    {
        v3.x = Log(v3.x, e);
        v3.y = Log(v3.y, e);
        v3.z = Log(v3.z, e);
        return v3;
    }

    public static Vector3 Clamp(Vector3 v)
    {
        v.x = Mathf.Clamp(v.x, -1f, 1f);
        v.y = Mathf.Clamp(v.y, -1f, 1f);
        v.z = Mathf.Clamp(v.z, -1f, 1f);
        return v;
    }

    public static float Lin(float currentValue, float minValue, float maxValue)
    {
        return (currentValue - minValue)/(maxValue - minValue);
    }

    public static Vector3 Lin(Vector3 currentValue, Vector3 minValue, Vector3 maxValue)
    {
        currentValue.x = Lin(currentValue.x, minValue.x, maxValue.x);
        currentValue.y = Lin(currentValue.y, minValue.y, maxValue.x);
        currentValue.z = Lin(currentValue.z, minValue.z, maxValue.x);
        return currentValue;
    }
}
