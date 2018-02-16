using System;
using Android.Views;

namespace RotationGesture
{
	public class RotationGestureDetector
	{
		IOnRotationGestureListener rotationListener;

		float angleOffset;
		float angle;

		public RotationGestureDetector (IOnRotationGestureListener listener)
		{
			rotationListener = listener;
		}

		public bool OnTouchEvent (MotionEvent e)
		{
            switch (e.ActionMasked) 
			{
                case MotionEventActions.PointerDown:
                    if(e.PointerCount == 2)
                        angleOffset = GetAngle(e) - angle;
				    break;
			    case MotionEventActions.Move:
				    if(e.PointerCount >= 2)
                    {
                        angle = GetAngle(e) - angleOffset;
                        rotationListener?.OnRotate(angle);
                    }
				    break;
            }
			return true;
		}

		float GetAngle (MotionEvent e)
		{
			return GetAngle (e.GetX(0), e.GetY(0), e.GetX(1), e.GetY(1));
		}

		float GetAngle (float x1, float y1, float x2, float y2)
		{
			var angle = Math.Atan2((y1 - y2), (x1 - x2));

			return (float)(angle * 180 / Math.PI);
		}
	}
}