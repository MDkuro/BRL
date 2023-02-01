using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public List<CheckPoint> checkPoints;
    public List<OneWayRoot> oneWayRoots;

    private List<CheckPoint> openList = new List<CheckPoint>();
    private List<CheckPoint> closeList = new List<CheckPoint>();

    public List<CheckPoint> FindPath(int startPointId, int goalId)
    {
        foreach(var checkPoint in checkPoints)
        {
            checkPoint.cost = 0;
            checkPoint.distance = 0;
            checkPoint.total = 0;
            checkPoint.fatherPoint = null;
        }
        openList.Clear();
        closeList.Clear();

        var startPoint = checkPoints.First(point => point.Id == startPointId);
        if(startPointId == goalId)
        {
            return new List<CheckPoint> { startPoint };
        }

        
        var directRoot = oneWayRoots.FirstOrDefault(root => root.startPointId == startPointId && root.endPointId == goalId);
        if (directRoot != null)
        {
            var endPoint = checkPoints.First(point => point.Id == goalId);
            return new List<CheckPoint> { startPoint, endPoint };
        }

        startPoint.cost = 0;
        startPoint.distance = 2;
        startPoint.total = startPoint.cost + startPoint.distance;
        openList.Add(startPoint);

        while (!openList.Any(point => point.Id == goalId))
        {
            var minTotal = openList.Min(openPoint => openPoint.total);
            var minOpenPoint = openList.First(openPoint => openPoint.total == minTotal);
            openList.Remove(minOpenPoint);
            closeList.Add(minOpenPoint);

            foreach (var closePoint in closeList)
            {
                foreach (var oneWayRoot in oneWayRoots.Where(root => root.startPointId == closePoint.Id))
                {
                    var nextPoint = checkPoints.First(point => point.Id == oneWayRoot.endPointId);
                    nextPoint.cost = closePoint.cost + 1;
                    var hasDirectRoot = oneWayRoots.FirstOrDefault(root => root.startPointId == nextPoint.Id && root.endPointId == goalId);
                    nextPoint.distance = hasDirectRoot ? 1 : 2;
                    nextPoint.total = nextPoint.cost + nextPoint.distance;

                    var closeNextPoint = closeList.FirstOrDefault(point => point.Id == nextPoint.Id);
                    if (closeNextPoint != null)
                    {
                        if(closeNextPoint.total > nextPoint.total)
                        {
                            nextPoint.fatherPoint = closePoint;
                            closeList.Remove(closeNextPoint);
                            openList.Add(nextPoint);
                        }
                        continue;
                    }
                    var openNextPoint = openList.FirstOrDefault(point => point.Id == nextPoint.Id);
                    if(openNextPoint != null)
                    {
                        if(openNextPoint.total > nextPoint.total)
                        {
                            nextPoint.fatherPoint = closePoint;
                            openList.Remove(openNextPoint);
                            openList.Add(nextPoint);
                        }
                        continue;
                    }

                    nextPoint.fatherPoint = closePoint;
                    openList.Add(nextPoint);
                }
            }
        }

        var goalPoint = openList.First(point => point.Id == goalId);
        var travelPoints = new List<CheckPoint>();
        var prevPoint = goalPoint;
        while(prevPoint != null)
        {
            travelPoints.Add(prevPoint);
            if (prevPoint.Id == startPointId)
                break;

            prevPoint = prevPoint.fatherPoint;
        }

        travelPoints.Reverse();
        return travelPoints;
        
    }

    private void OnDrawGizmos()
    {
        foreach (var findPath in oneWayRoots)
        {
            var startPoint = checkPoints.FirstOrDefault(point => point.Id == findPath.startPointId);
            var endPoint = checkPoints.FirstOrDefault(point => point.Id == findPath.endPointId);
            if (startPoint == null || endPoint == null)
            {
                continue;
            }
            Gizmos.DrawLine(startPoint.transform.position, endPoint.transform.position);
        }
    }

}
