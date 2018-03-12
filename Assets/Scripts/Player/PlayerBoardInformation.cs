using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Unit;

public class PlayerBoardInformation
{
    public List<Unit> units = new List<Unit>();

    public void RemoveEmpty()
    {
        units.RemoveAll(item => item == null);
    }
}
