using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public struct Coordinate
{
    public int x;
    public int y;

    public Coordinate (int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static List<List<Coordinate>> GetPatternDirections(List<Coordinate> relativeCoordinates)
    {
        List<List<Coordinate>> paths = new List<List<Coordinate>>();

        paths.Add(relativeCoordinates);
        paths.Add(RotateRelativeCoordinates(relativeCoordinates));
        paths.Add(FlipRelativeCoordinates(paths[0]));
        paths.Add(RotateRelativeCoordinates(paths[2]));

        return paths;
    }

    public static List<Coordinate> RotateRelativeCoordinates(List<Coordinate> relativeCoordinates)
    {
        List<Coordinate> newCoordinates = new List<Coordinate>();

        foreach(var coordinate in relativeCoordinates)
        {
            newCoordinates.Add(new Coordinate(coordinate.y, coordinate.x));
        }

        return newCoordinates;
    }

    public static List<Coordinate> FlipRelativeCoordinates(List<Coordinate> relativeCoordinates)
    {
        List<Coordinate> newCoordinates = new List<Coordinate>();

        foreach (var coordinate in relativeCoordinates)
        {
            newCoordinates.Add(new Coordinate(coordinate.x, -coordinate.y));
        }

        return newCoordinates;
    }
}
