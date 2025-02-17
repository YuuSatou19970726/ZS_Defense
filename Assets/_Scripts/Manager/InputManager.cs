using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSDefense
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        public static InputManager Instance => instance;

        private float xDirection;
        public float XDirection => xDirection;
        private float yDirection;
        public float YDirection => yDirection;
        private bool checkedJump;
        public bool CheckedJump => checkedJump;
        private bool checkedLeftClick;
        public bool CheckedLeftClick => checkedLeftClick;

        private bool checkedProcessInputs = false;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void Update()
        {
            if (this.checkedProcessInputs)
                this.GetDirection();
        }

        public void SetProcessInputs(bool isCheck)
        {
            this.checkedProcessInputs = isCheck;
        }

        private void GetDirection()
        {
            xDirection = Input.GetAxis(AxisTags.HORIZONTAL);
            yDirection = Input.GetAxis(AxisTags.VERTICAL);
            checkedJump = Input.GetButton(GetButtonTags.BUTTON_JUMP);
            checkedLeftClick = Input.GetButton(GetButtonTags.BUTTON_FIRE_1);
        }
    }
}