// Created by Carlos Arturo Rodriguez Silva (Legend)
// Video: https://www.youtube.com/watch?v=LXYWPNltY0s
// Contact: carlosarturors@gmail.com

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateSong : MonoBehaviour {

	[HideInInspector]
	public int pos = 0;

	public Text songName;
	public Text duration;
	public Image activeSongImage;


	MusicPlayer musicPlayer;

	void Awake () {
		musicPlayer = GetComponentInParent<MusicPlayer> ();
	}

	public void ChangeToThisSong () {
		musicPlayer.ChangeSong (pos);
	}

	public void ShowSongData (string _artist, string _songName, int _pos, int minutes, int seconds) {
		pos = _pos;
		duration.text = "Duration \n" + minutes + ":" + seconds.ToString ("D2");

		songName.text = _artist + " - " + _songName;

		activeSongImage.enabled = true;
	}
}
