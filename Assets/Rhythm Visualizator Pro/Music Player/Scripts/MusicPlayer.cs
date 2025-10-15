// Created by Carlos Arturo Rodriguez Silva (Legend)
// Video: https://www.youtube.com/watch?v=LXYWPNltY0s
// Contact: carlosarturors@gmail.com

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;
using ThridPart;

public class MusicPlayer : MonoBehaviour {

	[HideInInspector]
	public bool PrimaryMusicPlayer;

	public SongData actualSong;

	[Header ("Assignation")]
	public AudioSource audioSource;
	public Sprite playSprite;
	public Sprite pauseSprite;
	public InputField searchSong;

	[Header ("Music Player UI")]
	public Image playPauseButton;
	public Text songName;
	public Image playerBar;
	public Slider sliderBar;
	public Text actualTime;
	public Text totalTime;

	[Header ("Songs List")]
	public GameObject songList;

	public List<SongData> songs;
	public List<GameObject> songSearchList;

	[Header ("Song Selection")]
	public GameObject songPrefab;
	public GameObject songSelectionPanel;
	public Transform songsSelectionTransform;

	int actualPos = 0;

	float amount = 0f;
	bool playing;
	bool active;

	public bool animateSearch = true;
	public Animator contentAnim;

	public static MusicPlayer instance;

	void Awake () {

        if (audioSource == null) {
            audioSource = FindObjectOfType<RhythmVisualizatorPro>().audioSource;
        }

        if (audioSource == null) {
            Debug.Log("Assign an Audio Source to the Music Player script");
            enabled = false;
            return;
        }

        if (instance == null) {
			PrimaryMusicPlayer = true;
			instance = this;
		} else {
			PrimaryMusicPlayer = false;
			return;
		}

		var songsDataList = songList.GetComponentsInChildren<SongData> ();

		foreach (SongData song in songsDataList) {
			songs.Add (song);
		}

		active = false;
		playing = false;

		CreateSongSelectionList ();

		actualPos = UnityEngine.Random.Range (0, songSearchList.Count - 1);
		ChangeSong (actualPos);
	}

	/// <summary>
	/// Creates the song selection list.
	/// </summary>
	void CreateSongSelectionList () {
		var pos = 0;

		foreach (SongData song in songs) {

			// Create a new clone
			var clone = Instantiate (songPrefab, transform);

			// Assigns Name to clone
			clone.name = string.Format ("{0} - {1}", song.artist, song.title);

			// Calculate duration to show in 00:00 format
			var totalSeconds = song.songClip.length;
			int seconds = (int)(totalSeconds % 60f); 
			int minutes = (int)((totalSeconds / 60f) % 60f); 

			// Show Song Data
			clone.GetComponent<ActivateSong> ().ShowSongData (song.artist, song.title, pos, minutes, seconds);

			clone.transform.SetParent (songsSelectionTransform);

			// Change scale
			var rectTransform = clone.GetComponent<RectTransform> ();
			rectTransform.localPosition = Vector3.zero;
			rectTransform.localScale = Vector3.one;

			// Add to list
			songSearchList.Insert (songSearchList.Count, clone);
			pos++;
		}
	}

