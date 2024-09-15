using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestData", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
   public List<Quest> quests;
}   
