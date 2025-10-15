// Created by ChaomengOrion
// Create at 2022-08-14 23:00:07
// Last modified on 2022-08-15 12:55:22

using UnityEngine;
using UnityEngine.UI;
using VContainer;
using RhodeIsland.RemoteTerminal.UI.ScrollView;

namespace RhodeIsland.RemoteTerminal.UI.Audio
{
    public class MusicItem : MonoBehaviour, IAcceptFocus
    {
        [SerializeField]
        private Text _name;
        [SerializeField]
        private Text _author;

        private string m_id;

        public Vector3 GetLocalPos()
        {
            return transform.localPosition;
        }

        public object GetObject() => this;

        public string GetId() => m_id;

        public void OnDisFocus() { }

        public void OnDistanceChange(float x) { }

        public void OnDynamicFocus() { }

        public void OnFocus() { }

        public void Render(string id, string name, string author)
        {
            m_id = id;
            _name.text = name;
            _author.text = author;
        }
    }
}