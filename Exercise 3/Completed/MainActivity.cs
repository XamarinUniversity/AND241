using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RotationGesture;

namespace Gestures
{
	[Activity (Label = "Gestures", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, GestureDetector.IOnGestureListener, ScaleGestureDetector.IOnScaleGestureListener, IOnRotationGestureListener
	{
		private RotationGestureDetector rotationDetector;
		private ScaleGestureDetector scaleDetector;
		private GestureDetector gestureDetector;

		private ImageView xamLogo;

		private float deltaX, deltaY;
		private float scale = 1.0f;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);

			gestureDetector = new GestureDetector (context: this, listener: this);
			scaleDetector = new ScaleGestureDetector (this, this);
			rotationDetector = new RotationGestureDetector (this);
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			gestureDetector.OnTouchEvent (e);
			scaleDetector.OnTouchEvent (e);
			rotationDetector.OnTouchEvent (e);

			return true;
		}

		public void OnRotate (float angle)
		{
			//set the rotation of the xamarin logo
			xamLogo.Rotation = angle;
		}

		public bool OnScale (ScaleGestureDetector detector)
		{
			this.scale *= detector.ScaleFactor;

			xamLogo.ScaleX = scale;
			xamLogo.ScaleY = scale;

			return true;
		}

		public bool OnScaleBegin (ScaleGestureDetector detector)
		{
			return true;
		}

		public void OnScaleEnd (ScaleGestureDetector detector)
		{
		}

		public bool OnDown (MotionEvent e)
		{
			return false;
		}
		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			return false;
		}
	
		public void OnLongPress (MotionEvent e)
		{
		}

		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			deltaX -= distanceX;
			deltaY -= distanceY;

			xamLogo.TranslationX = deltaX;
			xamLogo.TranslationY = deltaY;

			return true;
		}
	
		public void OnShowPress (MotionEvent e)
		{
		}

		public bool OnSingleTapUp (MotionEvent e)
		{
			return false;
		}
	}
}


