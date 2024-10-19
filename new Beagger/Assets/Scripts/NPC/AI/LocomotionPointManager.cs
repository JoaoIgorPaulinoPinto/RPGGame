using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionPointManager : MonoBehaviour
{
    // Lista de Transform que representará os pais dos objetos
    public List<Transform> parents;

    // Lista que armazenará todos os pontos de locomoção encontrados
    public List<Transform> locomotionPoints = new List<Transform>();

    // Este método será chamado para buscar todos os pontos de locomoção
    public void FindLocomotionPoints()
    {
        // Limpa a lista de pontos antes de buscar novamente
        locomotionPoints.Clear();

        // Percorre cada parent fornecido
        foreach (Transform parent in parents)
        {
            // Busca nos filhos do parent o componente LocomotionPoint
            FindPointsInChildren(parent);
        }
    }

    // Método recursivo para buscar pontos de locomoção nos filhos
    private void FindPointsInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Se o objeto filho tem o componente LocomotionPoint, adiciona à lista
            if (child.GetComponent<LocomotionPoint>() != null)
            {
                locomotionPoints.Add(child);
            }

            // Chama recursivamente para verificar os filhos dos filhos
            if (child.childCount > 0)
            {
                FindPointsInChildren(child);
            }
        }
    }
}
