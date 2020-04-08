using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PelletHandler pelletHandler;

    void Start()
    {
        HW3NavigationHandler.Instance.Init();

        List<GraphNode> corners = new List<GraphNode>();

        Vector3[] cornerVecs = new Vector3[] { new Vector3(ObstacleHandler.Instance.Width, ObstacleHandler.Instance.Height), new Vector3(-1*ObstacleHandler.Instance.Width, ObstacleHandler.Instance.Height), new Vector3(ObstacleHandler.Instance.Width, -1*ObstacleHandler.Instance.Height), new Vector3(-1*ObstacleHandler.Instance.Width, -1*ObstacleHandler.Instance.Height) };

        foreach(Vector3 pos in cornerVecs)
        {
            GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(pos);
            corners.Add(g);
        }

        for (float x = ObstacleHandler.Instance.Width * -1; x <= ObstacleHandler.Instance.Width + 0.2f; x += 0.1f)
        {
            for (float y = ObstacleHandler.Instance.Height * -1; y <= ObstacleHandler.Instance.Height; y += 0.1f)
            {
                GraphNode g = HW3NavigationHandler.Instance.NodeHandler.ClosestNode(new Vector3(x, y));
                if (corners.Contains(g))
                {
                    pelletHandler.AddPellet(g.Location, true);
                }
                else
                {
                    pelletHandler.AddPellet(g.Location);
                }
                

            }
        }
    }
}
