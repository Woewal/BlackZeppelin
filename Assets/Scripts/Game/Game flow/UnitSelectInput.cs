using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Obstacles;
using System.Linq;
public class UnitSelectInput : MonoBehaviour
{
    ActionController actionController;
    RoundController roundController;

    List<Unit> horizontalUnits = new List<Unit>();
    List<Unit> verticalUnits = new List<Unit>();

    Unit currentUnit;
    int currentX;
    int currentY;

    private void Start()
    {
        roundController = GetComponent<RoundController>();
        actionController = GetComponent<ActionController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelection(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSelection(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelection(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeSelection(1, 0);
        }
        else if (Input.GetButtonDown("Submit"))
        {
            Confirm();
        }
    }

    public void Enable(List<Unit> availableUnits)
    {
        verticalUnits.Clear();
        horizontalUnits.Clear();

        this.enabled = true;

        foreach (var unit in availableUnits)
        {
            verticalUnits.Add(unit);
            horizontalUnits.Add(unit);
        }

        CalculatePositions();
        SelectUnit(horizontalUnits[0]);
    }

    public void Disable()
    {
        this.enabled = false;
    }

    void CalculatePositions()
    {
        horizontalUnits.OrderBy(
            x => Camera.main.WorldToScreenPoint(gameObject.transform.position)
        );
        verticalUnits.OrderBy(
            x => Camera.main.WorldToScreenPoint(gameObject.transform.position)
        );

        currentUnit = horizontalUnits[0];
    }

    void ChangeSelection(int x, int y)
    {
        ChangeIndex(x, y);

        if (x != 0)
        {
            SelectUnit(horizontalUnits[currentX]);
            SetVerticalIndex(currentUnit);
        }
        else if (y != 0)
        {
            SelectUnit(verticalUnits[currentY]);
            SetHorizontalIndex(currentUnit);
        }
    }

    void Confirm()
    {
        roundController.currentUnit = currentUnit;
        actionController.StartUnitTurn();
        Disable();
    }

    void ChangeIndex(int x, int y)
    {
        if (currentX + x < 0)
        {
            currentX = horizontalUnits.Count;
        }
        if (currentY + y < 0)
        {
            currentY = verticalUnits.Count;
        }

        if (currentX + x > horizontalUnits.Count - 1)
        {
            currentX = 0;
            return;
        }
        if (currentY + y > verticalUnits.Count - 1)
        {
            currentY = 0;
            return;
        }

        currentX += x;
        currentY += y;
    }

    void SetHorizontalIndex(Unit unit)
    {
        currentX = horizontalUnits.FindIndex(a => a == unit);
    }

    void SetVerticalIndex(Unit unit)
    {
        currentY = verticalUnits.FindIndex(a => a == unit);
    }

    void SelectUnit(Unit unit)
    {
        currentUnit = unit;
        GameController.instance.cameraController.TargetGameObject = unit.gameObject;
    }
}
