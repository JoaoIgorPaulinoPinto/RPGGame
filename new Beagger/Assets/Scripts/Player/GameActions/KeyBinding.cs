using UnityEngine;

[CreateAssetMenu(fileName = "New Key Binding", menuName = "Player Controls/Keyboard")]

public class KeyBinding : ScriptableObject
{
    public KeyCode key_inventory;
    public KeyCode key_menu;
    public KeyCode key_interact;
    public KeyCode key_atack;
    public KeyCode key_dash;
}