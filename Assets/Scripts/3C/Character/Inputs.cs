using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Inputs
    {
        [SerializeField] public string Vertical = "Vertical";
        [SerializeField] public string Horizontal = "Horizontal";
        [SerializeField] public string Yaw = "Yaw";
        [SerializeField] public string Pitch = "Pitch";
        [SerializeField] public string Jump = "Jump";
        [SerializeField] public string Sprint = "Sprint";
    }
}
