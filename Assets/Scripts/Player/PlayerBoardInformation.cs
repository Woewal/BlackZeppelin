using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Unit;

public class PlayerBoardInformation
{
    public List<Unit> units = new List<Unit>();
    public List<Unit> availableUnits = new List<Unit>();

    public void SetAvailableUnits()
    {
        foreach (Unit unit in units)
        {
            //check alive
            availableUnits.Add(unit);
        }
    }
}
