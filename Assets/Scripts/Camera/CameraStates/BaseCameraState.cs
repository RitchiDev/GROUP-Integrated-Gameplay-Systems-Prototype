using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using CameraSystem;

namespace CameraState
{
    public class BaseCameraState : BaseState
    {
        [SerializeField] private Color areaColor = Color.white;
        public Color AreaColor => areaColor;

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }

        public virtual void UpdateWithCamera(AreaCameraHandler _roomCamera)
        {
            Vector3 newPositionResult = CalculateNewPosition(_roomCamera);

            if (_roomCamera.CurrentNewCameraPosition == newPositionResult)
            {
                return;
            }

            _roomCamera.SetNewPositionValue(newPositionResult);
        }

        public virtual Vector3 CalculateNewPosition(AreaCameraHandler _roomCamera)
        {
            return _roomCamera.CurrentNewCameraPosition;
        }
    }
}
