using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hippo : BaseCoworker
{
    void Awake() {
        DialogChoices[] joke = {
			DialogChoices("Here's to great leadership and avoiding any slips! Banana slips..."),
			DialogChoices("The coconut market is booming right now. Do you think we should invest?")
		};
		DialogChoices[] happy = {
            DialogChoices("Well done on your recent investment in Palm Tree Inc. As a partner, they will help us make more bananas."),
			DialogChoices("I heard Mr. Chimp is very happy, as our top investor!"),
			DialogChoices("We ordered more banana pickers for our inventory. Productivity has increased by 10%.")
		};
		DialogChoices[] sad = {
			DialogChoices("The market is really changing right now. Thoughts?"),
			DialogChoices("Thanks for buying us a new coffee machine for the cafeteria!"),
			DialogChoices("Maybe our salaries will increase too. Wink wink ;)")	
		};
    }
    
}
