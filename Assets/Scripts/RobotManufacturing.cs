using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RobotManufacturing : MonoBehaviour
{
    public ResourceManager resourceManager;
    public WorkerManager workerManager; // Reference to WorkerManager
    public Button manufactureWorkerButton;
    public Button manufactureSoldierButton;
    public TextMeshProUGUI successMessageText;
    public TextMeshProUGUI errorMessageText;
    public float messageDisplayDuration = 2.0f;
    public float manufactureDelay = 5.0f; // Time delay for robot manufacturing
    public GameObject workerRobotPrefab;   // Variable to store Worker robot prefab
    public GameObject soldierRobotPrefab;
    public Transform spawnLocation;

    void Start()
    {
        // Add listeners to buttons
        manufactureWorkerButton.onClick.AddListener(() => ManufactureRobot("Worker"));
        manufactureSoldierButton.onClick.AddListener(() => ManufactureRobot("Soldier"));

        // Hide message texts initially
        successMessageText.gameObject.SetActive(false);
        errorMessageText.gameObject.SetActive(false);
    }

    void ManufactureRobot(string robotType)
    {
        // Define resource costs
        int steelCost = (robotType == "Worker") ? 10 : 20;
        int cpuCost = (robotType == "Worker") ? 5 : 7;

        // Check if resources are sufficient
        if (resourceManager.GetProductAmount("Steel") >= steelCost && resourceManager.GetProductAmount("CPU") >= cpuCost)
        {
            // Deduct resources
            resourceManager.DecrementProduct("Steel", steelCost);
            resourceManager.DecrementProduct("CPU", cpuCost);

            // Show success message and start the robot manufacture coroutine
            ShowMessage(successMessageText, robotType + " robot is being manufactured!");
            StartCoroutine(ManufactureRobotAfterDelay(robotType));
        }
        else
        {
            ShowMessage(errorMessageText, "Not enough resources!");
        }
    }

    private IEnumerator ManufactureRobotAfterDelay(string robotType)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(manufactureDelay);

        GameObject robotPrefab = null;

        // Determine which prefab to instantiate based on robotType
        if (robotType == "Worker")
        {
            robotPrefab = workerRobotPrefab;
        }
        else if (robotType == "Soldier")
        {
            robotPrefab = soldierRobotPrefab;
        }
        else
        {
            Debug.LogError("Unknown robot type!");
            yield break; // Exit coroutine if the robot type is unknown
        }

        // Spawn the robot prefab at the specified spawn location
        GameObject newRobot = Instantiate(robotPrefab, spawnLocation.position, Quaternion.identity);

        // Notify WorkerManager about the new robot
        if (robotType == "Worker")
        {
            workerManager.RegisterRobot(newRobot);
        }

        ShowMessage(successMessageText, robotType + " robot has been manufactured!");
    }

    private void ShowMessage(TextMeshProUGUI messageText, string message)
    {
        // Display the message
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        // Hide the message after a certain duration
        Invoke(nameof(HideMessages), messageDisplayDuration);
    }

    private void HideMessages()
    {
        successMessageText.gameObject.SetActive(false);
        errorMessageText.gameObject.SetActive(false);
    }
}
