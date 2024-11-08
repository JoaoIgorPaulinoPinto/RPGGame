using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture; // A imagem do cursor
    [SerializeField] Vector2 hotSpot = Vector2.zero; // Ponto de refer�ncia no cursor (geralmente o canto superior esquerdo ou o centro)

    private void Start()
    {
        // Define o cursor com a imagem fornecida e o ponto de refer�ncia
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    // Se voc� deseja restaurar o cursor padr�o, pode criar um m�todo para isso
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Restaura o cursor padr�o
    }
}
