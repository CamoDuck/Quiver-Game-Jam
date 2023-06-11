using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reaction {Joke, Happy, Sad, None}

public class DialogChoices
{
    public string text;
    public Reaction reaction; // how the coworker will react if the if this dialog is chosen

    public DialogChoices(string text) {
        this.text = text;
        this.reaction = Reaction.None;
    }


}
