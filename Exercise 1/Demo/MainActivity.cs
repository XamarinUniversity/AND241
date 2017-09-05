using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections;
using System.Collections.Generic;

namespace Gestures
{
	[Activity (Label = "Gesture Explorer", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, GestureDetector.IOnGestureListener, GestureDetector.IOnDoubleTapListener
	{
		private GestureDetector gestureDetector;

		private ImageView xamLogo;

		private TextView tvScroll; 

		private ListView listOutput;

		DateTime start;
		List<string> messages = new List<string>();

		bool bOn;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			tvScroll = FindViewById<TextView> (Resource.Id.textViewScroll); 

			listOutput = FindViewById<ListView> (Resource.Id.listOutput);

			xamLogo = FindViewById<ImageView> (Resource.Id.xamLogo);
			xamLogo.Alpha = 0.6f;

			gestureDetector = new GestureDetector (context: this, listener: this);

			gestureDetector.SetOnDoubleTapListener (this);

			Clear ();
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			gestureDetector.OnTouchEvent (e);

			if (e.ActionMasked == MotionEventActions.Down) {
				xamLogo.Alpha = 1;
			} else if (e.ActionMasked == MotionEventActions.Up) {
				xamLogo.Alpha = bOn ? 1.0f : 0.6f;
				xamLogo.SetImageResource (Resource.Drawable.xamlogo);
			}

			xamLogo.TranslationX = e.GetX () - xamLogo.Width / 2;
			xamLogo.TranslationY = e.GetY () - xamLogo.Height / 2 - 200;

			return true;
		}

		public bool OnScroll (MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			tvScroll.Text = String.Format ("OnScroll - Δx:{0:0.0}dp Δy:{1:0.0}dp", distanceX, distanceY);
			return false;
		}

		public bool OnDown (MotionEvent e)
		{
			start = DateTime.Now;
			AddOutput("OnDown");
			return false;
		}
		public bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			AddOutput ("OnFling");
			return false;
		}
	
		public void OnLongPress (MotionEvent e)
		{
			AddOutput ("OnLongPress");

			xamLogo.SetImageResource (Resource.Drawable.xamlogo_gr);
		}

		public void OnShowPress (MotionEvent e)
		{
			AddOutput ("OnShowPress");

			xamLogo.SetImageResource (Resource.Drawable.xamlogo_rd);
		}

		public bool OnSingleTapUp (MotionEvent e)
		{
			AddOutput("OnSingleTapUp");
			return false;
		}

		void Clear ()
		{
			messages.Clear ();

			listOutput.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, messages);
			listOutput.Invalidate ();

			tvScroll.Text = String.Empty;
		}

		public bool OnDoubleTap (MotionEvent e)
		{
			AddOutput("OnDoubleTap");

			bOn = !bOn;
			return true;
		}

		public bool OnDoubleTapEvent (MotionEvent e)
		{
		//	AddOutput("OnDoubleTapEvent");
			return true;
		}

		public bool OnSingleTapConfirmed (MotionEvent e)
		{
		//	AddOutput("OnSingleTapConfirmed");
			return true;
		}

		void AddOutput (string msg)
		{
			var txt = String.Format("{0} - {1:0.0}ms", msg, (DateTime.Now - start).TotalMilliseconds);

			messages.Insert (0, txt);

			listOutput.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, messages);
			listOutput.Invalidate ();
		}

		public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.actionbar, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
		{
			switch (item.ItemId) {
			case Resource.Id.refresh:
				Clear ();
				xamLogo.TranslationX = xamLogo.TranslationY = 0;
				break;
			default:
				break;
			}

			return base.OnOptionsItemSelected(item);
		}

	}
}


