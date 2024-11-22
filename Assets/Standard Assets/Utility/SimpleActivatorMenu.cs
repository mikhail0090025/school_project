using System;
using UnityEngine;
using UnityEngine.UI; // Required for the Text component

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // A simple menu which switches between referenced game objects in the scene.
        public Text camSwitchButton; // Updated from GUIText to Text
        public GameObject[] objects;

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            // Active object starts from the first in the array
            m_CurrentActiveObject = 0;
            UpdateButtonText();
        }

        public void NextCamera()
        {
            int nextActiveObject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            m_CurrentActiveObject = nextActiveObject;
            UpdateButtonText();
        }

        private void UpdateButtonText()
        {
            if (camSwitchButton != null && objects.Length > 0)
            {
                camSwitchButton.text = objects[m_CurrentActiveObject].name;
            }
        }
    }
}
