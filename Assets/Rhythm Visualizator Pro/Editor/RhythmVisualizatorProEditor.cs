// Created by Carlos Arturo Rodriguez Silva (Legend)
// Contact: carlosarturors@gmail.com
// Thread: http://forum.unity3d.com/threads/rhythm-visualizator.423168/ 
// Video: https://www.youtube.com/watch?v=i5uRU45fi8U

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(RhythmVisualizatorPro))]
public class RhythmVisualizatorEditor : Editor {
	public override void OnInspectorGUI () {
		var rhythmVisualizator = (RhythmVisualizatorPro)target;

		if (GUILayout.Button ("Restart Script")) {
			rhythmVisualizator.Restart ();
		} else if (EditorApplication.isPlaying) {
			if (DrawDefaultInspector ()) {
				rhythmVisualizator.UpdateVisualizations ();
			}

		} else {
			DrawDefaultInspector ();
		}

		if (GUILayout.Button ("Restart Script")) {
			rhythmVisualizator.Restart ();
		}

	}
}
#endif