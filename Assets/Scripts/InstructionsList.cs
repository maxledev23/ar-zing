using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class InstructionsList : MonoBehaviour
{
    // Déclaration des objets
    public Button previousButton, nextButton;
    public GameObject titleObj, descriptionObj;
    private TMP_Text title, description;

    // Index de l'instruction en cours
    private int currentInstruction;

    // Tableau des instructions
    private Instructions instructionsList;

    // Fichier JSON contenant les instructions
    public TextAsset jsonFile;

    void Start() {
        // Initialisation de l'index
        currentInstruction = 0;

        // Initialisation des éléments de texte
        title = titleObj.GetComponent<TMP_Text>();
        description = descriptionObj.GetComponent<TMP_Text>();

        // Ajouts des listeners sur les clicks de boutons
        previousButton.onClick.AddListener(decrementInstruction);
        nextButton.onClick.AddListener(incrementInstruction);

        // Deserialize JSON File
        instructionsList = JsonUtility.FromJson<Instructions>(jsonFile.text);

        foreach (Instruction item in instructionsList.Items) {
            Debug.Log(item.title);
        }

        // Initialise les champs de texte avec la première Instruction
        changeText(currentInstruction);
    }

    public void incrementInstruction() {
        // Incrémente l'index si on ne dépasse pas la taille max du tableau d'instruction
        if(currentInstruction != instructionsList.Items.Length - 1) {
            currentInstruction++;
            changeText(currentInstruction);
        }
    }

    public void decrementInstruction() {
        // Décrémente l'index si on n'est pas à la taille minimale
        if(currentInstruction != 0) {
            currentInstruction--;
            changeText(currentInstruction);
        }
    }

    private void changeText(int currentInstruction) {
        // Change le titre et le texte en fonction de l'index
        title.text = instructionsList.Items[currentInstruction].title;
        description.text = instructionsList.Items[currentInstruction].description;
        Debug.Log("Current status : " + currentInstruction);
    }
}