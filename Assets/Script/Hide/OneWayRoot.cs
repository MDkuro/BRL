using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OneWayRoot", menuName = "CreateOneWayRoot")]
public class OneWayRoot : ScriptableObject
{

    public int startPointId;
    public int endPointId;
    public OneWayDirection direction;
    
}
