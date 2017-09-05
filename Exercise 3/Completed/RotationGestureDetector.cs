using System;
using Android.Views;
using System.Collections.Generic;

namespace RotationGesture
{
	public class RotationGestureDetector
	{
		private IOnRotationGestureListener rotationListener;

		private static int INVALID_ID = -1;

		private int pId1 = INVALID_ID;
		private int pId2 = INVALID_ID;

		private float angleOffset;
		private float angle;

		public RotationGestureDetector (IOnRotationGestureListener listener)
		{
			rotationListener = listener;
	
			pId1 = INVALID_ID;
			pId2 = INVALID_ID;
		}

		public bool OnTouchEvent (MotionEvent e)
		{
			switch (e.ActionMasked) 
			{
			case MotionEventActions.Down:
				pId1 = e.GetPointerId(e.ActionIndex);
				break;
			case MotionEventActions.PointerDown:
				//save the 2nd pointer ID
				pId2 = e.GetPointerId (e.ActionIndex);
				//since we now have 2 fingers on screen, 
				//use the GetAngle helper method
				//to find an initial relative angle which we'll use an offset for our rotation
				angleOffset = GetAngle (e, pId1, pId2) - angle;
				break;
			case MotionEventActions.Move:
				if (e.PointerCount != 2 || 
					pId1 == INVALID_ID || pId2 == INVALID_ID)
					return false;

				//calculate the current angle and subtract the initial offset
				angle = GetAngle (e, pId1, pId2) - angleOffset;

				//call the OnRotate method of the rotationListener to report the current angle
				if (rotationListener != null)
					rotationListener.OnRotate (angle); 

				break;
			case MotionEventActions.Up:
			case MotionEventActions.PointerUp: 
			//add cases for Up & PointerUp 
			//and set each Id to invalid
				int id = e.GetPointerId (e.ActionIndex);
				if (id == pId1)
					pId1 = INVALID_ID;
				if (id == pId2)
					pId2 = INVALID_ID;
				break;

			}
			return true;
		}

		//these will be used to calculate the angle from the pointer IDs
		float GetAngle (MotionEvent e, int pointerId1, int pointerId2)
		{
			var x1 = e.GetX (pointerId1);
			var y1 = e.GetY (pointerId1);
			var x2 = e.GetX (pointerId2);
			var y2 = e.GetY (pointerId2);

			return GetAngle (x1, y1, x2, y2);
		}

		float GetAngle (float x1, float y1, float x2, float y2)
		{
			double angle = Math.Atan2((y1 - y2), (x1 - x2));

			return (float)(angle * 180 / Math.PI);
		}
	}
}

