using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZSDefense
{
    public class TextFPS : MonoBehaviour
    {
        private Text textFPS;

        private void Awake()
        {
            this.textFPS = GetComponent<Text>();

            InvokeRepeating(nameof(UpdateFPS), 0f, 1f);

            // Set frame rate to the default
            // Application.targetFrameRate = -1;

            // Set target FPS in game
            // Application.targetFrameRate = 120;
        }

        private void UpdateFPS()
        {
            float fps = 1 / Time.deltaTime;
            this.textFPS.text = fps.ToString("F2");
        }
    }
}