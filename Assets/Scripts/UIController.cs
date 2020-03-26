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
        cityText.text = string.Format("Jobs: {0}/{1}\n, Cash: {2} + (+${6})\n, Population: {3}/{4}\n, Food: {5}",
        city.JobsCurrent,
        city.JobsCeiling,
        city.Cash,
        (int)city.PopulationCurrent,
        (int)city.PopulationCeiling,
        (int)city.Food,
        city.JobsCurrent);
    }

    public void UpdateDayCount()
    {
        dayText.text = string.Format("Day {0}", city.Day.ToString());
    }

}
