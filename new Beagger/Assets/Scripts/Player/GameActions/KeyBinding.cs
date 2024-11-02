using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Binding", menuName = "Player Controls/Keyboard")]

public class KeyBinding : ScriptableObject
{
    public KeyCode key_inventory;
    public KeyCode key_menu;
    public KeyCode key_interact;
    public int mouseKey_atack;
    public int mouseKey_use;
    public KeyCode key_use;
}   