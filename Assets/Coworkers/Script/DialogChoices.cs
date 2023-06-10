using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogChoices
{
    public string text;
    public DialogChoices nextFirst;
    public DialogChoices nextSecond;
    public DialogChoices nextThird;

    public DialogChoices(string text) {
        this.text = text;
        this.nextFirst = null;
        this.nextSecond = null;
        this.nextThird = null;
    }

    public DialogChoices(string text, DialogChoices nextFirst, DialogChoices nextSecond, DialogChoices nextThird) {

        this.text = text;
        this.nextFirst = nextFirst;
        this.nextSecond = nextSecond;
        this.nextThird = nextThird;
    }

}
