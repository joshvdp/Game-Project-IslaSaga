using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Controls
{
    [CreateAssetMenu(fileName = "Control Binding", menuName = "Control Binding")]
    public class ControlBindings : ScriptableObject
    {
        public KeyCode SprintKey;

        public KeyCode Attack1Key;
        public KeyCode ShieldKey;


        public KeyCode ForwardKey;
        public KeyCode BackwardKey;
        public KeyCode LeftKey;
        public KeyCode RightKey;


        public KeyCode PickUpKey;

        public KeyCode CameraRotateKey;

    }
}

