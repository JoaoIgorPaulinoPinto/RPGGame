
using UnityEngine;

[System.Serializable]
public virtual class NPCAction
{
    public bool enable;
    public float duration;
}

[System.Serializable]
public class TalkAction : NPCAction
{

}

[System.Serializable]
public class StealAction : NPCAction
{

}

[System.Serializable]
public class BuyAction : NPCAction
{

}

[System.Serializable]
public class SellAction : NPCAction
{

}
