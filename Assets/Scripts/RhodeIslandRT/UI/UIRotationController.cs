// Created by ChaomengOrion
// Create at 2022-07-21 00:20:07
// Last modified on 2022-07-21 12:32:29

using UnityEngine;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class UIRotationController : MonoBehaviour
    {
        [SerializeField]
        private float _simulateZ;

        protected UIRotationElement[] m_elements;

        protected void Awake()
        {
            m_elements = GetComponentsInChildren<UIRotationElement>(true);
        }

        protected void LateUpdate()
        {
            float x, y;
            Vector2 pos = new Vector3(Screen.width, Screen.height) / 2f - Input.mousePosition;
            x = Mathf.Atan2(pos.x, -_simulateZ) * Mathf.Rad2Deg;
            y = Mathf.Atan2(pos.y, -_simulateZ) * Mathf.Rad2Deg;
            foreach (UIRotationElement element in m_elements)
            {
                element.UpdateRotation(y, -x);
            }
        }
    }
}