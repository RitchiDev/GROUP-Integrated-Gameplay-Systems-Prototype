using CameraState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraSystem
{
    [CreateAssetMenu(fileName = "New Camera Data", menuName = "Camera System/Camera Data", order = 2)]
    public class CameraData : ScriptableObject
    {
        [Header("2D Camera")]
        [SerializeField] private bool is2D = true;
        public bool Is2D => is2D;

        [SerializeField] private float cameraZ = -10f;
        public float CameraZ => cameraZ;

        [Header("Lerp")]
        [SerializeField] private float smoothSpeed = 5f; // Use player velocity if the player exceeds the smoothSpeed.
        public float SmoothSpeed => smoothSpeed;

        [Header("Target")]
        [SerializeField] private int trackTargetDelay = 1000;
        public int TrackTargetDelay => trackTargetDelay;

        [Header("States")]
        [SerializeField] private BaseCameraState idleCameraState;
        public BaseCameraState IdleCameraState => idleCameraState;

        [SerializeField] private BaseCameraState[] cameraStates;
        public BaseCameraState[] CameraStates => cameraStates;

        [Header("Areas")]
        [SerializeField] private CameraArea initialArea;
        public CameraArea InitialArea => initialArea;
        [SerializeField] private List<CameraArea> availableAreas = new List<CameraArea>();
        public List<CameraArea> AvailableAreas => availableAreas;
    }
}
