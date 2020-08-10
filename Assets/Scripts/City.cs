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

    private UIController uiController;

    void Start()
    {
        uiController = GetComponent<UIController>();

        for (int i = 0; i < buildingCounts.Length; i++)
        {
            buildingCounts[i] = 0;
        }

        Cash = 50;
        Food = 0;
        JobsCeiling = 0;
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
        Cash += JobsCurrent * 2;
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
        uiController.UpdateCityData();
    }

    void CalculateFood()
    {
        Food += buildingCounts[2] * 4f;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 5;
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            Food -= PopulationCurrent * 0.25f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * 0.25f, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * 0.25f;
        }
    }

}
