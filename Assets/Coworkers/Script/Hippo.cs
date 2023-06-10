using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hippo : BaseCoworker
{
    new public string[] GetInteraction(int index)
    {
        if (index == 0) {
            string[] dialog = {
                "I am a happy hippo",
                "I am an angry hippo",
                "I am a hungry hippo",
            };
            return dialog;
        }
        else {
            string[] dialog = {
                "MISSING DIALOG",
                "MISSING DIALOG",
                "MISSING DIALOG",
            };
            return dialog;
        }
    }
}
