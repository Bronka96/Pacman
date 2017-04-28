using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour {

    public Sprite soundOff;
    public Sprite soundOn;
	public void SoundSwitch()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            gameObject.GetComponent<Image>().sprite = soundOn;
        }
        else
        {
            AudioListener.volume = 0;
            gameObject.GetComponent<Image>().sprite = soundOff;
        }
    }

}
