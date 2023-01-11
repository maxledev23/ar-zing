using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _title;
    private TMP_Text _text;

    UnityEvent previousEvent;
    UnityEvent nextEvent;

    public Button previous;
    public Button next;

    // step correspondant à l'étape en cours
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation du step
        step = 1;

        // Creation des Events pour les boutons
        if (previousEvent == null)
            previousEvent = new UnityEvent();
        
        if (nextEvent == null)
            nextEvent = new UnityEvent();

        previousEvent.AddListener(previousStep);
        nextEvent.AddListener(nextStep);

    }

    // Update is called once per frame
    void Update()
    {
        if (previous)
            previousEvent.Invoke();

        if (next)
            nextEvent.Invoke();
    }

    void previousStep()
    {
        if (step == 1)
            return;
        else
            step--;
    }
    
    void nextStep()
    {
        if (step == 0) // TODO replace 0 by lenght of instructions list
            return;
        else
            step++;
    }
}