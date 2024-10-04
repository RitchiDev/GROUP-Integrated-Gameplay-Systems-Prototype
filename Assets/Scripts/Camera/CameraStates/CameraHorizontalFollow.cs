using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using CameraSystem;

namespace CameraState
{
    [CreateAssetMenu(fileName = "Camera Horizontal Follow State", menuName = "Camera System/States/Camera Horizontal Follow State")]
    public class CameraHorizontalFollow : BaseCameraState
    {
        public override Vector3 CalculateNewPosition(AreaCameraHandler _roomCamera)
        {
            Transform cameraTransform = _roomCamera.CameraTransform;
            Transform target = _roomCamera.Target;
            Vector3 followTreshold = _roomCamera.CalculateFollowTreshold();

            float xDifference = Vector2.Distance(Vector2.right * cameraTransform.position.x, Vector2.right * target.position.x);

            Vector3 newPosition = cameraTransform.position;

            if (Mathf.Abs(xDifference) >= followTreshold.x)
            {
                newPosition.x = target.position.x;
            }

            float yCenter = _roomCamera.CurrentArea.GetCenter().y;
            newPosition.y = yCenter;

            return newPosition;
        }
    }
}