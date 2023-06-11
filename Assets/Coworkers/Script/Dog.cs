using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dog : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("What does a dog and a phone have in common?... They both have collar ID."),
			new DialogChoices("You're an amazing man, Jerry. You deserve a round of a-paws!"),
			new DialogChoices("")
		};
		sad = new DialogChoices[] {
			new DialogChoices("Our customers might have a bone to pick with us."),
			new DialogChoices("Unfortunately my quota this week was more than I could chew."),
			new DialogChoices("")
		};
		happy = new DialogChoices[] {
			new DialogChoices("I have a few ideas for how we can be more efficient chasing our goals."),
			new DialogChoices("The weather is pretty nice for a walk today! :)"),
			new DialogChoices("")
		};
    }
    
}
