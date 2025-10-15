// Created by ChaomengOrion
// Create at 2022-07-25 14:16:18
// Last modified on 2022-07-31 01:28:24

using UnityEngine;
using UnityEngine.UI;

namespace RhodeIsland.RemoteTerminal.UI
{
    public class ChangeableMask : Mask
    {
        public Material ModifiedMaterial
        {
            get
            {
                if (!m_modifiedMat)
                {
                    Material mat = base.GetModifiedMaterial(GetComponent<Graphic>().material);
                    m_modifiedMat = mat;
                }
                return m_modifiedMat;
            }
        }

        public System.Action<Material> onChange = null;

        private Material m_modifiedMat = null;

        public override Material GetModifiedMaterial(Material baseMaterial)
        {
            m_modifiedMat = base.GetModifiedMaterial(baseMaterial);
            onChange?.Invoke(m_modifiedMat);
            return m_modifiedMat;
        }
    }
}
