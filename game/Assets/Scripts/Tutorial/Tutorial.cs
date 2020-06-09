using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int Order;
    [TextArea(3, 10)]
    public string Explanation;

   
    void Awake()
    {
        TutorialManager.Instace.Tutorials.Add(this);
    }
    public virtual void checkIfHappening()
    {

    }
}
