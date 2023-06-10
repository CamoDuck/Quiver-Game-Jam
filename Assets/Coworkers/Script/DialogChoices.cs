using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reaction {Happy, Sad, Neutral, None}

public class DialogChoices
{
    public string text;
    public Reaction reaction; // how the coworker will react if the if this dialog is chosen
    public DialogChoices nextFirst;
    public DialogChoices nextSecond;
    public DialogChoices nextThird;

    public DialogChoices(string text) {
        this.text = text;
        this.reaction = Reaction.None;
        this.nextFirst = null;
        this.nextSecond = null;
        this.nextThird = null;
    }

    public DialogChoices(string text, Reaction reaction, DialogChoices nextFirst, DialogChoices nextSecond, DialogChoices nextThird) {
        this.text = text;
        this.reaction = reaction;
        this.nextFirst = nextFirst;
        this.nextSecond = nextSecond;
        this.nextThird = nextThird;
    }

}
