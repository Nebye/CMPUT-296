using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest5Points : ObstacleDefiner
{

	
	public override Vector2[][] GetObstaclePoints()
	{
		Vector2[] obstacles1 = new Vector2[] { new Vector2(-1f, -0.5f), new Vector2(-0.9f, -0.4f), new Vector2(-1f, -0.6f) };
        Vector2[] obstacles2 = new Vector2[] { new Vector2(1.8f, -0.45f), new Vector2(1.9f, -0.4f), new Vector2(1.8f, -0.3f) };
        Vector2[] obstacles3 = new Vector2[] { new Vector2(1.8f, 0.7f), new Vector2(1.9f, 0.5f), new Vector2(1.95f, 0.6f) };
        Vector2[] obstacles4 = new Vector2[] { new Vector2(-1.5f, 0.5f), new Vector2(-1.45f, 0.3f), new Vector2(-1.6f, 0.4f) };
        Vector2[] obstacles5 = new Vector2[] { new Vector2(0, 0.8f), new Vector2(-0.1f, 0.75f), new Vector2(0.1f, 0.8f) };
        Vector2[] obstacles6 = new Vector2[] { new Vector2(0.6f, 0f), new Vector2(0.7f, 0.1f), new Vector2(0.75f, -0.1f) };
        Vector2[] obstacles7 = new Vector2[] { new Vector2(0.2f, -0.8f), new Vector2(0.25f, -0.75f), new Vector2(0.4f, -0.8f) };

        Vector2[][] arrayOfObstaclePoints = new Vector2[][] { obstacles1, obstacles2, obstacles3, obstacles4, obstacles5, obstacles6, obstacles7 };

		width = 2;
		height = 1;

		return arrayOfObstaclePoints;
	}
}
