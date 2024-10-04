using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraState;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CameraSystem
{
    [CreateAssetMenu(fileName = "New Camera Area", menuName = "Camera System/Camera Area", order = 1)]
    public class CameraArea : ScriptableObject
    {
        [SerializeField] private BaseCameraState cameraState;
        public BaseCameraState CameraState => cameraState;

        [SerializeField] private float cameraSize = 7;
        public float CameraSize => cameraSize;

        [SerializeField] private Vector3 minimumBoundary = new Vector3(-25f, 1f, -22f);
        [SerializeField] private Vector3 maximumBoundary = new Vector3(25f, 20f, 15F);
        public Vector3 Minimum => minimumBoundary;
        public Vector3 Maximum => maximumBoundary;

        public float MinimumX => minimumBoundary.x;
        public float MinimumY => minimumBoundary.y;
        public float MinimumZ => minimumBoundary.z;
        public float MaximumX => maximumBoundary.x;
        public float MaximumY => maximumBoundary.y;
        public float MaximumZ => maximumBoundary.z;

        [Header("Debug")]
        [SerializeField] private bool isIgnoringActiveScenes = false;
        [SerializeField] private Color labelColor = Color.white;
        [SerializeField] private Color areaColor = Color.white;
        public Color AeraColor => areaColor;
        [SerializeField] private List<int> scenesActiveIn = new List<int>();
        public List<int> ScenesActiveIn => scenesActiveIn;

#if UNITY_EDITOR
        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneUpdate;
        }
#endif

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneUpdate;
        }

        private void OnSceneUpdate(SceneView _sceneView)
        {
            for (int i = 0; i < scenesActiveIn.Count; i++)
            {
                int sceneIndex = scenesActiveIn[i];

                if (sceneIndex == SceneManager.GetActiveScene().buildIndex || isIgnoringActiveScenes)
                {
                    DrawGizmos();
                    EditorUtility.SetDirty(_sceneView);
                }
            }
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {

        }

        public Vector3 GetCenter()
        {
            Vector3 center = (minimumBoundary + maximumBoundary) * 0.5f;
            return center;
        }

        public bool Contains(Vector3Int _targetPosition)
        {
            if (_targetPosition.x < minimumBoundary.x || _targetPosition.x > maximumBoundary.x)
            {
                return false;
            }

            if (_targetPosition.y < minimumBoundary.y || _targetPosition.y > maximumBoundary.y)
            {
                return false;
            }

            if (_targetPosition.z < minimumBoundary.z || _targetPosition.z > maximumBoundary.z)
            {
                return false;
            }

            return true;
        }

        public bool RoomIsValid()
        {
            bool xIsValid = minimumBoundary.x < maximumBoundary.x;
            bool yIsValid = minimumBoundary.y < maximumBoundary.y;
            bool zIsValid = minimumBoundary.z <= maximumBoundary.z;

            if (xIsValid && yIsValid && zIsValid)
            {
                return true;
            }

            return false;
        }

#if UNITY_EDITOR

        public void DrawGizmos()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = labelColor;
            style.alignment = TextAnchor.MiddleCenter;

            Handles.color = Color.white;
            if (cameraState != null)
            {
                Handles.color = cameraState.AreaColor;
            }

            DrawBounds();

            DrawTypeLabel(style);

            DrawSizeLabel(style);

            DrawValidLabel(style);
        }

        private void DrawBounds()
        {
            Vector3 scale = minimumBoundary - maximumBoundary;
            scale.x = Mathf.Abs(scale.x);
            scale.y = Mathf.Abs(scale.y);
            scale.z = Mathf.Abs(scale.z);

            Handles.DrawWireCube(GetCenter(), scale);
        }

        private void DrawTypeLabel(GUIStyle _style)
        {
            if (cameraState == null)
            {
                Handles.Label(GetCenter(), $"{name}\nNo state has been referenced!", _style);

                return;
            }

            Handles.Label(GetCenter(), $"{name}\n{cameraState.name}", _style);
        }

        private void DrawSizeLabel(GUIStyle _style)
        {
            Handles.Label(GetCenter() + (Vector3.down * 2), $"Orthographic Size: {cameraSize}", _style);
        }

        private void DrawValidLabel(GUIStyle _style)
        {
            if (RoomIsValid())
            {
                return;
            }

            Handles.Label(GetCenter() + (Vector3.up * 3), "INVALID AREA", _style);
        }

#endif
    }
}
