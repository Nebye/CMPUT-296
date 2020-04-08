using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KClusterer
{
    const int K = 6;//TODO; set K to the optimal value that you've found via experimentation
    const int MAX_ATTEMPTS = 10000;//Maximum number of clustering attempts, you may want to use this
    const float threshold = 1f;//Threshold for cluster similarity, you may want to use this and alter it if so

    //TODO; fix this function

    // 1) Randomly select K amount of centers
    // 2) Measure distance between all datapoints and the randomly selected centers 
    // 3) Set those datapoints to whatever center is closest to it
    // 4) Get the mean/average ~  GetAverage() ~ of each cluster
    // 5) Set those means to be the new centers and repeat (steps 2 - 4) up to MAX_ATTEMPTS or if the clusters stay the same twice in a row (whatever occurs first)
    public Dictionary<Datapoint, List<Datapoint>> Cluster(Datapoint[] datapoints)
    {
        //This datastructure will hold the clusters as lists by their centers. 
        Dictionary<Datapoint, List<Datapoint>> clustersByCenters = new Dictionary<Datapoint, List<Datapoint>>();

        //Select K random centers to start
        List<Datapoint> centers = new List<Datapoint>();

        //List<Datapoint> old_centers = new List<Datapoint>();

        //Choose K random points to be centers
        while (centers.Count < K)
        {


            // Generate a random index less than the size of the array.  
            int randomIndex = Random.Range(0, datapoints.Length);
            Datapoint randomCenter = datapoints[randomIndex];



            if (!centers.Contains(randomCenter))
            {
                centers.Add(randomCenter);
            }
        }

        bool twice = false;
        int MAX = 10000;
        // NUMBER 5
        while ((MAX != 0) && (twice == false))
        {
            // Instantiate clusters by these centers
            // NUMBER 3
            foreach (Datapoint center in centers)
            {
                clustersByCenters.Add(center, new List<Datapoint>());
            }


        
            // Map each datapoint to its closest center
            foreach (Datapoint pnt in datapoints)
            {
                Datapoint closestCenter = null;
                float minDistance = float.PositiveInfinity;

                foreach (Datapoint center in centers)
                {
                    float thisDistance = Distance(pnt, center);
                    if (thisDistance < minDistance)
                    {
                        closestCenter = center;
                        minDistance = thisDistance;
                    }
                }

                clustersByCenters[closestCenter].Add(pnt);
            }

            // Get the mean of each cluster and set them as the new centers
            // NUMBER 4
            List<Datapoint> new_centers = new List<Datapoint>();

            for (int i = 0; i == K; i++)  
            {
                // go through each cluster and get median
                // set median as the new centers - in the new centers list

                new_centers[i] = GetAverage(clustersByCenters[i]);
            }
            // replace values of centers list with items in new_centers list


            // if it is the first time around, do not check if cluster medians are the same
            if (MAX_ATTEMPTS != 10000)
            {
                int amount_of_same = 0;
                for(int x = 0; x == K; x++)
                {
                    if (new_centers[x] == centers[x])
                    {
                        amount_of_same++;
                    }
                    x++;
                }
                if (amount_of_same == K)
                {
                    twice = true;
                }
            }
            
            //List<Datapoint> centers = new List<Datapoint>(); // make centers list empty
            //centers = new_centers; // fill centers with the new centers
            for (int i = 0; i == K; ++i)
            {
                centers[i] = new_centers[i];
            }



            MAX = MAX - 1;

        }
        

        return clustersByCenters;
    }

    //Calculate the difference between sets of centers
    private float DifferenceBetweenCenters(Datapoint[] centers1, Datapoint[] centers2)
    {
        List<Datapoint> mappedPoints = new List<Datapoint>();
        float totalDistance = 0;
        foreach(Datapoint c1 in centers1)
        {
            Datapoint minPoint = null;
            float minDistance = float.PositiveInfinity;

            foreach(Datapoint c2 in centers2)
            {
                if (!mappedPoints.Contains(c2))
                {
                    float thisDistance = Distance(c1, c2);

                    if (thisDistance < minDistance)
                    {
                        minDistance = thisDistance;
                        minPoint = c2;
                    }
                }
            }
            mappedPoints.Add(minPoint);
            totalDistance += minDistance;
        }
        
        return totalDistance;
    }

    //Calculate and returns the geometric median of the given datapoints
    public Datapoint GetMedian(Datapoint[] datapoints)
    {
        Datapoint medianPnt = null;
        float totalDistance = float.PositiveInfinity;

        for(int i = 0; i<datapoints.Length; i++)
        {
            float thisDistance = 0;
            for(int j=0; j<datapoints.Length; j++)
            {
                if (i != j)
                {
                    thisDistance += Distance(datapoints[i], datapoints[j]);
                }
            }

            if (thisDistance < totalDistance)
            {
                totalDistance = thisDistance;
                medianPnt = datapoints[i];
            }
        }

        return medianPnt;
    }

    //Calculate and returns the average of the given datapoints
    public Datapoint GetAverage(Datapoint[] datapoints)
    {
        Datapoint sumDatapoint = new Datapoint("", 0, 0, 0, 0, 0, 0, 0, 0);
        int churnedVal = 0;

        foreach(Datapoint d in datapoints)
        {
            sumDatapoint = new Datapoint("", sumDatapoint.HoursPlayed + d.HoursPlayed, sumDatapoint.Level + d.Level, sumDatapoint.PelletsEaten + d.PelletsEaten, sumDatapoint.FruitEaten + d.FruitEaten, sumDatapoint.GhostsEaten + d.GhostsEaten, sumDatapoint.AvgScore + d.AvgScore, sumDatapoint.MaxScore + d.MaxScore, sumDatapoint.TotalScore + d.TotalScore);

            if (d.Churned)
            {
                churnedVal += 1;
            }
            else
            {
                churnedVal -= 1;
            }
        }
        //Calculate averages
        int hoursPlayed = (int)(((float)sumDatapoint.HoursPlayed) / ((float)datapoints.Length));
        int level = (int)(((float)sumDatapoint.Level) / ((float)datapoints.Length));
        int pelletsEaten = (int)(((float)sumDatapoint.PelletsEaten) / ((float)datapoints.Length));
        int fruitEaten = (int)(((float)sumDatapoint.FruitEaten) / ((float)datapoints.Length));
        int ghostsEaten = (int)(((float)sumDatapoint.GhostsEaten) / ((float)datapoints.Length));
        float avgScore = (((float)sumDatapoint.AvgScore) / ((float)datapoints.Length));
        int maxScore = (int)(((float)sumDatapoint.MaxScore) / ((float)datapoints.Length));
        int totalScore = (int)(((float)sumDatapoint.MaxScore) / ((float)datapoints.Length));

        bool churned = false;
        if (churnedVal > 0)
        {
            churned = true;
        }

        return new Datapoint("", hoursPlayed, level, pelletsEaten, fruitEaten, ghostsEaten, avgScore, maxScore, totalScore, churned);
    }

    //TODO; alter this distance function
    //WARNING: DO NOT USE CHURNED AS PART OF THIS FUNCTION

    // to fix make these relative to each other, ex since there are one fruit per every 240 pellets, make fruit have 240 times more weight
    public static float Distance(Datapoint a, Datapoint b)
    {
        float dist = 0;
        dist += Mathf.Abs(a.HoursPlayed - b.HoursPlayed);
        dist += Mathf.Abs(a.Level - b.Level);
        dist += Mathf.Abs(a.PelletsEaten - b.PelletsEaten);
        dist += Mathf.Abs(a.FruitEaten - b.FruitEaten);
        dist += Mathf.Abs(a.GhostsEaten - b.GhostsEaten);
        dist += Mathf.Abs(a.AvgScore - b.AvgScore);
        dist += Mathf.Abs(a.MaxScore - b.MaxScore);
        dist += Mathf.Abs(a.TotalScore - b.TotalScore);
        return dist;
    }
    
}

