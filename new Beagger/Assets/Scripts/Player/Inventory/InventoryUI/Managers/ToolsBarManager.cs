using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsBarManager : MonoBehaviour
{
    public GameObject[] slots;
    public int currentSlotIndex = 0;

    public void ChangeSelectedSlot(int i)
    {
        // Deselecionar o slot atual
         

        // Alterar o índice atual
        currentSlotIndex += i;

        // Verificar os limites para garantir que o índice está dentro do intervalo
        if (currentSlotIndex < 0)
        {
            currentSlotIndex = slots.Length - 1; // Volta para o último slot
        }
        else if (currentSlotIndex >= slots.Length)
        {
            currentSlotIndex = 0; // Volta para o primeiro slot
        }

        // Selecionar o novo slot
        // slots[currentSlotIndex].SelectSlot();
        EquipedItemsManager.Instance.ChangeEquipedTool(slots[currentSlotIndex]);
    }

    private void Update()
    {
        if (PlayerControlsManager.Instance.realease)
        {
            ScrollChengeSlot();
        }
       
    }
    public void ScrollChengeSlot()
    {
        // Mudança pelo scroll do mouse
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            ChangeSelectedSlot(-1); // Mudar para o próximo slot
        }
        else if (scroll < 0f)
        {
            ChangeSelectedSlot(1); // Mudar para o slot anterior
        }

        // Mudança pelos números do teclado (1 a 4)
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1))) // Checar teclas 1, 2, 3, 4
            {
                ChangeSelectedSlot(i - 1 - currentSlotIndex); // Ajusta para o índice correto
            }

        }
    }
    public void UpdateSelectedSlotUI(ToolsBarSlot selectedButton)
    {
        foreach (var item in slots)
        {
            item.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot t);
            t.UnSelectSlot();
        }
        selectedButton.SelectSlot();
    }
    private void Start()
    {
        ChangeSelectedSlot(0);
    }
}