	/// <summary>
	/// Searchs song by name.
	/// </summary>
	public void SearchSongByName () {
		var nameToSearch = searchSong.text;

		foreach (GameObject s in songSearchList) {
			if (System.Text.RegularExpressions.Regex.IsMatch (s.name, nameToSearch, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
				s.SetActive (true);
			} else {
				s.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Orders the list.
	/// </summary>
	public void OrderList () {
		
		// Create ref to the comparer
		IComparer myComparer = new OrderSongs ();

		// Create an array with the songs
		GameObject [] gameobjects = new GameObject[songSearchList.Count];

		// Add songs to the array
		for (int i = 0; i < songSearchList.Count; i++) {
			gameobjects [i] = songSearchList [i];
		}

		// Order the array
		Array.Sort (gameobjects, myComparer);
	
		// Order the transform
		foreach (GameObject i in gameobjects) {
			i.transform.SetAsLastSibling ();
		}

		// Clear the previous list
		songSearchList.Clear ();

		// Add the new list
		for (int i = 0; i < gameobjects.Length; i++) {
			songSearchList.Insert (songSearchList.Count, gameobjects [i]);
		}
			
		// Update actual position
		for (int b = 0; b < songs.Count; b++) {
			if (songs [b] == actualSong) {
				actualPos = b;
				break;
			}
		}
	}

	/// <summary>
	/// Activates or deactivate the song selection list.
	/// </summary>
	public void ActivateOrDeactivate () {

		if (!gameObject.activeSelf) {
			return;
		}

		if (!songSelectionPanel.activeSelf) {
			StopCoroutine ("HideAnimate");
			contentAnim.SetBool ("Show", true);
			songSelectionPanel.SetActive (true);

		} else {
			if (animateSearch) {
				StartCoroutine (HideAnimation ());
			} else {
				songSelectionPanel.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Hide animation.
	/// </summary>
	IEnumerator HideAnimation () {
		if (contentAnim != null) {
			contentAnim.SetBool ("Show", false);
			yield return new WaitForSeconds (contentAnim.GetCurrentAnimatorStateInfo (0).length);
			songSelectionPanel.SetActive (false);
		} else {
			songSelectionPanel.SetActive (false);
		}
	}

	/// <summary>
	/// Sets to active the actual song image.
	/// </summary>
	void SetActiveSongImage () {
		foreach (GameObject i in songSearchList) {
			i.GetComponent<ActivateSong> ().activeSongImage.enabled = false;
		}
		songSearchList [actualPos].GetComponent<ActivateSong> ().activeSongImage.enabled = true;
	}

	/// <summary>
	/// Change the song using his id
	/// </summary>
	/// <param name="pos">Position.</param>
	public void ChangeSong (int pos) {
		if (!PrimaryMusicPlayer) {
			instance.ChangeSong (pos);
			return;
		}

		StopSong ();

		actualPos = pos;

		actualSong = songs [actualPos];

		songName.text = string.Format ("{0} - {1}", songs [pos].artist, songs [pos].title);

		PrepareToLoadSong (pos);

		SetActiveSongImage ();
	}

	void Update () {

		if (!PrimaryMusicPlayer) {
			playPauseButton.sprite = instance.playPauseButton.sprite;
			songName.text = instance.songName.text;
			actualTime.text = instance.actualTime.text;
			totalTime.text = instance.totalTime.text;
			playerBar.fillAmount = instance.playerBar.fillAmount;
			actualSong = instance.actualSong;
			active = instance.active;
			playing = instance.playing;
			amount = instance.amount;
			actualPos = instance.actualPos;
			songSearchList = instance.songSearchList;
			songs = instance.songs;
			return;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			PlayOrPauseSong ();
		} else if (Input.GetKeyDown (KeyCode.Backspace)) {
			StopSong ();
		}

		if (active) {
			if (playing) {
				if (audioSource.isPlaying) {
					amount = (audioSource.time / audioSource.clip.length);
					playerBar.fillAmount = amount;

					CalculateActualTime ();
		
				} else {
					active = false;
					playing = false;
					NextSong ();
				}
			}
		}
	}

	void CalculateActualTime () {
		// Calculate duration to show in 00:00 format
		var totalSeconds = audioSource.time;
		int seconds = (int)(totalSeconds % 60f); 
		int minutes = (int)((totalSeconds / 60f) % 60f); 

		actualTime.text = minutes + ":" + seconds.ToString ("D2");
	}

	/// <summary>
	/// Changes the playback position in the song.
	/// </summary>
	public void ChangePosition () {
		if (audioSource.clip != null) {
			active = false;
			audioSource.time = sliderBar.value * audioSource.clip.length;
			playerBar.fillAmount = sliderBar.value;
			active = true;

			CalculateActualTime ();
		}
	}

	/// <summary>
	/// Stops the song.
	/// </summary>
	public void StopSong () {
		if (!PrimaryMusicPlayer) {
			instance.StopSong ();
			return;
		}

		Debug.Log ("Stop");
		StopAllCoroutines ();
		active = false;
		playing = false;

		actualTime.text = "0:00";

		audioSource.Stop ();
		audioSource.time = 0;

		playPauseButton.sprite = playSprite;
		amount = 0f;
		sliderBar.value = 0f;
		playerBar.fillAmount = 0f;
	}

	/// <summary>
	/// Play or pause the song.
	/// </summary>
	public void PlayOrPauseSong () {
		if (!PrimaryMusicPlayer) {
			instance.PlayOrPauseSong ();
			return;
		}

		if (playing) {
			//	Debug.Log ("Pause");

			active = false;
			playing = false;
			audioSource.Pause ();
			playPauseButton.sprite = playSprite;

		} else {

			//	Debug.Log ("Play");

			audioSource.Play ();
			playPauseButton.sprite = pauseSprite;
			playing = true;
			active = true;
		}
	}

	/// <summary>
	/// Plays the next song.
	/// </summary>
	public void NextSong () {
		if (!PrimaryMusicPlayer) {
			instance.NextSong ();
			return;
		}

		++actualPos;

		if (actualPos > songSearchList.Count - 1) {
			actualPos = 0;
		}

		ChangeSong (actualPos);

		SetActiveSongImage ();
	}

	/// <summary>
	/// Plays the previous song.
	/// </summary>
	public void PreviousSong () {
		if (!PrimaryMusicPlayer) {
			instance.PreviousSong ();
			return;
		}

		--actualPos;

		if (actualPos < 0) {
			actualPos = songSearchList.Count - 1;
		}
						
		ChangeSong (actualPos);

		SetActiveSongImage ();
	}

	/// <summary>
	/// Prepares to load the song.
	/// </summary>
	/// <param name="pos">Position.</param>
	void PrepareToLoadSong (int pos) {
		StopCoroutine ("LoadSong");
		StartCoroutine (LoadSong (songs [pos]));
	}

	/// <summary>
	/// Loads the song.
	/// </summary>
	/// <returns>The song.</returns>
	/// <param name="song">Song.</param>
	IEnumerator LoadSong (SongData song) {

		// Rename the clip
		AudioClip a = song.songClip;
		a.name = song.artist + " - " + song.title;

		// Loads and wait for song load
		#pragma warning disable 618
		while (!a.isReadyToPlay)
			#pragma warning restore 618
		{
			//	Debug.Log("Loading Song...");
			yield return null; 
		}

		// Assign the clip, and play
		audioSource.clip = a;

		// Calculate duration to show in 00:00 format
		var totalSeconds = audioSource.clip.length;
		int seconds = (int)(totalSeconds % 60f); 
		int minutes = (int)((totalSeconds / 60f) % 60f); 

		totalTime.text = minutes + ":" + seconds.ToString ("D2");

		PlayOrPauseSong ();
	}
}

public class OrderSongs : IComparer {

	int IComparer.Compare (System.Object x, System.Object y) {
		return((new CaseInsensitiveComparer ()).Compare (((GameObject)x).name, ((GameObject)y).name));
	}
}
