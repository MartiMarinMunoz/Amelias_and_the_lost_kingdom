using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imageMute;

    void Start()
    {
        slider.value = State.volume;
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float valor)
    {
        State.volume = valor;
        sliderValue = State.volume;
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }
    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}

public class State
{
    public static float volume = 0.5f;
    public static Vector2 positionCheck;
    public static Quaternion rotationCheck;
    public static bool checkPlayer = false;

}
