using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoObstacle
{
    //This obstacle's position
    public Vector3 worldLocation;
    //This obstacle shape, specified in local coordinates to the worldLocation
    public Vector2[] shape;//Note:  NO SELF-INTERSECTING LINES
    public Color lineColor = new Color(0.129f, 0.129f, 1.0f);//Colour for this polygon. Note: You do not need to edit this for the base assignment.
    public float lineWidth = 0.02f;//The width of the lines for this polygon. Note: You do not need to edit this for the base assignment.

    public ProtoObstacle(Vector3 _worldLocation, Vector2[] _shape)
    {
        worldLocation = _worldLocation;
        shape = _shape;
    }

    public ProtoObstacle(Vector3 _worldLocation, Vector2[] _shape, Color _lineColor)
    {
        worldLocation = _worldLocation;
        shape = _shape;
        lineColor = _lineColor;
    }

    public ProtoObstacle(Vector3 _worldLocation, Vector2[] _shape, float _lineWidth)
    {
        worldLocation = _worldLocation;
        shape = _shape;
        lineWidth = _lineWidth;
    }

    public ProtoObstacle(Vector3 _worldLocation, Vector2[] _shape, Color _lineColor, float _lineWidth)
    {
        worldLocation = _worldLocation;
        shape = _shape;
        lineColor = _lineColor;
        lineWidth = _lineWidth;
    }

    //Clones this object
    public ProtoObstacle Clone() {
        return new ProtoObstacle(worldLocation, shape, lineColor, lineWidth);
    }

    //Returns the points of this shape in their world coordinates
    public Vector2[] GetPoints()
    {
        List<Vector2> pointsInWorld = new List<Vector2>();
        foreach(Vector2 pnt in shape)
        {
            pointsInWorld.Add(new Vector2(worldLocation.x + pnt.x, worldLocation.y + pnt.y));
        }

        return pointsInWorld.ToArray();
    }

}
