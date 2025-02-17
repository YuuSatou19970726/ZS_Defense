using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSDefense
{
    public class CameraMotor : MonoBehaviour
    {
        private Transform lookAt;

        private Vector3 desiredPosition;
        private Vector3 offset;

        private float smoothSpeed = 7.5f;
        private float distance = 9.0f;
        private float yOffset = 10.0f;

        private void Start()
        {
            this.offset = new Vector3(0, yOffset, -1f * distance);
        }

        private void FixedUpdate()
        {
            if (this.lookAt == null) return;
            this.desiredPosition = this.lookAt.position + this.offset;
            transform.position = Vector3.Lerp(transform.position, this.desiredPosition, this.smoothSpeed * Time.deltaTime);
            transform.LookAt(lookAt.position + Vector3.up);
        }

        public void LoadTransforms()
        {
            this.lookAt = GameObject.FindWithTag(Tags.PLAYER).transform;
        }
    }
}