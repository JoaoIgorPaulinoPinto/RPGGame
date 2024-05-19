using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Resource")]
public class Item : ScriptableObject{ 
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject? prefab;

  /*  public virtual void DropItem(Transform Spwnposition, GameObject[] prefabs)
    {
        Debug.Log("item dropado");
    }*/
}