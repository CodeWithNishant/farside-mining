using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RobotController : MonoBehaviour
{
    // Capacities and Capabilities
    public float idleRadius = 5.0f;
    public float idleMoveInterval = 3.0f;
    public float movementSpeed = 3.5f;
    public int oreCapacity = 2;
    public int oreFilled = 0;

    // Other important features
    private NavMeshAgent navMeshAgent;
    private WorkerManager workerManager;
    private OreDeposit currentOreDeposit;
    private Warehouse nearestWarehouse;
    private Vector3 spawnPoint;
    private float timer;
    private bool isMining;
    private string taskAssigned;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        spawnPoint = transform.position; // Save the spawn point
        timer = idleMoveInterval; // Initialize the timer
        workerManager = FindFirstObjectByType<WorkerManager>();
        isMining = false;
        taskAssigned = null;
    }

    void Update()
    {
        if (taskAssigned == "mine-ore")
        {
            if (!isMining && currentOreDeposit != null && currentOreDeposit.oreAmount > 0)
            {
                StartCoroutine(MiningCycle());
            }
        }
        else
        {
            HandleIdleState();
        }
    }

    void HandleIdleState()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to move to a new random position
        if (timer >= idleMoveInterval)
        {
            // Reset the timer
            timer = 0f;

            // Calculate a random point within the idleRadius around the spawn point
            Vector3 randomDirection = Random.insideUnitSphere * idleRadius;
            randomDirection += spawnPoint;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, idleRadius, 1))
            {
                // Move the robot to the random position
                navMeshAgent.SetDestination(hit.position);
            }
        }
    }

    public void AssignMiningTask(OreDeposit oreDeposit)
    {
        taskAssigned = "mine-ore";
        currentOreDeposit = oreDeposit;
        isMining = false;
    }

    IEnumerator MiningCycle()
    {
        isMining = true;

        while (currentOreDeposit != null && currentOreDeposit.oreAmount > 0)
        {
            // Move to the ore deposit
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            navMeshAgent.SetDestination(currentOreDeposit.transform.position + randomOffset);

            // Wait until the robot reaches the ore deposit
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            // Mining process
            while (oreFilled < oreCapacity && currentOreDeposit.oreAmount > 0)
            {
                yield return new WaitForSeconds(1.0f);
                currentOreDeposit.ChangeOreAmount(-1);
                oreFilled++;
                Debug.Log("Mining ore: " + currentOreDeposit.oreAmount + " left.");
            }

            // Move to the warehouse
            nearestWarehouse = FindNearestWarehouse();
            navMeshAgent.SetDestination(GetWarehousePositionWithOffset(nearestWarehouse.transform.position));

            // Wait until the robot reaches the warehouse
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            // Unload ore into the warehouse
            if (currentOreDeposit.oreName == "iron")
            {
                nearestWarehouse.ChangeIronOreAmount(oreFilled);
            }
            else if (currentOreDeposit.oreName == "copper")
            {
                nearestWarehouse.ChangeCopperOreAmount(oreFilled);
            }
            Debug.Log(currentOreDeposit.oreName);
            oreFilled = 0;

            // Check if more ore is available
            if (currentOreDeposit.oreAmount <= 0)
            {
                taskAssigned = null;
                currentOreDeposit = null;
            }
        }

        isMining = false;
    }

    public void ChangeTask(string newTask)
    {
        taskAssigned = newTask;
    }

    Warehouse FindNearestWarehouse()
    {
        GameObject[] warehouses = GameObject.FindGameObjectsWithTag("Warehouse");
        Warehouse nearestWarehouse = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject warehouseObj in warehouses)
        {
            float distanceToWarehouse = Vector3.Distance(currentPosition, warehouseObj.transform.position);
            if (distanceToWarehouse < shortestDistance)
            {
                shortestDistance = distanceToWarehouse;
                nearestWarehouse = warehouseObj.GetComponent<Warehouse>();
            }
        }
        return nearestWarehouse;
    }

    Vector3 GetWarehousePositionWithOffset(Vector3 originalPosition)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        return originalPosition + randomOffset;
    }
}
