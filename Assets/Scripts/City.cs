using UnityEngine;

public class City : MonoBehaviour
{
    public int Cash { get; set; }
    public int Day { get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[4];

    private const int START_CASH = 10_000_000;
    private const int START_FOOD = 0;
    private const int START_JOB_CEILING = 0;

    public const int PAYROLL = 1000;
    private const float FARM_FOOD = 5f;
    private const int HOUSE_MAX_POPULATION = 5;
    private const float FOOD_CONSUMPTION = 1.0f;

    private UIController uiController;

    void Start()
    {
        uiController = GetComponent<UIController>();

        for (int i = 0; i < buildingCounts.Length; i++)
        {
            buildingCounts[i] = 0;
        }

        Cash = START_CASH;
        Food = START_FOOD;
        JobsCeiling = START_JOB_CEILING;
    }

    public void EndTurn()
    {
        Day++;
        Debug.Log("Day ended.");

        CalculateJobs();
        CalculateCash();
        CalculateFood();
        CalculatePopulation();

        uiController.UpdateCityData();
        uiController.UpdateDayCount();

        Debug.LogFormat("Jobs: {0}/{1}, Cash: {2}, pop: {3}/{4}, Food: {5}", JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        JobsCurrent = Mathf.Min(JobsCeiling, (int)PopulationCurrent);
    }

    void CalculateCash()
    {
        Cash += JobsCurrent * PAYROLL;
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
        uiController.UpdateCityData();
    }

    void CalculateFood()
    {
        Food += buildingCounts[2] * FARM_FOOD;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * HOUSE_MAX_POPULATION;

        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            Food -= PopulationCurrent * FOOD_CONSUMPTION;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * FOOD_CONSUMPTION, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * FOOD_CONSUMPTION;
        }
    }

}
