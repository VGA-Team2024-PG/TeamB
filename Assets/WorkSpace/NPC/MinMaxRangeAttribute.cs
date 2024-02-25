using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class MinMaxRangeAttribute : PropertyAttribute
{
    public readonly float min;
    public readonly float max;

    public MinMaxRangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
