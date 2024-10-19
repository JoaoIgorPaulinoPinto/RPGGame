using UnityEngine;
interface IInteractable
{
    public void Interact();
}
public class InteractableGameObject : MonoBehaviour
{
    public string interactionName;

    public bool isOnFocus = false;

    
}
