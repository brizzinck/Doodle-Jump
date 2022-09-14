using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateController
{
    public static void Vibrate()
    {
        if (Saver.GetStringPrefs("MuteVibrate") == "False") Handheld.Vibrate();
    }
    public static void MuteVibrate()
    {
        bool mute = false;
        if (Saver.GetStringPrefs("MuteVibrate") == "True") mute = false;
        else if (Saver.GetStringPrefs("MuteVibrate") == "False") mute = true;
        Saver.SaveStringPrefs("MuteVibrate", mute.ToString());
    }
}
