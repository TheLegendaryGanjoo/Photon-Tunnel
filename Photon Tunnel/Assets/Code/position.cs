using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public readonly struct position
{
    public readonly double x, z, y;

    public position(double x, double y, double z)
    {
        this.x = x;
        this.z = z;
        this.y = y;
    }

    public position(position p)
    {
        this.x = p.x;
        this.y = p.y;
        this.z = p.z;
    }
}
