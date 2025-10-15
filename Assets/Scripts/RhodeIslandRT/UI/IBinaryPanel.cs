// Created by ChaomengOrion
// Create at 2022-06-05 15:50:49
// Last modified on 2022-06-05 15:53:15

using System;

namespace RhodeIsland.RemoteTerminal.UI
{
    public interface IBinaryPanel
    { 
        public void Init(string message, Action<bool> onClick);
    }
}