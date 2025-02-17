using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace ZSDefense
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject loadingScreen;
        [SerializeField]
        private TMP_Text loadingText;
        [SerializeField]
        private GameObject menuButtons;
        [SerializeField]
        private GameObject nameInputScreen;
        [SerializeField]
        private TMP_InputField nameInput;

        public void SetLoadingText(string text)
        {
            this.loadingText.text = text;
        }

        public void SetNameInput(string text)
        {
            this.nameInput.text = text;
        }

        public string GetNameInput()
        {
            return nameInput.text;
        }

        public void OpenMenu()
        {
            this.loadingScreen.SetActive(false);
            this.menuButtons.SetActive(true);
            this.nameInputScreen.SetActive(false);
        }

        public void OpenLoadingScreen()
        {
            this.loadingScreen.SetActive(true);
            this.menuButtons.SetActive(false);
            this.nameInputScreen.SetActive(false);
        }

        public void OpenNameInputScreenScreen()
        {
            this.loadingScreen.SetActive(false);
            this.menuButtons.SetActive(false);
            this.nameInputScreen.SetActive(true);
        }
    }
}