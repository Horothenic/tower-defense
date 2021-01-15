using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;

using Utilities.Events;

namespace Utilities.TouchDetection
{
    public class TouchManager : MonoBehaviour
    {
        #region FIELDS

        private const float ClickThresholdPercentage = 0.012f;
        private const float EightDirectionDetectionThreshold = 0.45f;

        [Header("CONFIGURATIONS")]
        [SerializeField] private bool forceMouse = false;

        [Header("STATUS")]
        [SerializeField] private bool detected = false;

        #endregion

        #region EVENTS

        [Header("EVENTS")]
        public UnityVector2Event onStart;
        public UnityVector2Event onStay;
        public UnityVector2Event onMove;
        public UnityVector2Event onEnd;
        public UnityVector2Event onClicked;

        #endregion

        #region PROPERTIES

        private bool MouseMoved { get => InputPosition != (Vector2)Input.mousePosition; }

        private int CurrentTouchId { set; get; }
        private Touch CurrentTouch { get => Input.touches.First(touch => touch.fingerId == CurrentTouchId); }

        public float ClickThreshold { get { return Screen.width * ClickThresholdPercentage; } }

        public Vector2 StartPosition { private set; get; }
        private Vector2 InputPosition { set; get; }

        #endregion

        #region BEHAVIORS

        void Update()
        {
            if (Application.isEditor || forceMouse)
                DetectMouse();
            else
                DetectTouches();
        }

        private void DetectMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPosition = InputPosition = Input.mousePosition;

                if (IsClickingOverUI())
                    return;

                detected = true;
                onStart?.Invoke(Input.mousePosition);
                return;
            }

            if (!detected)
                return;

            if (Input.GetMouseButtonUp(0))
            {
                detected = false;
                if (Vector2.Distance(StartPosition, InputPosition) <= ClickThreshold)
                {
                    onClicked?.Invoke(InputPosition);
                    return;
                }

                onEnd?.Invoke(InputPosition);
                return;
            }

            if (Input.GetMouseButton(0))
            {
                if (MouseMoved)
                    onMove?.Invoke(InputPosition);
                else
                    onStay?.Invoke(InputPosition);

                InputPosition = Input.mousePosition;
            }
        }

        private void DetectTouches()
        {
            if (!detected)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        StartPosition = InputPosition = touch.position; ;

                        if (IsClickingOverUI())
                            return;

                        detected = true;
                        CurrentTouchId = touch.fingerId;

                        onStart?.Invoke(StartPosition);
                        return;
                    }
                }
            }
            else
            {
                Touch touch = CurrentTouch;

                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        InputPosition = touch.position;
                        onMove?.Invoke(InputPosition);
                        break;
                    case TouchPhase.Stationary:
                        InputPosition = touch.position;
                        onStay?.Invoke(InputPosition);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        detected = false;
                        if (Vector2.Distance(StartPosition, InputPosition) <= ClickThreshold)
                            onClicked?.Invoke(InputPosition);

                        onEnd?.Invoke(InputPosition);
                        break;
                }
            }
        }

        public Direction DetectDirection(Vector2 start, Vector2 finish)
        {
            Vector2 result = (finish - start).normalized;
            Direction direction = Direction.None;

            if (Mathf.Abs(result.x) > Mathf.Abs(result.y))
            {
                if (result.x > 0)
                    direction |= Direction.Right;
                else
                    direction |= Direction.Left;

                if (Mathf.Abs(result.y) > EightDirectionDetectionThreshold)
                {
                    if (result.y > 0)
                        direction |= Direction.Up;
                    else
                        direction |= Direction.Down;
                }
            }
            else
            {
                if (result.y > 0)
                    direction |= Direction.Up;
                else
                    direction |= Direction.Down;

                if (Mathf.Abs(result.x) > EightDirectionDetectionThreshold)
                {
                    if (result.x > 0)
                        direction |= Direction.Right;
                    else
                        direction |= Direction.Left;
                }
            }

            return direction;
        }

        private bool IsClickingOverUI()
        {
            PointerEventData cursor = new PointerEventData(EventSystem.current);
            cursor.position = StartPosition;
            List<RaycastResult> objectsHit = new List<RaycastResult>();
            EventSystem.current.RaycastAll(cursor, objectsHit);

            return objectsHit.Count > 0;
        }

        #endregion
    }
}
