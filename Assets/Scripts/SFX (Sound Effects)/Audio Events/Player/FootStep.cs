using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class FootStep : MonoBehaviour
    {
        public void Walk()
        {
            StepSound.walk?.Invoke();
        }

        public void Run()
        {
            StepSound.run?.Invoke();
        }
    }
}
