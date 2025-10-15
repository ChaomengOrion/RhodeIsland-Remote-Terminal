// Created by ChaomengOrion
// Create at 2022-07-26 12:07:54
// Last modified on 2022-07-26 12:14:07

using UnityEngine;

namespace RhodeIsland.RemoteTerminal
{
    [CreateAssetMenu(fileName = "ShaderHub", menuName = "RhodeIsland/ShaderHub")]
    public class ShaderHub : SingletonScriptableObject<ShaderHub>
    {
        public Shader splitShader;
        public Shader avgSplitShader;
    }
}