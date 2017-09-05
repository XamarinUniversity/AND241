using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Gestures
{
	//Step 1 - create a new Android App

	//Step 2 - delete boiler plate code 
	//remove the main.axml from Resources / layout and remove the button/count code from MainActivity.cs

	//Step 3 - add assets
	//3a - add new main.axml file from Assets folder
	//3b - add xamlogo.png to Resources/drawable-mdpi
	[Activity (Label = "Gestures", MainLauncher = true, Icon = "@drawable/icon")]
	//Step 4a - impliment IOnGestureListener
	public class MainActivity : Activity, GestureDetector.IOnGestureListener
	{
		//Step 5 - add class level variables
		private GestureDetector gestureDetector;
		private ImageView xamLogo;
		private float deltaX, deltaY;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			//Step 6 - save reference to the Xamarin Logo ImageView
			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			//Step 7 - instantiate GestureDetector
			gestureDetector = new GestureDetector (context: this, listener: this);
		}

		//Step 8 - override OnTouchEvent 
		public override bool OnTouchEvent (MotionEvent e)
		{
			//8b - pass MotionEvent to the gesture detector via the OnTouchEvent method
			return gestureDetector.OnTouchEvent (e);
		}

		//4b
		public bool OnDown (MotionEvent e)
		{
			//4c remove code to throw NotImplimented exception and return false where appropriate
			return false;
		}
		//4b
		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			//4c 
			return false;
		}



		//4b
		public void OnLongPress (MotionEvent e)
		{
			//4c 
		}
		//4b
		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			//4c 
			//Step 9 - add code to translate the Xamarin Logo Image view in the IOnGestureListener OnScroll method 
			deltaX -= distanceX;
			deltaY -= distanceY;

			xamLogo.TranslationX = deltaX;
			xamLogo.TranslationY = deltaY;

			return true;
		}
		//4b
		public void OnShowPress (MotionEvent e)
		{
			//4c 
		}

		public bool OnSingleTapUp (MotionEvent e)
		{
			//4c
			return false;
		}
	}
}


