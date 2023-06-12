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
    }
    
}
