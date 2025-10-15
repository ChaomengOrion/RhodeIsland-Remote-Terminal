// Created by Carlos Arturo Rodriguez Silva (Legend)
// Video: https://www.youtube.com/watch?v=LXYWPNltY0s
// Contact: carlosarturors@gmail.com

// Rhythm Visualizator PRO //

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class RhythmVisualizatorPro : MonoBehaviour {

	#region Variables

	public GameObject soundBarPrefabCenter;
    public GameObject soundBarPrefabDownside;
    public Transform soundBarsTransform;

	[Header ("AUDIO SETTINGS")]
	public bool listenAllSounds;
	public AudioSource audioSource;

    [Space(5)]

    [Header("SOUND BARS [Requires Restart]")]


	[Range (32, 256)]
    public int soundBarsQuantity = 100;

    List<GameObject> soundBars = new List<GameObject>();

    int usedSoundBars = 100;

	public enum ScaleFrom
	{
		Center,
		Downside};

	public ScaleFrom scaleFrom = ScaleFrom.Downside;

	[Range (0.1f, 20f)] 
	public float soundBarsWidth = 3f;
    [Range(0.1f, 20f)]
    public float soundBarsDepth = 1f;

    [Header ("CAMERA")]
	public Transform center;

	public bool cameraControl = true;
	public bool rotateCamera = true;

    public bool useDefaultValues = true;

	[Range (-35, 35)]
	public float velocity = 15f;
	[Range (0, 200f)]
	public float height = 40f;

	[Range (0, 500)]
	public float orbitDistance = 300f;

	[Range (1, 179)]
	public int fieldOfView = 60;

    [Header("VISUALIZATION")]
    public bool mirror = true;
	public bool scaleByRhythm = false;


	[Range (10, 200f)]
	public float length = 65f;

	public enum Visualizations
	{
		Line,
		Circle,
		ExpansibleCircle,
		Sphere,
        Square}

	;

	public Visualizations visualization = Visualizations.Line;

	[Range (1f, 100f)]
	public float extraScaleVelocity = 50f;

	[Header ("LEVELS")]
	[Range (0.75f, 15f)]
	public float globalScale = 4f;

    [Range(0f, 5f)]
    public float minHeight = 1.5f;

    [Range (1, 15)]
	public int smoothVelocity = 3;

	public enum Channels
	{
		n512,
		n1024,
		n2048,
		n4096,
		n8192}

	;

	public Channels channels = Channels.n4096;
	public FFTWindow method = FFTWindow.Blackman;
	int channelValue = 2048;

	[Header ("RHYTHM DETECTION [Experimental]")]
	public ParticleSystem rhythmParticleSystem;

	public bool autoRhythmParticles = true;

	[Range (0f, 100f)]
	public float rhythmSensibility = 30;

	// Rhythm Minimum Sensibility. This don't need to change, use Rhythm Sensibility instead. Recommended: 1.5
	const float minRhythmSensibility = 1.5f;

	[Range (1, 150)]
	public int amountToEmit = 100;

	[Range (0.01f, 1f)]
	public float rhythmParticlesMaxInterval = 0.25f;

	float remainingRhythmParticlesTime;
	bool rhythmSurpassed = false;

	[Header ("BASS / MIRROR SETTINGS")]
	[Range (1f, 300f)]
	public float bassSensibility = 60f;
	[Range (0.5f, 2f)]
	public float bassHeight = 1.5f;
	[Range (1, 5)]
	public int bassHorizontalScale = 1;
	[Range (0, 256)]
	public int bassOffset = 0;

	[Header ("TREBLE SETTINGS")]
	[Range (1f, 300f)]
	public float trebleSensibility = 120f;
	[Range (0.5f, 2f)]
	public float trebleHeight = 1.35f;
	[Range (1, 5)]
	public int trebleHorizontalScale = 3;
	[Range (0, 256)]
	public int trebleOffset = 40;

	[Header ("APPEARANCE")]
	public bool soundBarsParticles = true;

	[Range (0f, 0.1f)]
	public float particlesMaxInterval = 0.02f;

	float remainingParticlesTime;
	bool surpassed = false;

	[Range (0.1f, 2f)]
	public float minParticleSensibility = 1.3f;
	public bool lerpColor = true;
	public Color [] colors = new Color[9];

	[Range (0.1f, 5f)]
	public float colorIntervalTime = 3f;

	[Range (0.1f, 5f)]
	public float colorLerpTime = 2f;

	public bool useGradient = false;
	public Gradient gradient;
	public Color rhythmParticleSystemColor = Color.white;



	[Header ("RAYS [Requires Restart]")]

	[Range (0f, 2f)]
	public float raysLength = 0.7f;

	[Range (0f, 1f)]
	public float raysAlpha = 0.8f;

	int posColor;

	[HideInInspector]
	public Color actualColor;

	Vector3 prevLeftScale;
	Vector3 prevRightScale;

	Vector3 rightScale;
	Vector3 leftScale;

	float timeChange;

	int halfBarsValue;

	int visualizationNumber = 1;

	float newLeftScale;

	float newRightScale;

	float rhythmAverage;

	Visualizations lastVisualizationForm = Visualizations.Line;

	#endregion

	#region Extra

	/// <summary>
	/// Emits particles if there are rhythm.
	/// </summary>
	public void EmitIfThereAreRhythm () {
		float [] spectrumLeftData;
		float [] spectrumRightData;

		#pragma warning disable 618
		spectrumLeftData = audioSource.GetSpectrumData (channelValue, 0, method);
		spectrumRightData = audioSource.GetSpectrumData (channelValue, 1, method);
		#pragma warning restore 618
	
		int count = 0;
		float spectrumSum = 0;

		// Using bass data only
		for (int i = 0; i < 40; i++) {
			spectrumSum += Mathf.Max (spectrumLeftData [i], spectrumRightData [i]);
			count++;
		}

		rhythmAverage = (spectrumSum / count) * rhythmSensibility;

		// If the spectrum value exceeds the minimum 
		if (rhythmAverage >= minRhythmSensibility) {
			rhythmSurpassed = true;
		}

		// Auto Rhythm Particles
		if (autoRhythmParticles) {
			if (rhythmSurpassed) {
				// Emit particles
				rhythmParticleSystem.Emit (amountToEmit);
			}
		}
	}

    #endregion

    bool visualizationsCanBeUpdated = false;

	#region Start

	public void Restart () {
		colorUpdated = false;
        visualizationsCanBeUpdated = false;

        if (soundBars.Count > 0) {
            for (int i = 0; i < soundBars.Count; i++) {
                DestroyImmediate(soundBars[i]);
            }
        }

		soundBars.Clear ();

		Application.targetFrameRate = 144;

		// Check the prefabs
		if ((soundBarPrefabCenter != null) && (soundBarPrefabDownside != null)) {

            if (soundBarsQuantity % 4 != 0) {
                soundBarsQuantity += soundBarsQuantity % 4;
            }

            if (soundBarsQuantity < 32) {
                soundBarsQuantity = 32;
            }
            else if (soundBarsQuantity > 256) {
                soundBarsQuantity = 256;
            }

			usedSoundBars = soundBarsQuantity;
			halfBarsValue = usedSoundBars / 2;

			CreateCubes ();

        } else {
			Debug.LogWarning ("Please assign Sound Bar Prefabs to the script");
			enabled = false;
		}

	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		Restart ();
	}

	/// <summary>
	/// Creates the cubes.
	/// </summary>
	void CreateCubes () {

		float newRayScale = (raysLength * 5);
		float newWidth = soundBarsWidth - 1;
		GameObject soundBarToInstantiate;

		if (scaleFrom == ScaleFrom.Center) {
			soundBarToInstantiate = soundBarPrefabCenter;
			newWidth = (newWidth / 2f) - 0.5f;
		} else {
			soundBarToInstantiate = soundBarPrefabDownside;
		}

		for (int i = 0; i < usedSoundBars; i++) { 
			
			var clone = Instantiate (soundBarToInstantiate, transform.position, Quaternion.identity) as GameObject;
			clone.transform.SetParent (soundBarsTransform.transform);
			clone.GetComponent<SoundBar> ().cube.transform.localScale = new Vector3 (soundBarsWidth, 1, soundBarsDepth);

			clone.name = string.Format ("SoundBar {0}", i + 1);

			var renderers = clone.GetComponentsInChildren<Renderer> ();

			Color newColor = colors [0];
			Color newColor2 = newColor;
			newColor2.a = raysAlpha;

			if (useGradient) {
				newColor = gradient.Evaluate (((float)(i + 1) / (float)usedSoundBars));
		

				var rhythmParticleS = rhythmParticleSystem.main;
				rhythmParticleS.startColor = rhythmParticleSystemColor;
			}

			foreach (Renderer rend in renderers) {
				rend.material.color = newColor;
			}

			var actualParticleSystem = clone.GetComponentInChildren<ParticleSystem> ().main;
			actualParticleSystem.startColor = newColor;

			clone.GetComponent<SoundBar> ().ray.material.SetColor ("_TintColor", newColor2);

			if (scaleFrom == ScaleFrom.Downside) {
				clone.GetComponent<SoundBar> ().ray.transform.localScale = new Vector3 (Mathf.Clamp (newWidth, 1, Mathf.Infinity), 1, raysLength);
				clone.GetComponent<SoundBar> ().ray.transform.localPosition = new Vector3 (0, newRayScale, 0);
			} else {
				clone.GetComponent<SoundBar> ().ray.transform.localScale = new Vector3 (Mathf.Clamp (newWidth, 0.5f, Mathf.Infinity), 1, raysLength);
			}

			soundBars.Add (clone);
		}

        visualizationsCanBeUpdated = true;
		UpdateVisualizations ();
	}

	/// <summary>
	/// Change to the next form. TRUE = Next, FALSE = PREVIOUS
	/// </summary>
	/// <param name="next">If set to <c>true</c> next.</param>
	public void NextForm (bool next) {
		if (next) {
			visualizationNumber++;
		} else {
			visualizationNumber--;
		}

		if (visualizationNumber > 5) {
			visualizationNumber = 1;
		} else if (visualizationNumber <= 0) {
			visualizationNumber = 5;
		}

		if (visualizationNumber == 1) {
			visualization = Visualizations.Line;
		} else if (visualizationNumber == 2) {
			visualization = Visualizations.Circle;
		} else if (visualizationNumber == 3) {
			visualization = Visualizations.ExpansibleCircle;
		} else if (visualizationNumber == 4) {
			visualization = Visualizations.Sphere;
		}

		UpdateVisualizations ();
	}

	/// <summary>
	/// Updates the channels of audio.
	/// </summary>
	void UpdateChannels () {
		if (channels == Channels.n512) {
			channelValue = 512;
		} else if (channels == Channels.n1024) {
			channelValue = 1024;
		} else if (channels == Channels.n2048) {
			channelValue = 2048;
		} else if (channels == Channels.n4096) {
			channelValue = 4096;
		} else if (channels == Channels.n8192) {
			channelValue = 8192;
		}
	}

	#endregion

	#region Camera


	/// <summary>
	/// Move Camera
	/// </summary>
	void CameraPosition () {
        if (visualization == Visualizations.Line) {
            Camera.main.fieldOfView = fieldOfView;
            var cameraPos = transform.position;
            cameraPos.z -= 170f;
            Camera.main.transform.position = cameraPos;
            cameraPos.y += 5f + height;
            Camera.main.transform.position = cameraPos;
            Camera.main.transform.LookAt(center);


        }
        else if (visualization == Visualizations.Circle) {
            Camera.main.fieldOfView = fieldOfView;
            var cameraPos = transform.position;
            cameraPos.y += ((1f + height) / 20f);
            cameraPos.z += 5f;
            Camera.main.transform.position = cameraPos;

            Camera.main.transform.LookAt(soundBarsTransform.position);

        }
        else if (visualization == Visualizations.ExpansibleCircle) {
            Camera.main.fieldOfView = fieldOfView;
            var cameraPos = transform.position;
            cameraPos.y += 55f;
            Camera.main.transform.position = cameraPos;
            Camera.main.transform.LookAt(soundBarsTransform.position);


        }
        else if (visualization == Visualizations.Sphere) {
            Camera.main.fieldOfView = fieldOfView;
            var cameraPos = transform.position;
            cameraPos.z -= 40f;
            cameraPos.y += 5f + height;

            Camera.main.transform.position = cameraPos;

            Camera.main.transform.LookAt(soundBarsTransform.position);
            Camera.main.transform.position = cameraPos;
        }
        else if (visualization == Visualizations.Square) {
            Camera.main.fieldOfView = fieldOfView;
            var cameraPos = transform.position;
            cameraPos.z -= 40f;
            cameraPos.y += 5f + height;

            Camera.main.transform.position = cameraPos;

            Camera.main.transform.LookAt(soundBarsTransform.position);
            Camera.main.transform.position = cameraPos;
        }

    }

    void SetVisualizationPredefinedValues()
    {
        if (visualization == Visualizations.Line) {
            scaleByRhythm = false;
            lerpColor = false;
            length = 65;
            height = 40;
            orbitDistance = 300;
        }
        else if (visualization == Visualizations.Circle) {
            scaleByRhythm = false;
            lerpColor = false;
            length = 125;
            height = 40;
            orbitDistance = 250;
        }
        else if (visualization == Visualizations.ExpansibleCircle) {
            scaleByRhythm = false;
            lerpColor = false;
            length = 100;
            height = 40;
            orbitDistance = 275;
        }
        else if (visualization == Visualizations.Sphere) {
            scaleByRhythm = true;
            lerpColor = true;
            length = 65;
            height = 15;
            orbitDistance = 220;
            Restart();
        } else if (visualization == Visualizations.Square) {
            scaleByRhythm = false;
            lerpColor = false;
            length = 35;
            height = 15;
            orbitDistance = 250;

            Restart();
        }
    }

    /// <summary>
    /// Camera Rotating Around Movement.
    /// </summary>
    void CameraMovement () {
		Camera.main.transform.position = center.position + (Camera.main.transform.position - center.position).normalized * orbitDistance;

		if (rotateCamera) {
			Camera.main.transform.RotateAround (center.position, Vector3.up, -velocity * Time.deltaTime);
		}
	}

	#endregion

	#region ColorLerp

	Color currentColor;

	/// <summary>
	/// Change SoundBars and Particles Color.
	/// </summary>
	void ChangeColor () {

		currentColor = soundBars [0].GetComponent<SoundBar> ().cube.material.color;

		actualColor = Color.Lerp (currentColor, colors [posColor], Time.deltaTime / colorLerpTime);

		foreach (GameObject cube in soundBars) {
			var newColor = actualColor;
			newColor.a = raysAlpha;
			cube.GetComponent<SoundBar> ().ray.material.SetColor ("_TintColor", newColor);
			cube.GetComponent<SoundBar> ().cube.material.color = actualColor;

			var ps = cube.GetComponent<SoundBar> ().pSystem.main;
			ps.startColor = actualColor;

			var actualParticleSystem = rhythmParticleSystem.main;
			actualParticleSystem.startColor = actualColor;
		}
	}

	/// <summary>
	/// Change SoundBars and Particles Color Helper.
	/// </summary>
	void NextColor () {

		timeChange = colorIntervalTime;
		lerpColor = false;

		if (posColor < colors.Length - 1) {
			posColor++;
		} else {
			posColor = 0;
		}
		lerpColor = true;
	}

	#endregion

	#region Visualizations

	/// <summary>
	/// Updates the visualizations.
	/// </summary>
	public void UpdateVisualizations () {
        if (!visualizationsCanBeUpdated) {
            return;
        }

        // Visualizations
        if (visualization == Visualizations.Circle) {
            for (int i = 0; i < usedSoundBars; i++) {
                float angle = i * Mathf.PI * 2f / usedSoundBars;
                Vector3 pos = soundBarsTransform.transform.localPosition;
                pos -= new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * length;
                soundBars[i].transform.localPosition = pos;
                soundBars[i].transform.LookAt(soundBarsTransform.position);

                var rot = soundBars[i].transform.eulerAngles;
                rot.x = 0;
                soundBars[i].transform.localEulerAngles = rot;
            }

        }
        else if (visualization == Visualizations.Line) {
            for (int i = 0; i < usedSoundBars; i++) {
                Vector3 pos = soundBarsTransform.transform.localPosition;
                pos.x -= length * 5;
                pos.x += (length / usedSoundBars) * (i * 10);

                soundBars[i].transform.localPosition = pos;
                soundBars[i].transform.localEulerAngles = Vector3.zero;
            }
        }
        else if (visualization == Visualizations.ExpansibleCircle) {
            for (int i = 0; i < usedSoundBars; i++) {
                float angle = i * Mathf.PI * 2f / usedSoundBars;
                Vector3 pos = soundBarsTransform.transform.localPosition;
                pos -= new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * length;
                soundBars[i].transform.localPosition = pos;
                soundBars[i].transform.LookAt(soundBarsTransform.position);

                var newRot = soundBars[i].transform.eulerAngles;
                newRot.x -= 90;

                soundBars[i].transform.eulerAngles = newRot;
            }

        }
        else if (visualization == Visualizations.Sphere) {

            var points = UniformPointsOnSphere(usedSoundBars, length);

            for (var i = 0; i < usedSoundBars; i++) {

                soundBars[i].transform.localPosition = points[i];

                soundBars[i].transform.LookAt(soundBarsTransform.position);

                var rot = soundBars[i].transform.eulerAngles;
                rot.x -= 90;

                soundBars[i].transform.eulerAngles = rot;
            }
        }
        else if (visualization == Visualizations.Square) {
            try {
                var points = UniformPointsOnSquare(length / 4f, usedSoundBars);

                for (var i = 0; i < usedSoundBars; i++) {
                    soundBars[i].transform.localPosition = points[i];
                }
            } catch { // Fix annoying error when you are changing the Soundbars quantity in playmode
                Restart();
            }

        }
			
		UpdateChannels ();

		if (cameraControl) {
			
			if (lastVisualizationForm != visualization) {
                lastVisualizationForm = visualization;

				if (visualization == Visualizations.Line) {
					visualizationNumber = 1;
				} else if (visualization == Visualizations.Circle) {
					visualizationNumber = 2;
				} else if (visualization == Visualizations.ExpansibleCircle) {
					visualizationNumber = 3;
				} else if (visualization == Visualizations.Sphere) {
					visualizationNumber = 4;
				} else if (visualization == Visualizations.Square) {
                    visualizationNumber = 5;
                }

				if (useDefaultValues) {
					SetVisualizationPredefinedValues ();
				}
			}

			CameraPosition ();

		}
	}

    Vector3 [] UniformPointsOnSquare (float separation, int size)
    {
        var points = new List<Vector3>();

        int realSize = size / 4;
        float otherSize = (realSize / 2f) * separation;
        float currentPos = (-otherSize) + 1;

        // Create top
        for (int i = 0; i < realSize; i++) {
            points.Add(new Vector3(currentPos, 0, otherSize));

            currentPos += separation;
        }

        currentPos = (otherSize) - 1;

        // Create right
        for (int i = 0; i < realSize; i++) {
            points.Add(new Vector3(otherSize, 0, currentPos));

            currentPos -= separation;
        }

        currentPos = (otherSize) - 1;

        // Create down from right
        for (int i = 0; i < realSize; i++) {
            points.Add(new Vector3(currentPos, 0, -otherSize));

            currentPos -= separation;
        }

        currentPos = (-otherSize) + 1;

        // Create left from down
        for (int i = 0; i < realSize; i++) {
            points.Add(new Vector3(-otherSize, 0, currentPos));

            currentPos += separation;
        }

        return points.ToArray();
    }

	/// <summary>
	/// Create a Sphere with the given verticles number.
	/// </summary>
	/// <returns>The points on sphere.</returns>
	/// <param name="verticlesNum">Verticles number.</param>
	/// <param name="scale">Scale.</param>
	Vector3[] UniformPointsOnSphere (float verticlesNum, float scale) {
		var points = new List<Vector3> ();
		var i = Mathf.PI * (3 - Mathf.Sqrt (5));
		var o = 2 / verticlesNum;
		for (var k = 0; k < verticlesNum; k++) {
			var y = k * o - 1 + (o / 2);
			var r = Mathf.Sqrt (1 - y * y);
			var phi = k * i;
			points.Add (new Vector3 (Mathf.Cos (phi) * r, y, Mathf.Sin (phi) * r) * scale);
		}
		return points.ToArray ();
	}

	#endregion

	bool colorUpdated = false;

	#region BaseScript

	/// <summary>
	/// Updates every frame this instance.
	/// </summary>
	void LateUpdate () {

		// Change Colors
		if (lerpColor) {
			timeChange -= Time.deltaTime;

			// When the counter are less than 0, change to the next Color
			if (timeChange < 0f) {
				NextColor ();
			}

			// Execute color lerping
			ChangeColor ();

			colorUpdated = false;
		} else {
			if (useGradient) {
				if (!colorUpdated) {


					for (int i = 0; i < soundBars.Count; i++) {
						var newColor = gradient.Evaluate (((float)(i + 1) / (float)usedSoundBars));

						soundBars[i].GetComponent<SoundBar> ().cube.material.color = newColor;

                        var actualParticleSystem = soundBars[i].GetComponent<SoundBar>().pSystem.main;
						actualParticleSystem.startColor = newColor;

						newColor.a = raysAlpha;

						soundBars[i].GetComponent<SoundBar> ().ray.material.SetColor ("_TintColor", newColor);

					}

					var actualRhythmPS = rhythmParticleSystem.main;
					actualRhythmPS.startColor = rhythmParticleSystemColor;

					colorUpdated = true;
				}
			} else {
				colorUpdated = false;
			}
		}

#pragma warning disable

        // Get Spectrum Data from Both Channels of audio
        float[] spectrumLeftData;
		float [] spectrumRightData;

        if (listenAllSounds) {
            // Get Spectrum Data from Both Channels of audio

            if (mirror) {  // MIRROR MODE
                spectrumLeftData = AudioListener.GetSpectrumData(channelValue, 0, method);
                spectrumRightData = AudioListener.GetSpectrumData(channelValue, 0, method);
            } else { // NORMAL MODE
                spectrumLeftData = AudioListener.GetSpectrumData(channelValue, 0, method);
                spectrumRightData = AudioListener.GetSpectrumData(channelValue, 1, method);
            }
        }
        else {
            if (audioSource == null) {
                Debug.LogWarning("No AudioSource detected 'Listen All Sounds' activated");
                listenAllSounds = true;
                return;
            }

            if (mirror) {  // MIRROR MODE
                spectrumLeftData = audioSource.GetSpectrumData(channelValue, 0, method);
                spectrumRightData = audioSource.GetSpectrumData(channelValue, 0, method);
            }
            else { // NORMAL MODE
                spectrumLeftData = audioSource.GetSpectrumData(channelValue, 0, method);
                spectrumRightData = audioSource.GetSpectrumData(channelValue, 1, method);
            }
        }
#pragma warning restore


        // Wait for Rhythm Particles Interval (for performance)
        if (remainingRhythmParticlesTime <= 0) {
			
			int count = 0;
			float spectrumSum = 0;

			// Using bass data only
			for (int i = 0; i < 40; i++) {
				spectrumSum += Mathf.Max (spectrumLeftData [i], spectrumRightData [i]);
				count++;
			}

			rhythmAverage = (spectrumSum / count) * rhythmSensibility;


			// If the spectrum value exceeds the minimum 
			if (rhythmAverage >= minRhythmSensibility) {
				rhythmSurpassed = true;
			}

			// Auto Rhythm Particles
			if (autoRhythmParticles) {
				if (rhythmSurpassed) {
					// Emit particles
					rhythmParticleSystem.Emit (amountToEmit);
				}
			}
		}
			

		// Scale SoundBars Normally
		if (!scaleByRhythm) {

            // SoundBars for Left Channel and Right Channel
            for (int i = 0; i < halfBarsValue; i++) {

                // Apply Off-Sets to get the AudioSpectrum
                int spectrumLeft = i * bassHorizontalScale + bassOffset;
                int spectrumRight = i * trebleHorizontalScale + trebleOffset;

                float spectrumLeftValue = 0;
                float spectrumRightValue = 0;

                // Get Actual Scale from SoundBar in "i" position (MIRROR MODE)
                if (mirror) {
                    prevLeftScale = soundBars[(halfBarsValue - 1 - i)].transform.localScale;
                    prevRightScale = soundBars[i + halfBarsValue].transform.localScale;

                    spectrumLeftValue = spectrumLeftData[spectrumLeft] * bassSensibility;
                    spectrumRightValue = spectrumLeftValue;
                } else {
                    // Get Actual Scale from SoundBar in "i" position (NORMAL MODE)
                    prevLeftScale = soundBars [i].transform.localScale;
                    prevRightScale = soundBars [i + halfBarsValue].transform.localScale;

                    spectrumLeftValue = spectrumLeftData[spectrumLeft] * bassSensibility;
                    spectrumRightValue = spectrumRightData[spectrumRight] * trebleSensibility;
                }

				// Left Channel //

				// Apply scale to that SoundBar using Lerp
				newLeftScale = Mathf.Lerp (prevLeftScale.y,
				                           spectrumLeftValue * bassHeight * globalScale,
				                           Time.deltaTime * extraScaleVelocity);

				// If the New Scale is greater than Previous Scale, set the New Value
				if (newLeftScale >= prevLeftScale.y) {
					prevLeftScale.y = newLeftScale;
					leftScale = prevLeftScale;

//					leftScale.y = Mathf.Lerp (soundBars [i].transform.localScale.y, newLeftScale, Time.deltaTime * smoothVelocity * 10);
				} else { // Else, Lerp to MinYValue
					leftScale = prevLeftScale;
					leftScale.y = Mathf.Lerp (prevLeftScale.y, minHeight, Time.deltaTime * smoothVelocity);
				}

                // Set new scale (MIRROR MODE)
                if (mirror) {
                    EmitParticle((halfBarsValue - 1 - i), spectrumLeftValue);
                    soundBars[(halfBarsValue - 1 - i)].transform.localScale = leftScale;
                } else {
                    // Set new scale (NORMAL MODE)
                    EmitParticle(i, spectrumLeftValue);
                    soundBars[i].transform.localScale = leftScale;
                }

                // Right Channel //

                if (mirror) {
                    newRightScale = newLeftScale;
                }
                else {
                    // Apply scale to that SoundBar using Lerp
                    newRightScale = Mathf.Lerp(prevRightScale.y,
                                                spectrumRightValue * trebleHeight * globalScale,
                                                Time.deltaTime * extraScaleVelocity);
                }

				// If the New Scale is greater than Previous Scale, set the New Value
				if (newRightScale >= prevRightScale.y) {
					prevRightScale.y = newRightScale;
					rightScale = prevRightScale;
//					rightScale.y = Mathf.Lerp (soundBars [i].transform.localScale.y, newRightScale.y, Time.deltaTime * smoothVelocity * 10);

				} else { // Else, Lerp to MinY
					rightScale = prevRightScale;
					rightScale.y = Mathf.Lerp (prevRightScale.y, minHeight, Time.deltaTime * smoothVelocity);
				}

                // Set new scale
                EmitParticle(i + halfBarsValue, spectrumRightValue);
                soundBars[i + halfBarsValue].transform.localScale = rightScale;

			}

		} else { // Scale All SoundBars by Rhythm

			for (int i = 0; i < usedSoundBars; i++) {
				
				prevLeftScale = soundBars [i].transform.localScale;

				// If Minimum Particle Sensibility is exceeded (volume is clamped beetween 0.01 and 1 to avoid 0)
				if (rhythmSurpassed) {

					// Apply extra scale to that SoundBar using Lerp
					newLeftScale = Mathf.Lerp (prevLeftScale.y,
					                           rhythmAverage * bassHeight * globalScale,
					                           Time.deltaTime * smoothVelocity);

					// If the Particles are activated, emit a particle too
					if (soundBarsParticles) {
						if (remainingParticlesTime <= 0f) {
							soundBars [i].GetComponentInChildren<ParticleSystem> ().Play ();

							surpassed = true;
						}
					}

				} else { 	// Else, Lerp to the previous scale
					newLeftScale = Mathf.Lerp (prevLeftScale.y,
					                           rhythmAverage * globalScale,
					                           Time.deltaTime * extraScaleVelocity);
				}

				// If the New Scale is greater than Previous Scale, set the New Value
				if (newLeftScale >= prevLeftScale.y) {
					prevLeftScale.y = newLeftScale;
					rightScale = prevLeftScale;
				} else { // Else, Lerp to 0.1
					rightScale = prevLeftScale;
					rightScale.y = Mathf.Lerp (prevLeftScale.y, minHeight, Time.deltaTime * smoothVelocity);
				}

				// Set new scale
				soundBars [i].transform.localScale = rightScale;
			
			}
			

		}

		// Particles Interval Reset
		if (soundBarsParticles) {
			if (surpassed) {
				surpassed = false;
				remainingParticlesTime = particlesMaxInterval;
			} else {
				remainingParticlesTime -= Time.deltaTime;
			}
		}
			
		// Rhythm Interval Reset
		if (rhythmSurpassed) {
			rhythmSurpassed = false;
			remainingRhythmParticlesTime = rhythmParticlesMaxInterval;
		} else {
			remainingRhythmParticlesTime -= Time.deltaTime;
		}

		// Execute Camera Control
		if (cameraControl) {
			CameraMovement ();
		}
	}

	void EmitParticle (int index, float spectrumValue) {
		// If the Particles are activated, emit a particle too
		if (soundBarsParticles) {

			if (spectrumValue >= minParticleSensibility) {
				if (remainingParticlesTime <= 0f) {

					soundBars [index].GetComponentInChildren<ParticleSystem> ().Emit (1);

					surpassed = true;
				}
			}
		}
	}

	#endregion

}