using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using CameraSystem;

namespace CameraState
{
    [CreateAssetMenu(fileName = "Camera Vertical Follow State", menuName = "Camera System/States/Camera Vertical Follow State")]
    public class CameraVerticalFollow : BaseCameraState
    {
        public override Vector3 CalculateNewPosition(AreaCameraHandler _roomCamera)
        {
            Transform cameraTransform = _roomCamera.CameraTransform;
            Transform target = _roomCamera.Target;
            Vector3 followTreshold = _roomCamera.CalculateFollowTreshold();

            float yDifference = Vector2.Distance(Vector2.up * cameraTransform.position.y, Vector2.up * target.position.y);

            Vector3 newPositition = cameraTransform.position;

            if (Mathf.Abs(yDifference) >= followTreshold.y)
            {
                newPositition.y = target.position.y;
            }

            float xCenter = _roomCamera.CurrentArea.GetCenter().x;
            newPositition.x = xCenter;

            return newPositition;
        }
    }
}
    