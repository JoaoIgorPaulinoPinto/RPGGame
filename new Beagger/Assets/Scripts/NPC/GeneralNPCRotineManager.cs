//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GeneralNPCRoutineManager : MonoBehaviour
//{
//    public List<NPCBehavior> npcs;
//    public Transform targetLocation;
//    public float sleepTime;
//    public float wakeTime;

//    private bool isNPCsInSpecialAction = false;

//    private void Update()
//    {
//        float currentHour = GetCurrentHour();
//        if (currentHour > sleepTime && currentHour < wakeTime)
//        {
//            if (!isNPCsInSpecialAction)
//            {
//                SendNPCsToLocation();
//                isNPCsInSpecialAction = true;
//            }
//        }
//        else
//        {
//            if (isNPCsInSpecialAction)
//            {
//                ResumeNPCsRoutine();
//                isNPCsInSpecialAction = false;
//            }
//        }
//    }

//    private void SendNPCsToLocation()
//    {
//        foreach (var npc in npcs)
//        {
//            if (npc != null)
//            {
//                npc.StopRoutine();
//                npc.GoToLocation(targetLocation.position);
//            }
//        }

//        Debug.Log("NPCs enviados ao local especificado.");
//    }

//    private void ResumeNPCsRoutine()
//    {
//        foreach (var npc in npcs)
//        {
//            if (npc != null)
//            {
//                npc.ResumeNormalRoutine();
//            }
//        }

//        Debug.Log("NPCs retomaram suas rotinas normais.");
//    }

  
//}
