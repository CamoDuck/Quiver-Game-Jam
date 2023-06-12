using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dog : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("What does a dog and a phone have in common?... They both have collar ID."),
			new DialogChoices("Work must be ruff init?"),
			new DialogChoices("You are an amazing ,manager! You deserve a round of a-paws!")
		};
		sad = new DialogChoices[] {
			new DialogChoices("Our customers might have a bone to pick with us."),
			new DialogChoices("Unfortunately my quota this week was more than I could chew."),
			new DialogChoices("I'm sorry that you got demoted.")
		};
		happy = new DialogChoices[] {
			new DialogChoices("I have a few ideas for how we can be more efficient chasing our goals."),
			new DialogChoices("I love your outfit today!"),
			new DialogChoices("You have always been a role model for me.")
		};

		jokeR = new DialogChoices[] {
			new DialogChoices("This Dog is sipping their hot coffee while simultaneously browsing their phone."),
			new DialogChoices("Dog is wagging their tail!"),
			new DialogChoices("Dog is panting with enthusiasm.")
		};

		happyR = new DialogChoices[] {
			new DialogChoices("You recognize this Manager Dog and the photo of their newly born puppies they are shoving in their collegue's faces."),
			new DialogChoices("Dog had just gotten a promotion."),
			new DialogChoices("Dog is looking for an energy boost")
		};

		sadR = new DialogChoices[] {
			new DialogChoices("Dog can’t seem to scratch the itch behind their ear"),
			new DialogChoices("Dog wonders why all squares are rectangles but all rectangles aren’t squares."),
			new DialogChoices("Dog is thinking about the company’s poor performance")
		};
    }
    
}
