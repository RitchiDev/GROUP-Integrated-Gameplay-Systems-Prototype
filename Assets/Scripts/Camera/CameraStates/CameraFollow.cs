using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using CameraSystem;

namespace CameraState
{
    [CreateAssetMenu(fileName = "Camera Follow State", menuName = "Camera System/States/Camera Follow State")]
    public class CameraFollow : BaseCameraState
    {
        public override Vector3 CalculateNewPosition(AreaCameraHandler _roomCamera)
        {
            Transform cameraTransform = _roomCamera.CameraTransform;
            Transform target = _roomCamera.Target;
            Vector3 followTreshold = _roomCamera.CalculateFollowTreshold();

            // xDifference should always be a positive number (Mathf.Abs)
            float xDifference = Vector2.Distance(Vector2.right * cameraTransform.position.x, Vector2.right * target.position.x);
            float yDifference = Vector2.Distance(Vector2.up * cameraTransform.position.y, Vector2.up * target.position.y);

            Vector3 newPosition = cameraTransform.position;

            if (Mathf.Abs(xDifference) >= followTreshold.x)
            {
                newPosition.x = target.position.x;
            }

            if (Mathf.Abs(yDifference) >= followTreshold.y)
            {
                newPosition.y = target.position.y;
            }

            return newPosition;
        }
    }
}
