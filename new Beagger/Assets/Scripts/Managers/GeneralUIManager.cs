using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIManager : MonoBehaviour
{
    public static GeneralUIManager Instance { get; private set; }
    public Animator animator;

    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do PlayerControlsManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destr�i a nova inst�ncia se outra j� existir
        }
        else
        {
            Instance = this; // Define esta como a inst�ncia ativa
          
        }

    }

    public void SetAnimatioState(string parameterName, bool value)
    {
        Debug.Log($"Setting animation parameter {parameterName} to {value}");
        animator.SetBool(parameterName, value);
    }
}
