using UnityEngine;
using System.Collections.Generic;

public class WorkerManager : MonoBehaviour
{
    private List<GameObject> idleRobots = new List<GameObject>();
    private List<GameObject> workingRobots = new List<GameObject>();

    public void RegisterRobot(GameObject robot)
    {
        // Add the newly instantiated robot to the list of idle robots
        idleRobots.Add(robot);
    }

    public void RobotWorking(GameObject robot)
    {
        workingRobots.Add(robot);
    }

    // Assign mining work to a robot
    public void AssignMiningWork(OreDeposit oreDeposit)
    {
        if (idleRobots.Count > 0)
        {
            int randomIndex = Random.Range(0, idleRobots.Count);
            GameObject robot = idleRobots[randomIndex];

            // Assign the mining work
            Debug.Log("This is the oreDeposit", oreDeposit);
            robot.GetComponent<RobotController>().AssignMiningTask(oreDeposit); ;

            idleRobots.RemoveAt(randomIndex);
            RobotWorking(robot);

            // Update the info at the ore deposit UI
            oreDeposit.GetComponent<OreDeposit>().IncreaseWorkers();
        }
        else
        {
            Debug.LogWarning("No idle robots available!");
        }
    }

    // Assign manufacturing work to a robot
    public void AssignManufacturingWork()
    {
        if (idleRobots.Count > 0)
        {
            int randomIndex = Random.Range(0, idleRobots.Count);
            GameObject robot = idleRobots[randomIndex];

            // Assign the manufacturing work (implementation depends on your game logic)
            // robot.GetComponent<RobotController>().StartManufacturing();

            idleRobots.RemoveAt(randomIndex);
        }
        else
        {
            Debug.LogWarning("No idle robots available!");
        }
    }

    public void RobotFinishedWork(OreDeposit oreDeposit)
    {
        int randomIndex = Random.Range(0, workingRobots.Count);
        GameObject robot = workingRobots[randomIndex];
        robot.GetComponent<RobotController>().ChangeTask(null);
        idleRobots.Add(robot);
        oreDeposit.GetComponent<OreDeposit>().DecreaseWorkers();
    }
}
