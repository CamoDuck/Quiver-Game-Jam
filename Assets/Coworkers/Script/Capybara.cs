using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Capybara : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("How's the progress in the capybara department? I heard it's all high and no cap!"),
			new DialogChoices("How much bar can a capybara cap if a capybara can cap bar?")
		};
		sad = new DialogChoices[] {
			new DialogChoices("Your last presentation wasn't very high-bar."),
			new DialogChoices("I noticed eveyone comes to your department to use the Capier... I mean Copier."),
			new DialogChoices("Are you handling your work capacity?")
		};
		happy = new DialogChoices[] {
			new DialogChoices("I heard you were being considered for Emploee of the Month"),
			new DialogChoices("You always have original ideas. I guess Capybaras are not Copycats.")
		};


		jokeR = new DialogChoices[] {
			new DialogChoices("This Capybara employee is nodding in response to someone's cheerful laugh"),
			new DialogChoices("Capybara has a slight grin"),
			new DialogChoices("Capybara has a slighter grin.")
		};

		happyR = new DialogChoices[] {
			new DialogChoices("Capybara is in the midst of another conversation. Their face is as blank as it gets."),
			new DialogChoices("Capybara is chilling!"),
			new DialogChoices("Capybara is absorbing the good vibes")
		};

		sadR = new DialogChoices[] {
			new DialogChoices("Capybara is slowly moving through the crowd. Their step is heavy, posture slouched, paws hidden in the pockets."),
			new DialogChoices("Capybara is contemplating about the horrors of the world."),
			new DialogChoices("Capybara is thinking about pulling up.")
		};
	}
    
}
