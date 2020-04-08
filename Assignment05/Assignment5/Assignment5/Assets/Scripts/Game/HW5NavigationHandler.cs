using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Exact copy of the NavigationHandler. Implement Smoothing here
public class HW5NavigationHandler : MonoBehaviour
{
    public static HW5NavigationHandler Instance;

    [SerializeField]
    private ObstacleHandler obstacleHandler;
    [SerializeField]
    private PathFinder pathFinder;
    [SerializeField]
    private NodeHandler nodeHandler;

    [SerializeField]
    private float timeScale = 1f;//Impacts how fast the game will be played

    public ObstacleHandler ObstacleHandler { get { return obstacleHandler; } }
    public PathFinder PathFinder { get { return pathFinder; } }
    public NodeHandler NodeHandler { get { return nodeHandler; } }

    void Awake()
    {
        Instance = this;
        Time.timeScale = timeScale;
    }

    // Start is called before the first frame update
    public void Init(Level level)
    {
        obstacleHandler.Init();
        obstacleHandler.CreateAndRenderObstacles(level.obstacles.ToArray());

        nodeHandler = new GridHandler();
        nodeHandler.CreateNodes();
    }

    void Update()
    {
        nodeHandler.VisualizeNodes();
        
    }

}
