using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler: MonoBehaviour
{
    public PelletHandler pelletHandler;
    public Evaluator evaluator;
    public float goalValue = 1f;

    void Start()
    {
        LevelOptimizer levelOptimizer = new LevelOptimizer();

        //Run your GA and get the best level for this specific evaluator and the goalValue
        Level level = levelOptimizer.GetLevel(evaluator, goalValue);

        //Set in case this level changed where the ghosts spawn
        AgentConstants.GHOST_START_POS = level.ghostStartPos;

        //Set in case this level changed where Pacman spawns
        PacmanInfo pacmanInfo = GameObject.FindObjectOfType<PacmanInfo>();
        if (pacmanInfo != null)
        {
            pacmanInfo.transform.position = level.pacmanStartPos;
        }

        //Initialize obstacles and grid based on level
        HW5NavigationHandler.Instance.Init(level);

        //Initialize pellets based on level
        foreach(ProtoPellet p in level.pellets)
        {
            pelletHandler.AddPellet(p.worldLocation, p.powerPellet);
        }

    }
}
