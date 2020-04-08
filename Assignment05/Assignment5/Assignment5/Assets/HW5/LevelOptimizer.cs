using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelOptimizer
{
    const float width = 2;//half the level width
    const float height = 2.5f;//half the level height
    const float gridSize = 0.2f;//A grid cell's width

    //Parameters you may want to use, but do not have to use
    const int POPULATION_SIZE = 10;//The size of the population
    const float MUTATION_RATE = 0.8f;//The rate at which to mutate population members
    const int NUM_CROSSOVERS = 15;//The number of crossovers/children to produce
    const int MAX_ATTEMPTS = 1000;//The max number of times to iterate the algorithm

    //Square obstacle. The points make the assumption that the worldLocation will be the center of a grid cell. 
    private Vector2[] squareObstacle = new Vector2[] { new Vector2(-1 * gridSize / 2f, -1 * gridSize / 2f), new Vector2(gridSize / 2f, -1 * gridSize / 2f), new Vector2(gridSize / 2f, gridSize / 2f), new Vector2(-1 * gridSize / 2f, gridSize / 2f) };

    //TODO; Implement a Genetic Algorithm/Evolutionary Search
    public Level GetLevel(Evaluator evaluator, float goalValue)
    {
        //Instantiate random population
        List<Level> population = new List<Level>();
        for(int i = 0; i< POPULATION_SIZE; i++)
        {
            population.Add(GetRandomLevel());
        }

        Level bestCandidateSoFar = null;
        float bestFitnessSoFar = 0;

        int time = 0;
        while(time< MAX_ATTEMPTS && bestFitnessSoFar < 0.9)
        {
            time++;

            // Mutate population - COMPLETE
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                if(Random.value < MUTATION_RATE)
                {
                    //Mutate this level i
                    population[i] = Mutate(population[i]);
                }
            }

            // Crossover Population - COMPLETE?
            //Level child = Crossover(Sample(population, evaluator, goalValue), Sample(population, evaluator, goalValue));
            //population.Add(child);
            for (int i = 0; i < NUM_CROSSOVERS; i++)
            {
                Level Sample_01 = Sample(population, evaluator, goalValue);
                Level Sample_02 = Sample(population, evaluator, goalValue);
                if (Sample_01 != Sample_02) // Only does crossover if Sample sizes are different
                {
                    Level child = Crossover(Sample_01, Sample_02);
                    population.Add(child);
                    //cross++;
                }
           
            }

            // Reduce Population - COMPLETE
            population = Reduce(population, POPULATION_SIZE, evaluator, goalValue);

            foreach(Level l in population)
            {
                float thisFitness = Fitness(l, evaluator, goalValue);
                if (thisFitness > bestFitnessSoFar)
                {
                    bestCandidateSoFar = l.Clone();
                    bestFitnessSoFar = thisFitness;

                }
            }
        }





        /**
        //Check if we've found the perfect candidate
        foreach(Level l in population)
        {
            float thisFitness = Fitness(l, evaluator, goalValue);
            if (thisFitness > bestFitnessSoFar)
            {
                bestCandidateSoFar = l;
                bestFitnessSoFar = thisFitness;
            }
        }
        */
        Debug.Log("Fitness: " + bestFitnessSoFar);
        return bestCandidateSoFar;
    }

    //Fitness, how close this level is to matching the goal value for this specific evaluator
    //You may wish to change this for the extra credit, but should otherwise not be needed
    public float Fitness(Level l, Evaluator evaluator, float goalValue)
    {
        return 1.0f - Mathf.Abs(evaluator.EvaluateLevel(l) - goalValue);
    }

    //You may want to change this mutate function for the extra credit
    public Level Mutate(Level level)
    {
        Level mutatedLevel = level.Clone();
        //Flip a coin
        if (Random.value > 0.5)
        {
            //Turn a pellet into an obstacle
            int randomPelletIndex = Random.Range(0, mutatedLevel.pellets.Count-1);

            ProtoPellet p = mutatedLevel.pellets[randomPelletIndex];

            mutatedLevel.pellets.Remove(p);

            mutatedLevel.obstacles.Add(new ProtoObstacle(p.worldLocation, squareObstacle));
        }
        else
        {
            //Turn an obstacle into a pellet
            if (mutatedLevel.obstacles.Count > 0)
            {
                int randomObstacleIndex = Random.Range(0, mutatedLevel.obstacles.Count - 1);

                ProtoObstacle p = mutatedLevel.obstacles[randomObstacleIndex];

                mutatedLevel.obstacles.Remove(p);

                //Flip another coin to see if this is a power pellet
                if (Random.value > 0.5)
                {
                    //Add a pellet
                    mutatedLevel.pellets.Add(new ProtoPellet(p.worldLocation));
                }
                else
                {
                    //Add a power pellet
                    mutatedLevel.pellets.Add(new ProtoPellet(p.worldLocation, true));
                }
            }
        }
        return mutatedLevel;
    }

    //You may want to change this mutate function for the extra credit
    public Level Crossover(Level parent1, Level parent2)
    {
        Level childLevel = new Level();

        int minObstacles = Mathf.Min(parent1.obstacles.Count, parent2.obstacles.Count);

        //Take first half of obstacles from parent1 and second half from parent2
        for(int i = 0; i<minObstacles; i++)
        {
            if (i < minObstacles / 2)
            {
                childLevel.obstacles.Add(parent1.obstacles[i].Clone());
            }
            else
            {
                childLevel.obstacles.Add(parent2.obstacles[i].Clone());
            }
        }

        int minPellets = Mathf.Min(parent1.pellets.Count, parent2.pellets.Count);

        //Take first half of pellets from parent1 and second half from parent2
        for (int i = 0; i < minPellets; i++)
        {
            if (i < minPellets / 2)
            {
                childLevel.pellets.Add(parent1.pellets[i].Clone());
            }
            else
            {
                childLevel.pellets.Add(parent2.pellets[i].Clone());
            }
        }

        return childLevel;
    }

    //Probabilistically samples a level from a population based on fitness (this is not an efficient function)
    public Level Sample(List<Level> population, Evaluator evaluator, float goalValue)
    {
        //Calculate fitnesses
        List<float> fitnesses = new List<float>();
        float sumOfFitness = 0;

        for (int i = 0; i < population.Count; i++)
        {
            float fitness = Fitness(population[i], evaluator, goalValue);
            sumOfFitness += fitness;
            fitnesses.Add(fitness);
        }

        //Convert fitnesses into weights for sampling
        for (int i = 0; i < population.Count; i++)
        {
            fitnesses[i] /= sumOfFitness;
        }

        //Sample
        float goalWeight = Random.value;
        float currWeight = 0;
        int k = 0;
        while (currWeight < goalWeight && k < population.Count-1)
        {
            currWeight += fitnesses[k];
            k += 1;
        }

        return population[k];
    }

    //Returns a reduced population where only the populationSize best levels remain
    private List<Level> Reduce(List<Level> population, int populationSize, Evaluator evaluator, float goalValue){

        foreach(Level l in population)
        {
            float fitness = Fitness(l, evaluator, goalValue);
            l.fitness = fitness;
        }

        population = population.OrderBy(x => x.fitness).ToList();
        population.Reverse();

        List<Level> finalPopulation = new List<Level>();
        for(int i = 0; i<populationSize; i++)
        {
            finalPopulation.Add(population[i]);
        }
        return finalPopulation;
    }

    //Get a random level, useful for initializing a population
    public Level GetRandomLevel()
    {
        Level l = new Level();

        //Borders
        Vector2[] topObstacle = new Vector2[] { new Vector2(-1 * width, height - 0.2f), new Vector2(width, height - 0.2f), new Vector2(width, height - 0.25f), new Vector2(-1 * width, height - 0.25f) };
        l.obstacles.Add(new ProtoObstacle(Vector3.zero, topObstacle));
        Vector2[] rightObstacle = new Vector2[] { new Vector2(width, height - 0.25f), new Vector2(width + 0.05f, height - 0.25f), new Vector2(width + 0.05f, -1 * height + 0.25f), new Vector2(width, -1 * height + 0.25f) };
        l.obstacles.Add(new ProtoObstacle(Vector3.zero, rightObstacle));
        Vector2[] leftObstacle = new Vector2[] { new Vector2(-1 * width, height - 0.25f), new Vector2(-1 * width - 0.05f, height - 0.25f), new Vector2(-1 * width - 0.05f, -1 * height + 0.25f), new Vector2(-1 * width, -1 * height + 0.25f) };
        l.obstacles.Add(new ProtoObstacle(Vector3.zero, leftObstacle));
        Vector2[] downObstacle = new Vector2[] { new Vector2(-1 * width, -1 * height + 0.2f), new Vector2(width, -1 * height + 0.2f), new Vector2(width, -1 * height + 0.25f), new Vector2(-1 * width, -1 * height + 0.25f) };
        l.obstacles.Add(new ProtoObstacle(Vector3.zero, downObstacle));

        for (float x = width*-1+gridSize; x <= width; x += gridSize)
        {
            for (float y = -1*height + gridSize*2; y <= height-gridSize; y += gridSize)
            {
                //Flip a coin
                if (Random.value > 0.5)
                {
                    //Add an obstacle
                    l.obstacles.Add(new ProtoObstacle(new Vector3(x, y), squareObstacle));
                }
                else
                {
                    //Flip another coin to see if this is a power pellet
                    if (Random.value > 0.5)
                    {
                        //Add a pellet
                        l.pellets.Add(new ProtoPellet(new Vector3(x, y)));
                    }
                    else
                    {
                        //Add a power pellet
                        l.pellets.Add(new ProtoPellet(new Vector3(x, y), true));
                    }
                }
            }
        }

        return l;
    }
}
