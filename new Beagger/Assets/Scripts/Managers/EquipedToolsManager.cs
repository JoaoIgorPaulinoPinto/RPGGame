using UnityEngine;

public class EquipedToolsManager : MonoBehaviour
{
    [Tooltip("Ferramenta que esta na mao do jogador")]
    public Tool equipedTool;

    public void ChangeEquipedTool(Tool tool)
    {
        equipedTool = tool;
    }
}
