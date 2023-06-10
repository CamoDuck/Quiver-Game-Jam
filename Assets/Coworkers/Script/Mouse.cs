using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mouse : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("Are you enjoying your new job? Not too cheesy, I hope."),
			new DialogChoices("When I was 18 years old, I took an internship here too. They asked me if I had 20+ years of experience."), 
			new DialogChoices("What do you call cheese that isn't yours? Nacho Cheese!")

		};
		sad = new DialogChoices[] {
			new DialogChoices("That makes one of us"),
			new DialogChoices("How are your tasks for today going?"),
			new DialogChoices("Might wanna get back to it."),
			new DialogChoices("Don't worry, you'll get there. It's the usual rat race."),
			new DialogChoices("My first job was to serve coffee too.")
		};
		happy = new DialogChoices[] {
			new DialogChoices("Have fun with it. It will pay off eventually!"),
			new DialogChoices("I'm rooting for you! :)"),
			new DialogChoices("They didn't offer me much cheddar when I was an intern, but it was a good start for me.")
		};
    }
    
}
