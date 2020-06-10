using System;
using UnityEngine;
//namespace ParticleSwarmOptimization
//{
  public class ParticleProgram : MonoBehaviour
    {
    void Start()
    {
        Debug.Log("\nBegin Particle Swarm Optimization demo\n");
        Debug.Log("Goal is to minimize f(x0,x1) = x0 * exp( -(x0^2 + x1^2) )");
        Debug.Log("Known solution is at x0 = -0.707107, x1 = 0.000000");

        int dim = 2; // problem dimensions
        int numParticles = 5;
        int maxEpochs = 1000;
        double exitError = 0.0; // exit early if reach this error
        double minX = -10.0; // problem-dependent
        double maxX = 10.0;

        Debug.Log("\nSetting problem dimension to " + dim);
        Debug.Log("Setting numParticles = " + numParticles);
        Debug.Log("Setting maxEpochs = " + maxEpochs);
        Debug.Log("Setting early exit error = " + exitError.ToString("F4"));
        Debug.Log("Setting minX, maxX = " + minX.ToString("F1") + " " + maxX.ToString("F1"));
        Debug.Log("\nStarting PSO");

        double[] bestPosition = Solve(dim, numParticles, minX, maxX, maxEpochs, exitError);
        double bestError = Error(bestPosition);

        Debug.Log("Best position/solution found:");
        for (int i = 0; i < bestPosition.Length; ++i)
        {
            Debug.Log("x" + i + " = ");
            Debug.Log(bestPosition[i].ToString("F6") + " ");
        }
        Debug.Log("");
        Debug.Log("Final best error = ");
        Debug.Log(bestError.ToString("F5"));

        Debug.Log("\nEnd PSO demo\n");
        //Console.ReadLine();
    }

    double Error(double[] x)
    {
      // 0.42888194248035300000 when x0 = -sqrt(2), x1 = 0
      double trueMin = -0.42888194; // true min for z = x * exp(-(x^2 + y^2))
      double z = x[0] * Math.Exp( -((x[0]*x[0]) + (x[1]*x[1])) );
      return (z - trueMin) * (z - trueMin); // squared diff
    }

    double[] Solve(int dim, int numParticles, double minX, double maxX, int maxEpochs, double exitError)
    {
      // assumes existence of an accessible Error function and a Particle class
      System.Random rnd = new System.Random(0);

      Particle[] swarm = new Particle[numParticles];
      double[] bestGlobalPosition = new double[dim]; // best solution found by any particle in the swarm
      double bestGlobalError = double.MaxValue; // smaller values better

      // swarm initialization
      for (int i = 0; i < swarm.Length; ++i)
      {
        double[] randomPosition = new double[dim];
        for (int j = 0; j < randomPosition.Length; ++j)
          randomPosition[j] = (maxX - minX) * rnd.NextDouble() + minX; // 

        double error = Error(randomPosition);
        double[] randomVelocity = new double[dim];

        for (int j = 0; j < randomVelocity.Length; ++j)
        {
          double lo = minX * 0.1; 
          double hi = maxX * 0.1;
          randomVelocity[j] = (hi - lo) * rnd.NextDouble() + lo;
        }
        swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

        // does current Particle have global best position/solution?
        if (swarm[i].error < bestGlobalError)
        {
          bestGlobalError = swarm[i].error;
          swarm[i].position.CopyTo(bestGlobalPosition, 0);
        }
      } // initialization

      // prepare
      double w = 0.729; // inertia weight. see http://ieeexplore.ieee.org/stamp/stamp.jsp?arnumber=00870279
      double c1 = 1.49445; // cognitive/local weight
      double c2 = 1.49445; // social/global weight
      double r1, r2; // cognitive and social randomizations
      double probDeath = 0.01;
      int epoch = 0;

      double[] newVelocity = new double[dim];
      double[] newPosition = new double[dim];
      double newError;

      // main loop
      while (epoch < maxEpochs)
      {
        for (int i = 0; i < swarm.Length; ++i) // each Particle
        {
          Particle currP = swarm[i]; // for clarity

          // new velocity
          for (int j = 0; j < currP.velocity.Length; ++j) // each component of the velocity
          {
            r1 = rnd.NextDouble();
            r2 = rnd.NextDouble();

            newVelocity[j] = (w * currP.velocity[j]) +
              (c1 * r1 * (currP.bestPosition[j] - currP.position[j])) +
              (c2 * r2 * (bestGlobalPosition[j] - currP.position[j]));
          }
          newVelocity.CopyTo(currP.velocity, 0);

          // new position
          for (int j = 0; j < currP.position.Length; ++j)
          {
            newPosition[j] = currP.position[j] + newVelocity[j];
            if (newPosition[j] < minX)
              newPosition[j] = minX;
            else if (newPosition[j] > maxX)
              newPosition[j] = maxX;
          }
          newPosition.CopyTo(currP.position, 0);

          newError = Error(newPosition);
          currP.error = newError;

          if (newError < currP.bestError)
          {
            newPosition.CopyTo(currP.bestPosition, 0);
            currP.bestError = newError;
          }

          if (newError < bestGlobalError)
          {
            newPosition.CopyTo(bestGlobalPosition, 0);
            bestGlobalError = newError;
          }

          // death?
          double die = rnd.NextDouble();
          if (die < probDeath)
          {
            // new position, leave velocity, update error
            for (int j = 0; j < currP.position.Length; ++j)
              currP.position[j] = (maxX - minX) * rnd.NextDouble() + minX;
            currP.error = Error(currP.position);
            currP.position.CopyTo(currP.bestPosition, 0);
            currP.bestError = currP.error;

            if (currP.error < bestGlobalError) // global best by chance?
            {
              bestGlobalError = currP.error;
              currP.position.CopyTo(bestGlobalPosition, 0);
            }
          }

        } // each Particle
        ++epoch;
      } // while

      // show final swarm
      Debug.Log("\nProcessing complete");
      Debug.Log("\nFinal swarm:\n");
      for (int i = 0; i < swarm.Length; ++i)
        Debug.Log(swarm[i].ToString());

      double[] result = new double[dim];
      bestGlobalPosition.CopyTo(result, 0);
      return result;
    } // Solve

  } // program class

  public class Particle
  {
    public double[] position;
    public double error;
    public double[] velocity;
    public double[] bestPosition;
    public double bestError;

    public Particle(double[] pos, double err, double[] vel, double[] bestPos, double bestErr)
    {
      this.position = new double[pos.Length];
      pos.CopyTo(this.position, 0);
      this.error = err;
      this.velocity = new double[vel.Length];
      vel.CopyTo(this.velocity, 0);
      this.bestPosition = new double[bestPos.Length];
      bestPos.CopyTo(this.bestPosition, 0);
      this.bestError = bestErr;
    }

    public override string ToString()
    {
      string s = "";
      s += "==========================\n";
      s += "Position: ";
      for (int i = 0; i < this.position.Length; ++i)
        s += this.position[i].ToString("F4") + " ";
      s += "\n";
      s += "Error = " + this.error.ToString("F4") + "\n";
      s += "Velocity: ";
      for (int i = 0; i < this.velocity.Length; ++i)
        s += this.velocity[i].ToString("F4") + " ";
      s += "\n";
      s += "Best Position: ";
      for (int i = 0; i < this.bestPosition.Length; ++i)
        s += this.bestPosition[i].ToString("F4") + " ";
      s += "\n";
      s += "Best Error = " + this.bestError.ToString("F4") + "\n";
      s += "==========================\n";
      return s;
    }

  } // Particle


//} // ns
