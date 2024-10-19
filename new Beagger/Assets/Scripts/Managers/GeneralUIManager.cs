using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIManager : MonoBehaviour
{
    public static GeneralUIManager Instance { get; private set; }
    public Animator animator;

    private void Awake()
    {
        // Verifica se já existe uma instância do PlayerControlsManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se outra já existir
        }
        else
        {
            Instance = this; // Define esta como a instância ativa
          
        }

    }

    public void SetAnimatioState(string parameterName, bool value)
    {
        Debug.Log($"Setting animation parameter {parameterName} to {value}");
        animator.SetBool(parameterName, value);
    }
}
