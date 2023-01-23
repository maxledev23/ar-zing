using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InstructionsList : MonoBehaviour
{
    // GameObjects
    public Button previousButton, nextButton, quitButton, noticeButton;
    public GameObject areaTargetSteps, imageTargetSteps1, imageTargetSteps2, numberObj, titleObj, descriptionObj, messageIconObj, warningIconObj, hitbox;
    private GameObject StepsObject, StepObject, PreviousObject;
    private GameObject[] StepsObjects, PreviousObjects;
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
        quitButton.onClick.AddListener(quitApp);

        // Disable button
        previousButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // Get data from JSON File
        stepsInJson = JsonUtility.FromJson<Steps>(jsonFile.text);

        // Create GameObjects Arrays
        StepsObjects = new GameObject[3];
        PreviousObjects = new GameObject[3];

        // Assigning GameObjects to the Steps Array
        StepsObjects[0] = areaTargetSteps;
        StepsObjects[1] = imageTargetSteps1;
        StepsObjects[2] = imageTargetSteps2;

        foreach (GameObject StepsObject in StepsObjects) {
            for (int i = 0; i < StepsObject.transform.childCount; i++)
            {
                StepObject = StepsObject.transform.GetChild(i).gameObject;
                StepObject.SetActive(false);
            }
        }
        // Set the first step
        changeText();
        changeGameObjects();
    }

    // When the quit button is clicked
    public void quitApp() {
        Application.Quit();
    }

    // When the next button is clicked
    public void incrementStep() {
        previousStep = currentStep;
        if(currentStep != stepsInJson.steps.Length - 1) {
            currentStep++;
            changeText();
            changeGameObjects();
            previousButton.gameObject.SetActive(true);
            noticeButton.gameObject.SetActive(false);
        }

        if (currentStep == stepsInJson.steps.Length - 1) {
            nextButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(true);
        }
    }

    // When the previous button is clicked
    public void decrementStep() {
        previousStep = currentStep;
        if(currentStep != 0) {
            currentStep--;
            changeText();
            changeGameObjects();

            nextButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(false);
            if (currentStep == 0) {
                previousButton.gameObject.SetActive(false);
                noticeButton.gameObject.SetActive(true);
            }
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
        for (int i = 0; i < StepsObjects.Length; i++) {
            StepsObject = StepsObjects[i];
            if (previousStep != -1) {
                PreviousObjects[i].SetActive(false);
                Debug.Log("Desactivated previous GameObject");
            }

            StepObject = StepsObject.transform.GetChild(currentStep).gameObject;

            if (i != 0) {
                if (StepObject.transform.childCount > 0) {
                    hitbox.SetActive(false);
                }
            }
            else{
                hitbox.SetActive(true);
            }

            PreviousObjects[i] = StepObject;
            StepObject.SetActive(true);
            Debug.Log("Activated right GameObject");

            if (currentStep == StepsObject.transform.childCount - 1) {
                Handheld.Vibrate();
            }
        } 
    }
}