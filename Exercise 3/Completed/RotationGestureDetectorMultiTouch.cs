using System;
using Android.Views;
using System.Collections.Generic;

namespace RotationGesture
{
    //this is an updated version of the RoationGestureDetector that will update the offsetAngle w
    //when adding/removing more than two fingers to/from the screen.

	public class RotationGestureDetectorMutliTouch
	{
		IOnRotationGestureListener rotationListener;

        List<int> pointerIDs = new List<int>();

		float angleOffset;
		float angle;

		public RotationGestureDetectorMutliTouch(IOnRotationGestureListener listener)
		{
			rotationListener = listener;
		}

		public bool OnTouchEvent (MotionEvent e)
		{
            switch (e.ActionMasked) 
			{
                case MotionEventActions.Down:
                    pointerIDs.Add(e.GetPointerId(e.ActionIndex));
                    break;
                case MotionEventActions.PointerDown:
                    pointerIDs.Add(e.GetPointerId(e.ActionIndex));
                    angleOffset = GetAngle(e) - angle;
				    break;
			    case MotionEventActions.Move:
				    if(e.PointerCount >= 2)
                    {
                        angle = GetAngle(e) - angleOffset;
                        rotationListener?.OnRotate(angle);
                    }
				    break;
                case MotionEventActions.PointerUp:
                    pointerIDs.Remove(e.GetPointerId(e.ActionIndex));
                    angleOffset = GetAngle(e) - angle;
                    break;
                case MotionEventActions.Up:
                    pointerIDs.Clear();
                    break;
            }
			return true;
		}

		float GetAngle (MotionEvent e)
		{
            int index0 = e.FindPointerIndex(pointerIDs[0]);
            int index1 = e.FindPointerIndex(pointerIDs[1]);

 			return GetAngle (e.GetX(index0), e.GetY(index0), e.GetX(index1), e.GetY(index1));
		}

		float GetAngle (float x1, float y1, float x2, float y2)
		{
			var angle = Math.Atan2((y1 - y2), (x1 - x2));

			return (float)(angle * 180 / Math.PI);
		}
	}
}