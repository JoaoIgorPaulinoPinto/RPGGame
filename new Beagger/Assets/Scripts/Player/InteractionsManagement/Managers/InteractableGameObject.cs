using UnityEngine;
interface IInteractable
{
    public void Interact();
}
public class InteractableGameObject : MonoBehaviour
{
    public string interactionName;
    public string interactableObjectName;
    public string description;
    public bool isOnFocus = false;

    
}
