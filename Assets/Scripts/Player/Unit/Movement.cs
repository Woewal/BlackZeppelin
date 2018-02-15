using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Unit
{
    [CreateAssetMenu(fileName = "Movement", menuName = "Unit/Movement", order = 1)]
    public class Movement : ScriptableObject
    {
        public List<Direction> directions;

        [System.Serializable]
        public class Direction
        {
            public int x;
            public int y;
        }
    }
}

