using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDistanceChecker : MonoBehaviour
{
    [SerializeField]GameObject btnStartDialog;
    [SerializeField]Transform player;
    [SerializeField] float minDistance;
    void DistanceCheck()
    {
        if(Vector2.Distance(this.transform.position, player.position) < minDistance)
        {
            btnStartDialog.SetActive(true);
        }
        else
        {
            btnStartDialog.SetActive(false);
        }
    }

    private void Update()
    {
        DistanceCheck();
    }
}
