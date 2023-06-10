using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gorilla : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("I would never fight a gorilla. I heard you know King Kong Fu."),
			new DialogChoices("What does a gorilla wear when cooking?... An ape-ron."),
			new DialogChoices("Next week we have a company ice-cream day. Hope they have Chocolate-Chimp.")
		};
		sad = new DialogChoices[] {
			new DialogChoices("Some customers issued complaints about our product pealing experience."),
			new DialogChoices("Our banana stock dropped slightly this week.")
		};
		happy = new DialogChoices[] {
			new DialogChoices("Gorillaz is my favorite band. Have you heard of it?"),
			new DialogChoices("In this company, we make bananas, we don't go bananas.")
			
		};
    }
    
}
