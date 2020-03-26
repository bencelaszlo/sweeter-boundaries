using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    [SerializeField]
    private City city;
    [SerializeField]
    private UIController uIController;
    [SerializeField]
    private Building[] buildings;
    [SerializeField]
    private Board board;

    private Building selectedBuilding;

    void Update()
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
        {
            InteractWithBoard(0);
        }
        else if (Input.GetMouseButtonDown(1) && selectedBuilding != null)
        {
            InteractWithBoard(0);
        }
        else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl) && selectedBuilding != null)
        {
            InteractWithBoard(1);
        }
    }

    // action = 0 - add building
    // action = 1 - remove building
    void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPosition = board.CalculateGridPosition(hit.point);
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (action == 0 && board.CheckForBuildingAtPosition(gridPosition) == null)
                {
                        if (city.Cash >= selectedBuilding.cost)
                        {
                            city.DepositCash(selectedBuilding.cost);
                            city.buildingCounts[selectedBuilding.id]++;
                            board.AddBuilding(selectedBuilding, gridPosition);
                    }
                }
                else if (action == 1 && board.CheckForBuildingAtPosition(gridPosition) != null)
                {
                    city.DepositCash((int)(board.CheckForBuildingAtPosition(gridPosition).cost * 0.8)); // Return the 80% of the cost of the building's price
                    board.RemoveBuilding(gridPosition);
                }
            }
        }
    }

    public void EnableBuilder(int buildingIndex)
    {
        selectedBuilding = buildings[buildingIndex];
    }

}
