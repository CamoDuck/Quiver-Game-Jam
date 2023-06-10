using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Capybara : BaseCoworker
{
    void Awake() {
        DialogChoices[] joke = {
			DialogChoices("How's the progress in the capybara department? I heard it's all high and no cap!"),
			DialogChoices("How much bar can a capybara cap if a capybara can cap bar?")
		};
		DialogChoices[] sad = {
			DialogChoices("Your last presentation wasn't very high-bar."),
			DialogChoices("I noticed eveyone comes to your department to use the Capier... I mean Copier."),
			DialogChoices("Are you handling your work capacity?")
		};
		DialogChoices[] happy = {
			DialogChoices("I heard you were being considered for Emploee of the Month"),
			DialogChoices("You always have original ideas. I guess Capybaras are not Copycats.")
		};
    }
    
}
