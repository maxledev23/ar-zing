using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InstructionsList : MonoBehaviour
{
    // GameObjects
    public Button previousButton, nextButton;
    public GameObject numberObj, titleObj, descriptionObj, messageIconObj, warningIconObj;
    private GameObject StepsObject, StepObject, PreviousObject;
    private TMP_Text number, title, description;
    private Image messageIcon, warningIcon;

    // JSON file and its content
    public TextAsset jsonFile;
    private Steps stepsInJson;

    // The state variables
    private int currentStep;
    private int previousStep;

    void Start() {
        currentStep = 0;
        previousStep = -1;

        // Get the TMP Texts
        number = numberObj.GetComponent<TMP_Text>();
        title = titleObj.GetComponent<TMP_Text>();
        description = descriptionObj.GetComponent<TMP_Text>();

        // Get the images
        messageIcon = messageIconObj.GetComponent<Image>();
        warningIcon = warningIconObj.GetComponent<Image>();

        // Add listeners to buttons
        previousButton.onClick.AddListener(decrementStep);
        nextButton.onClick.AddListener(incrementStep);

        // Get data from JSON File
        stepsInJson = JsonUtility.FromJson<Steps>(jsonFile.text);

        // Find the GameObject containing all the steps
        StepsObject = GameObject.Find("Steps");

        for (int i = 0; i < StepsObject.transform.childCount; i++)
        {
            StepObject = StepsObject.transform.GetChild(i).gameObject;
            StepObject.SetActive(false);
        }

        // Set the first step
        changeText();
        changeGameObjects();
    }

    // When the next button is clicked
    public void incrementStep() {
        previousStep = currentStep;
        if(currentStep != stepsInJson.steps.Length - 1) {
        currentStep++;
        changeText();
        changeGameObjects();
        }
    }

    // When the previous button is clicked
    public void decrementStep() {
        previousStep = currentStep;
        if(currentStep != 0) {
        currentStep--;
        changeText();
        changeGameObjects();
        }
    }

    // Change the number, title and description
    private void changeText() {
        number.text = stepsInJson.steps[currentStep].number;
        title.text = stepsInJson.steps[currentStep].title;
        description.text = stepsInJson.steps[currentStep].description;
        
        if (stepsInJson.steps[currentStep].info == 1) {
            messageIcon.enabled = true;
            warningIcon.enabled = false;
        } else if (stepsInJson.steps[currentStep].info == 2) {
            messageIcon.enabled = false;
            warningIcon.enabled = true;
        } else {
            messageIcon.enabled = false;
            warningIcon.enabled = false;
        }

        Debug.Log("Current status : " + currentStep);
    }

    // Change the GameObjects on screen
    private void changeGameObjects() {
        // If it's not the start, make the previous GameObject disappear
        if(previousStep != -1) {
           PreviousObject.SetActive(false);
           Debug.Log("Desactivated previous GameObject");
        }
        StepObject = StepsObject.transform.GetChild(currentStep).gameObject;
        PreviousObject = StepObject;
        StepObject.SetActive(true);
        Debug.Log("Activated right GameObject");
    }
}