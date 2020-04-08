using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleHandler : MonoBehaviour
{
	public static ObstacleHandler Instance;
	public GameObject protoObstacle;
	private List<Polygon> obstacles = new List<Polygon>();
	public Polygon[] Obstacles{ get { return obstacles.ToArray(); } }
	private ObstacleDefiner obstacleDefiner;
    
	public float Width { get { return obstacleDefiner.width; } }
	public float Height { get { return obstacleDefiner.height; } }

	public void Init(ObstacleDefiner _obstacleDefiner)
	{
		ObstacleHandler.Instance = this;
		obstacleDefiner = _obstacleDefiner;
	}

	private Polygon CreateObstacle(Vector2[] points, string obstacleName = "")
	{
		GameObject newObstacleObj = GameObject.Instantiate(protoObstacle);
		if (obstacleName.Length > 0)
		{
			newObstacleObj.name = obstacleName;
		}
        Polygon obstacle = newObstacleObj.GetComponent<Polygon>();
		obstacle.SetPoints(points);

		obstacles.Add(obstacle);

		return obstacle;
	}

	private void CreateAndRenderObstacle(Vector2[] points, string obstacleName = "")
	{
        Polygon newObstacle = CreateObstacle(points, obstacleName);
		newObstacle.RenderObstacle();
	}

	public void CreateAndRenderObstacles(Vector2[][] arrayOfPoints)
	{
		for(int o=0; o<arrayOfPoints.Length; o++)
		{
			Vector2[] points = arrayOfPoints[o];
			CreateAndRenderObstacle(points, "Obstacle " + o);
		}
	}

    //Do any polygons intersect between these two  points?
	public bool AnyIntersect(Vector2 pt1, Vector2 pt2) {
		foreach (Polygon o in obstacles)
		{
			if (o.AnyIntersect(pt1, pt2)) {
				return true;
			}
		}
		return false;
	}

    //Purely so one doesn't have to convert vector3 to vector2
    public bool AnyIntersect(Vector3 pt1, Vector3 pt2)
    {
        return AnyIntersect(new Vector2(pt1.x, pt1.y), new Vector2(pt2.x, pt2.y));
    }

    //Returns true if this point is in any obstacle
    public bool PointInObstacles(Vector2 pnt)
	{
		foreach (Polygon obst in obstacles)
		{
			if (obst.ContainsPoint(pnt))
			{
				return true;
			}
		}
		return false;
	}

    //Returns the corners of the map
    public Vector2[] GetMapCorners()
    {
        return new Vector2[] { new Vector2(ObstacleHandler.Instance.Width * -1, ObstacleHandler.Instance.Height * -1),
            new Vector2(ObstacleHandler.Instance.Width * -1, ObstacleHandler.Instance.Height),
        new Vector2(ObstacleHandler.Instance.Width, ObstacleHandler.Instance.Height),
        new Vector2(ObstacleHandler.Instance.Width, ObstacleHandler.Instance.Height * -1)};
    }

    //Returns an array of obstacle points
    public Vector2[] GetObstaclePoints()
    {
        List<Vector2> pointsList = new List<Vector2>();

        foreach(Polygon o in obstacles)
        {
            foreach(Vector2 p in o.Points)
            {
                pointsList.Add(p);
            }
        }

        return pointsList.ToArray();
    }

}
