using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private City city;
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text cityText;

    void Start()
    {
        city = this.GetComponent<City>();
    }

    public void UpdateCityData()
    {
        int integerPopulationCurrent = (int)city.PopulationCurrent;
        int integerPopulationCeiling = (int)city.PopulationCeiling;
        int integerFood = (int)city.Food;

        cityText.text = $"Jobs: {city.JobsCurrent}/{city.JobsCeiling}\nCash: {city.Cash}\nPopulation: {integerPopulationCurrent}/{integerPopulationCeiling}\nFood: {integerFood}";
    }

    public void UpdateDayCount()
    {
        dayText.text = string.Format("Day {0}", city.Day.ToString());
    }

}
