using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBoardInformation
{
    public List<Unit> units;
    public List<Unit> availableUnits;

    public void SetAvailableUnits()
    {
        foreach (Unit unit in units)
        {
            //check alive
            availableUnits.Add(unit);
        }
    }
}
