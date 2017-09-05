using System;

namespace RotationGesture
{
	//Step 1a - create an interface for the rotation gesture listener 
	public interface IOnRotationGestureListener
	{
		//Step 1b - define an OnRotate method that will be used to receive the current angle of the gesture
		void OnRotate (float angle); 
	}
}

