using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraState;
using System.Threading.Tasks;

namespace CameraSystem
{
    public class AreaCameraHandler : GameBehaviour
    {
        [Header("FSM")]
        private CameraFSM fSM;

        [Header("Data")]
        private CameraData cameraData;
        private const string DATA_NAME = "CameraData";

        [Header("Camera")]
        [SerializeField] private Camera currentCamera;
        [SerializeField] private Transform cameraTransform;
        public Transform CameraTransform => cameraTransform;
        [SerializeField] private Vector3 followOffset;
        private Dictionary<CameraArea, Action> changeAreaDictionary = new Dictionary<CameraArea, Action>();
        private Vector3 newCameraPosition;
        public Vector3 CurrentNewCameraPosition => newCameraPosition;

        [Header("Target")]
        private Transform targetToTrack;
        public Transform Target => targetToTrack;
        private Vector3Int currentTargetPosition;
        private Vector3Int previousTargetPosition;

        [Header("Area")]
        private CameraArea currentArea;
        public CameraArea CurrentArea => currentArea;

        public override void Start()
        {
            SetUp();

            StartTracking();
        }

        public override void Update()
        {
            fSM.OnUpdate(this);

            TrackTarget();

            previousTargetPosition = currentTargetPosition;
        }

        public override void LateUpdate()
        {
            LerpSize();

            LerpPosition();
        }

        private void SetUp()
        {
            CreateCamera();

            SetUpFSM();

            SetUpAreas();

            SetUpTarget();
        }

        private void CreateCamera()
        {
            cameraData = Resources.Load<CameraData>(DATA_NAME);

            GameObject cameraObject = new GameObject("Camera");
            cameraObject.AddComponent<AudioListener>();
            currentCamera = cameraObject.AddComponent<Camera>();

            currentCamera.orthographic = cameraData.Is2D;

            cameraTransform = currentCamera.transform;

            SetNewPositionValue(new Vector3(0, 0, cameraData.CameraZ));
        }

        /// <summary>
        /// Setup the FiniteStateMachine.
        /// </summary>
        private void SetUpFSM()
        {
            fSM = new CameraFSM(cameraData.IdleCameraState, cameraData.CameraStates);
        }

        private void SetUpAreas()
        {
            for (int i = 0; i < cameraData.AvailableAreas.Count; i++)
            {
                CameraArea iteratedArea = cameraData.AvailableAreas[i];

                changeAreaDictionary.Add(iteratedArea, () => ChangeArea(iteratedArea));
            }
        }

        private void SetUpTarget()
        {
            targetToTrack = null;
        }

        /// <summary>
        /// Start tracking the target.
        /// </summary>
        private void StartTracking()
        {
            if (targetToTrack == null)
            {
                Debug.LogWarning("No trackable target has been found!");
                return;
            }

            TrackTarget();
        }

        /// <summary>
        /// Uses a roundend position of the target to track if it has entered the current iterated area.
        /// </summary>
        /// <returns></returns>
        private async void TrackTarget()
        {
            while(Application.isPlaying)
            {
                await Task.Delay(cameraData.TrackTargetDelay);

                for (int i = 0; i < cameraData.AvailableAreas.Count; i++)
                {
                    CameraArea iteratedArea = cameraData.AvailableAreas[i];

                    if (iteratedArea == currentArea)
                    {
                        continue;
                    }

                    UpdateTargetPosition();

                    if (currentTargetPosition == previousTargetPosition)
                    {
                        continue;
                    }

                    // Player moved towards new tile

                    if (iteratedArea.Contains(currentTargetPosition))
                    {
                        InvokeAreaChange(iteratedArea);
                    }
                }
            }
        }

        /// <summary>
        /// Smoothly lerps the camrea towards the position value;
        /// </summary>
        private void LerpPosition()
        {
            if (cameraTransform == null)
            {
                Debug.LogWarning("no Camera transform has been assigned!");
                return;
            }

            if (cameraTransform.position == newCameraPosition)
            {
                return;
            }

            Vector3 updatedPosition = Vector3.Lerp(cameraTransform.position, newCameraPosition, cameraData.SmoothSpeed * Time.deltaTime);
            cameraTransform.position = updatedPosition;
        }

        /// <summary>
        /// Smoothly lerps the camrea towards the position value;
        /// </summary>
        private void LerpSize()
        {
            if (currentArea == null)
            {
                return;
            }

            if (currentCamera.orthographicSize == currentArea.CameraSize)
            {
                return;
            }

            float updatedSize = Mathf.Lerp(currentCamera.orthographicSize, currentArea.CameraSize, cameraData.SmoothSpeed * Time.deltaTime);
            currentCamera.orthographicSize = updatedSize;
        }


        private void InvokeAreaChange(CameraArea _newArea)
        {
            changeAreaDictionary[_newArea]?.Invoke();
        }

        /// <summary>
        /// Switch to a new area and the camera state assigned to the area.
        /// </summary>
        /// <param name="_newArea"></param>
        private void ChangeArea(CameraArea _newArea)
        {
            if (changeAreaDictionary.Count <= 0)
            {
                Debug.LogWarning("There are no areas setup!");
                return;
            }

            if (currentArea != null)
            {
                currentArea.OnExit();
            }

            currentArea = _newArea;

            _newArea.OnEnter();

            fSM.SwitchState(_newArea.CameraState);
        }

        public void SetNewPositionValue(Vector3 _newPosition)
        {
            newCameraPosition = _newPosition;

            if (cameraData.Is2D)
            {
                newCameraPosition.z = cameraData.CameraZ;
            }
        }

        private void UpdateTargetPosition()
        {
            if (targetToTrack == null)
            {
                Debug.LogWarning("There is no target to track!");
                return;
            }

            currentTargetPosition = new Vector3Int(
                    Mathf.RoundToInt(targetToTrack.position.x),
                    Mathf.RoundToInt(targetToTrack.position.y),
                    Mathf.RoundToInt(targetToTrack.position.z));
        }

        public Vector3 CalculateFollowTreshold()
        {
            Rect aspect = currentCamera.pixelRect;
            float orthographicSize = currentCamera.orthographicSize;
            Vector2 t = new Vector2(orthographicSize * aspect.width / aspect.height, orthographicSize);

            t.x -= followOffset.x;
            t.y -= followOffset.y;

            return t;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector2 border = CalculateFollowTreshold();
            Vector3 bounds = new Vector3(border.x * 2, border.y * 2, 1);
            Gizmos.DrawWireCube(cameraTransform.position, bounds);
        }
#endif

    }
}
