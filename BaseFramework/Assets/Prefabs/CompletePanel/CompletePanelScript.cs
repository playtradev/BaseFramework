using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePanelScript : MonoBehaviour
{
	public void Feedback()
    {
        Application.OpenURL("https://www.docs.google.com/forms/d/e/1FAIpQLScNr-hMFEzglp7uk9XRvAR6xPijnFNvJrk5m2laU0hg0HhUrg/viewform?usp=sf_link");
    }

    public void MoreGames()
    {
        Debug.Log("inside");
        Application.OpenURL("https://www.playtra.com/");
    }
}
