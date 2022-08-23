using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class positionData 
{
    private const double degToRad = Math.PI / 180.0;
    private const double radToDeg = 180.0 / Math.PI;

    public static position kep2Cart(kepler elements, double time)
    { 
        double inclination = elements.incl * degToRad;
        double argOfPerigee = elements.w * degToRad;
        double longOfAscNode = elements.RAAN * degToRad;
        double semiMajorAxis = elements.semiMajorAxis;
        double eccentricity = elements.eccentricity;
        double startingMeanAnom = elements.startingMeanAnom;
        double startingEpoch = elements.startingEpoch;
        double mu = elements.mu;

        double meanAnom = startingMeanAnom;

        if (time == startingEpoch) meanAnom = startingMeanAnom;
        else meanAnom = startingMeanAnom + 86400.0 * (time - startingEpoch) * Math.Sqrt((mu / Math.Pow(semiMajorAxis, 3)));

        double EA = meanAnom;
        for (int i = 0; i < 50; i++) EA = meanAnom + eccentricity * Math.Sin(EA);

        double trueAnom1 = Math.Sqrt(1 - eccentricity * eccentricity) * (Math.Sin(EA) / (1 - eccentricity * Math.Cos(EA)));
        double trueAnom2 = (Math.Cos(EA) - eccentricity) / (1 - eccentricity * Math.Cos(EA));

        double trueAnom = Math.Atan2(trueAnom1, trueAnom2);

        double theta = trueAnom + argOfPerigee;

        double radius = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Math.Cos(trueAnom));

        double xp = radius * Math.Cos(theta);
        double yp = radius * Math.Sin(theta);

        position pos = new position(
        xp * Math.Cos(longOfAscNode) - yp * Math.Cos(inclination) * Math.Sin(longOfAscNode),
        xp * Math.Sin(longOfAscNode) - yp * Math.Cos(inclination) * Math.Cos(longOfAscNode),
        yp * Math.Sin(inclination));

        return pos;
    }
}

public readonly struct kepler
{
    public readonly double semiMajorAxis, eccentricity, incl, w, RAAN, startingMeanAnom, startingEpoch, mu;

    public kepler(double semiMajorAxis, double eccentricity, double incl, double w, double RAAN, double startingMeanAnom, double startingEpoch, double mu)
    {
        this.semiMajorAxis = semiMajorAxis;
        this.eccentricity = eccentricity;
        this.incl = incl;
        this.w = w;
        this.RAAN = RAAN;
        this.startingMeanAnom = startingMeanAnom;
        this.startingEpoch = startingEpoch;
        this.mu = mu;
    }
}