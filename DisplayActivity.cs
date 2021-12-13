using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XA_SQLite_Lectures
{
    [Activity(Label = "DisplayActivity")]
    public class DisplayActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_display);

            var Show = FindViewById<TextView>(Resource.Id.Show);

            var CourseName = FindViewById<EditText>(Resource.Id.CourseName);

            var AllCodes = FindViewById<Button>(Resource.Id.AllCodes);
            var Logout = FindViewById<Button>(Resource.Id.Logout);
            var AllCourses = FindViewById<Button>(Resource.Id.AllCourses);
            var Control = FindViewById<Button>(Resource.Id.Control);

            string CourseCode = Intent.GetStringExtra("CourseCode");

            SQLiteDB sq = new SQLiteDB();
            var lecture = sq.GetLecture(CourseCode);

            if (lecture != null)
            {
                Show.Text = lecture.Id + "\t\t" + lecture.CourseCode + "\t\t" + lecture.CourseName + "\t\t" + lecture.TId;
            }
            else
                Toast.MakeText(this, "Lecture is null ", ToastLength.Long).Show();


            // Display all users in db
            AllCodes.Click += delegate
            {
                var table = sq.GetAllCodes(CourseName.Text);
                string data = "";
                foreach (var s in table)
                    data += s.Id + "\t" + s.CourseCode + "\t" + s.CourseName + "\t" + s.TId + "\n";
                Show.Text = data;
            };


            // Display all users in db
            AllCourses.Click += delegate
            {
                var table = sq.GetAllCourses();
                string data = "";
                foreach (var s in table)
                    data += s.Id + "\t" + s.CourseCode + "\t" + s.CourseName + "\t" + s.TId + "\n";
                Show.Text = data;
            };

            // Logout to Login Screen
            Control.Click += delegate
            {
                Intent i = new Intent(this, typeof(ControlActivity));
                i.PutExtra("CourseCode", lecture.CourseCode);
                StartActivity(i);
            };

            // Logout to Login Screen
            Logout.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };
        }
    }
}