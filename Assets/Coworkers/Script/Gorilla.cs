using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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

		jokeR = new DialogChoices[] {
			new DialogChoices("Gorilla's freshly rennovated office reminds you of a hospital room, with it's crisp cold air and bleek colors."),
			new DialogChoices("Boss Gorilla is standing with their back towards you. As always, their presense is intimidating. They are grinning slightly."),
			new DialogChoices("Boss Gorilla smells like coconuts")
		};

		happyR = new DialogChoices[] {
			new DialogChoices("Boss Gorilla is standing with their back towards you. As always, their presense is intimidating."),
			new DialogChoices("Boss Gorillia does not recognize your face."),
			new DialogChoices("Boss Gorilla is drinking a fresh cup of coffee.")
		};

		sadR = new DialogChoices[] {
			new DialogChoices("You catch the Boss in the middle of reviwing reports. In swift motion, he strikes through paragraph after paragraph."),
			new DialogChoices("Boss is looking out the window."),
			new DialogChoices("Boss Gorilla smells of Bananas.")
		};
	}

	override protected void Death(){
		SceneManager.LoadScene("Level35.5");
	}
}
