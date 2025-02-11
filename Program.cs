using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        bool boostUsed = false;
        int targetX = 0, targetY = 0;

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            if (targetX != nextCheckpointX || targetY != nextCheckpointY)
            {
                targetX = nextCheckpointX;
                targetY = nextCheckpointY;
            }

            string thrust = CalculateThrust(nextCheckpointDist, 
                Math.Abs(nextCheckpointAngle), ref boostUsed);

            Console.WriteLine($"{targetX} {targetY} {thrust}");
        }
    }

    static string CalculateThrust(int distance, int angle, ref bool boostUsed)
    {
        if (angle > 90) return "0";
        
        // Boost when perfectly aligned on long straights
        if (!boostUsed && distance > 4000 && angle < 2)
        {
            boostUsed = true;
            return "BOOST";
        }

        // Angular speed damping
        double angleFactor = Math.Cos(angle * Math.PI / 180);
        int baseThrust = distance switch {
            > 4000 => 100,
            > 2000 => 85,
            > 800 => 70,
            > 400 => 50,
            _ => 30
        };
        
        return Math.Max((int)(baseThrust * angleFactor), 30).ToString();
    }

}