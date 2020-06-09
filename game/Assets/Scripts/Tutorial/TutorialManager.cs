using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> Tutorials = new List<Tutorial>();

    public Text expText;

    private static TutorialManager instance;
    public static TutorialManager Instace {

        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<TutorialManager>();
            }
            if(instance == null)
            {
               UnityEngine.Debug.Log("There is no TutorialManager");
            }

            return instance;
        }
    }
    private Tutorial currentTutorial;
    
    void Start()
    {
        setNextTutorial(0);
    }
    void Update()
    {
        if (currentTutorial)
            currentTutorial.checkIfHappening();
    }

    public void CompletedTutorial()
    {
        setNextTutorial(currentTutorial.Order + 1);
    }

    public void setNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }

        expText.text = currentTutorial.Explanation;
    }

    public void CompletedAllTutorials()
    {
        UnityEngine.Debug.Log("You have completed all the tuturials, hoerah");
        expText.text = "You have completed all the tuturials, hoerah";
    }

    public Tutorial GetTutorialByOrder(int Order)
    {
        for(int i =0; i< Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == Order)
                return Tutorials[i];
        }
        return null;
    }
}
