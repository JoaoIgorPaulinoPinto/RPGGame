using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsConfigAuto : MonoBehaviour
{
    [SerializeField] Transform[] parents;
    [SerializeField] List<Transform> points;

    private void Awake()
    {
        GetComponent<NPCBehaviorManager>().agent.points.Clear();
        foreach (var child in parents)
        {
            for (int i = 0; i < child.childCount; i++)
            {
                if(child.GetChild(i).gameObject != this.gameObject)
                {
                    NPCInteractionPoint point;
                    child.GetChild(i).TryGetComponent<NPCInteractionPoint>(out point);
                    if (point)
                    {
                        points.Add(point.transform);

                        GetComponent<NPCBehaviorManager>().agent.points = points;
                    }
                }  
            }
        }
    }
}

