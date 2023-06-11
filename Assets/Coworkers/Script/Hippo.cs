using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hippo : BaseCoworker
{
    void Awake() {
        joke = new DialogChoices[] {
			new DialogChoices("Here's to great leadership and avoiding any slips! Banana slips..."),
			new DialogChoices("The coconut market is booming right now. Do you think we should invest?")
		};
		happy = new DialogChoices[] {
            new DialogChoices("Well done on your recent investment in Palm Tree Inc. As a partner, they will help us make more bananas."),
			new DialogChoices("I heard Mr. Chimp is very happy, as our top investor!"),
			new DialogChoices("We ordered more banana pickers for our inventory. Productivity has increased by 10%.")
		};
		sad = new DialogChoices[] {
			new DialogChoices("The market is really changing right now. Thoughts?"),
			new DialogChoices("Thanks for buying us a new coffee machine for the cafeteria!"),
			new DialogChoices("Maybe our salaries will increase too. Wink wink ;)")	
		};


    }
    
}
