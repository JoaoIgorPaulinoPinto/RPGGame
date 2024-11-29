using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextWriter : MonoBehaviour
{
    [SerializeField] bool isComingout;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] string name;

    public TextMeshProUGUI lbl_text;
    public TextMeshProUGUI lbl_name;
    public GameObject UI;

    public float typingSpeed = 0.05f;

    private int currentPageIndex = 0;
    private bool isTyping = false;

    // Lista de frases para o diálogo
    public string[] pages;

    private void Start()
    {
        // Inicia o diálogo com a primeira frase
        lbl_text.text = "";
        StartDialog();
    }

    // Inicia o diálogo
    public void StartDialog()
    {
        UI.SetActive(true);
        lbl_name.text = name; // Nome do NPC ou quem está falando
        currentPageIndex = 0; // Inicia a partir da primeira frase
        StartCoroutine(TypeText(pages[currentPageIndex])); // Começa a digitar a primeira frase
    }

    // Função para digitar o texto gradualmente
    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        lbl_text.text = ""; // Limpa o texto anterior

        foreach (char letter in text.ToCharArray())
        {
            audioSource.PlayOneShot(clip); // Reproduz o som da letra sendo escrita
            lbl_text.text += letter; // Adiciona a letra ao texto
            yield return new WaitForSeconds(typingSpeed); // Aguarda antes de adicionar a próxima letra
        }

        isTyping = false; // Fim da digitação
    }

    // Função para passar para a próxima frase
    public void NextPage()
    {
        if (isTyping)
        {
            StopAllCoroutines(); // Se estiver digitando, interrompe a digitação e mostra o texto completo
            lbl_text.text = pages[currentPageIndex]; // Exibe o texto completo
            isTyping = false;
        }
        else if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            StartCoroutine(TypeText(pages[currentPageIndex])); // Digita a próxima frase
        }
        else
        {
            EndDialog(); // Fim do diálogo
        }
    }

    // Função para encerrar o diálogo
    private void EndDialog()
    {
        UI.SetActive(false); // Desativa a UI do diálogo
        Debug.Log("Dialog ended.");
        if (isComingout)
        {
            SceneManager.LoadScene("MainMenuScene");

        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Ao pressionar a tecla de espaço
        {
            NextPage(); // Avança para a próxima frase ou finaliza o diálogo
        }
    }
}
