using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Dialog Infos", menuName = "NPC/New NPC Dialog Infos")]

public class NPCDialogInfo : ScriptableObject
{
    public List<Dialog> dialogtexts;
    public string NPCName;
    [System.Serializable]
    public class Page
    {
        public string message;
    }
    [System.Serializable]
    public class Dialog
    {
        public List<Page> pages = new List<Page>();
        public int rarityIndex = 1;
        public float letterDelay;

        public bool once;
        public bool oncePageIsPlayed;
    }

}
/*
public class NPCDialogInfo : MonoBehaviour  
{
    public List<Dialog> dialogtexts;
    public string NPCName;
    [System.Serializable]
    public class Page
    {
        public string message;
    }
    [System.Serializable]
    public class Dialog
    {
        public List<Page> pages = new List<Page>();
        public int rarityIndex = 1;
        public float letterDelay;

        public bool once;
        public bool oncePageIsPlayed;
    }

}*/
