using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gorilla : BaseCoworker
{
    void Awake() {
        DialogChoices[] joke = {
			DialogChoices("I would never fight a gorilla. I heard you know King Kong Fu."),
			DialogChoices("What does a gorilla wear when cooking?... An ape-ron."),
			DialogChoices("Next week we have a company ice-cream day. Hope they have Chocolate-Chimp.")
		};
		DialogChoices[] sad = {
			DialogChoices("Some customers issued complaints about our product pealing experience."),
			DialogChoices("Our banana stock dropped slightly this week."),
		};
		DialogChoices[] happy = {
			DialogChoices("Gorillaz is my favorite band. Have you heard of it?"),
			DialogChoices("In this company, we make bananas, we don't go bananas."),
			
		};
    }
    
}
