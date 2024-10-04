using CameraSystem;
using FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraState
{
    [CreateAssetMenu(fileName = "Camera Center State", menuName = "Camera System/States/Camera Center State", order = 2)]
    public class CameraCenter : BaseCameraState
    {
        public override Vector3 CalculateNewPosition(AreaCameraHandler _roomCamera)
        {
            return _roomCamera.CurrentArea.GetCenter();
        }
    }
}
