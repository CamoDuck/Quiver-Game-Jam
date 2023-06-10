using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mouse : BaseCoworker
{
    void Awake() {
        DialogChoices[] joke = {
			DialogChoices("Are you enjoying your new job? Not too cheesy, I hope."),
			DialogChoices("When I was 18 years old, I took an internship here too. They asked me if I had 20+ years of experience."), 
			DialogChoices("What do you call cheese that isn't yours? Nacho Cheese!")

		};
		DialogChoices[] sad = {
			DialogChoices("That makes one of us"),
			DialogChoices("How are your tasks for today going?"),
			DialogChoices("Might wanna get back to it."),
			DialogChoices("Don't worry, you'll get there. It's the usual rat race."),
			DialogChoices("My first job was to serve coffee too.")
		};
		DialogChoices[] happy = {
			DialogChoices("Have fun with it. It will pay off eventually!"),
			DialogChoices("I'm rooting for you! :)"),
			DialogChoices("They didn't offer me much cheddar when I was an intern, but it was a good start for me.")
		};
    }
    
}
