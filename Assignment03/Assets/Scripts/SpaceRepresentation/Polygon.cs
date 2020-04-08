using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
	private Vector2[] points;
    public Vector2[] Points { get { return points; } }
	private float lineWidth = 0.02f;
	private Color lineColor = new Color(0.129f, 0.129f, 1.0f);
	public Material lineMaterial;

    public void SetPoints(Vector2[] _points)
    {
        this.points = _points;
    }

	// Given three colinear points p, q, r, the function checks if 
	// point q lies on line segment 'pr' 
	bool onSegment(Vector2 p, Vector2 q, Vector2 r)
	{
		if (q[0] <= Mathf.Max(p[0], r[0]) && q[0] >= Mathf.Min(p[0], r[0]) &&
			q.y <= Mathf.Max(p[1], r[1]) && q[1] >= Mathf.Min(p[1], r[1])) {
			return true;
		}

		return false;
	}

	// To find orientation of ordered triplet (p, q, r). 
	// The function returns following values 
	// 0 --> p, q and r are colinear 
	// 1 --> Clockwise 
	// 2 --> Counterclockwise 
	int orientation(Vector2 p, Vector2 q, Vector2 r)
	{
		// See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
		// for details of below formula. 
		float val = (q[1] - p[1]) * (r[0] - q[0]) -
				(q[0] - p[0]) * (r[1] - q[1]);

		if (val == 0) return 0; // colinear 

		return (val > 0) ? 1 : 2; // clock or counterclock wise 
	}

	// The main function that returns true if line segment 'p1q1' 
	// and 'p2q2' intersect. 
	private bool doIntersect(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
	{
		// Find the four orientations needed for general and 
		// special cases 
		int o1 = orientation(p1, q1, p2);
		int o2 = orientation(p1, q1, q2);
		int o3 = orientation(p2, q2, p1);
		int o4 = orientation(p2, q2, q1);

		// General case 
		if (o1 != o2 && o3 != o4)
			return true;

		// Special Cases 
		// p1, q1 and p2 are colinear and p2 lies on segment p1q1 
		if (o1 == 0 && onSegment(p1, p2, q1)) return true;

		// p1, q1 and q2 are colinear and q2 lies on segment p1q1 
		if (o2 == 0 && onSegment(p1, q2, q1)) return true;

		// p2, q2 and p1 are colinear and p1 lies on segment p2q2 
		if (o3 == 0 && onSegment(p2, p1, q2)) return true;

		// p2, q2 and q1 are colinear and q1 lies on segment p2q2 
		if (o4 == 0 && onSegment(p2, q1, q2)) return true;

		return false; // Doesn't fall in any of the above cases 
	}

	public Vector2[][] GetLines()
	{
		Vector2[][] lines = new Vector2[points.Length][];
		for (int i = 0; i < points.Length; i++)
		{
			Vector2[] line = new Vector2[2];
			if (i < points.Length - 1)
			{
				line[0] = points[i];
				line[1] = points[i + 1];
			}
			else
			{
				line[0] = points[i];
				line[1] = points[0];
			}
			lines[i] = line;
		}
		return lines;
	}

	public bool AnyIntersect(Vector2 pt1, Vector2 pt2)
	{
		Vector2[][] lines = GetLines();
		foreach (Vector2[] line in lines)
		{
			if (doIntersect(pt1, pt2, line[0], line[1]))
			{
				return true;
			}
		}
		return false;

	}

	public bool ContainsPoint(Vector2 pnt)
	{
		Vector2[][] lines = GetLines();
		int numIntersections = 0;
		foreach (Vector2[] line in lines)
		{
			if (doIntersect(new Vector2(-4.2f, -20.1f), pnt, line[0], line[1]))
			{
				numIntersections += 1;
			}
		}

		return numIntersections % 2 == 1;
	}


	public void RenderObstacle(){		
		for (int i = 0; i < points.Length; i++){
			GameObject obj = new GameObject();
			LineRenderer line = obj.AddComponent<LineRenderer>();
			Vector2 point1, point2;
			if (i < points.Length - 1){
				point1 = points[i];
				point2 = points[i + 1];
			}
			else{
				point1 = points[i];
				point2 = points[0];
			}

			line.SetPosition(0, point1);
			line.SetPosition(1, point2);
			line.startWidth = lineWidth;
			line.endWidth = lineWidth;
			line.material = lineMaterial;
			obj.name = "" + i;
			line.startColor = lineColor;
			line.endColor = lineColor;
			obj.transform.parent = transform;

			PolygonCollider2D collider = obj.AddComponent<PolygonCollider2D>();

			Vector2 perpindicularSlope1 = new Vector2(point2[0] - point1[0], point1[1]-point2[1]);
			Vector2 perpindicularSlope2 = new Vector2(point1[0] - point2[0], point2[1] - point1[1]);
			perpindicularSlope1.Normalize();
			perpindicularSlope2.Normalize();

			collider.points = new Vector2[4];
			collider.points[0] = new Vector2(point1[0] + perpindicularSlope1[0] * lineWidth / 2f, point1[1] + perpindicularSlope1[1] * lineWidth / 2f);
			collider.points[1] = new Vector2(point1[0] + perpindicularSlope2[0] * lineWidth / 2f, point1[1] + perpindicularSlope2[1] * lineWidth / 2f);
			collider.points[2] = new Vector2(point2[0] + perpindicularSlope2[0] * lineWidth / 2f, point2[1] + perpindicularSlope2[1] * lineWidth / 2f);
			collider.points[3] = new Vector2(point2[0] + perpindicularSlope1[0] * lineWidth / 2f, point2[1] + perpindicularSlope1[1] * lineWidth / 2f);

		}

    }
}
