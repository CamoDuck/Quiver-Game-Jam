using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hippo : BaseCoworker
{
    void Awake() {
        dialog = 
        new DialogChoices("Start",
            new DialogChoices("I like your style Mr.Hippo",
                new DialogChoices("wow"),
                new DialogChoices("cool"),
                new DialogChoices("awesome")
            ), 
            new DialogChoices("Did you know bread tastes like chicken"), 
            new DialogChoices("Wanna hear a knock knock joke?")
        );
    }
    
}
