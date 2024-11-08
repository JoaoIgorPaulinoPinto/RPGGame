using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture; // A imagem do cursor
    [SerializeField] Vector2 hotSpot = Vector2.zero; // Ponto de referência no cursor (geralmente o canto superior esquerdo ou o centro)

    private void Start()
    {
        // Define o cursor com a imagem fornecida e o ponto de referência
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    // Se você deseja restaurar o cursor padrão, pode criar um método para isso
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Restaura o cursor padrão
    }
}
