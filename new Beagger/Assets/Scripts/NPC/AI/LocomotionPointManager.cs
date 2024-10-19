using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionPointManager : MonoBehaviour
{
    // Lista de Transform que representar� os pais dos objetos
    public List<Transform> parents;

    // Lista que armazenar� todos os pontos de locomo��o encontrados
    public List<Transform> locomotionPoints = new List<Transform>();

    // Este m�todo ser� chamado para buscar todos os pontos de locomo��o
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

    // M�todo recursivo para buscar pontos de locomo��o nos filhos
    private void FindPointsInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Se o objeto filho tem o componente LocomotionPoint, adiciona � lista
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
