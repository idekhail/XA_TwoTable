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
    [Activity(Label = "ControlActivity")]
    public class ControlActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here


            // Create your application here
            SetContentView(Resource.Layout.activity_control);

            var Id = FindViewById<TextView>(Resource.Id.LId);
            var Code = FindViewById<TextView>(Resource.Id.Code);

            var Course = FindViewById<EditText>(Resource.Id.Course);
            var Teacher = FindViewById<EditText>(Resource.Id.Teacher);

            var Update = FindViewById<Button>(Resource.Id.Update);
            var Delete = FindViewById<Button>(Resource.Id.Delete);
            var Cancel = FindViewById<Button>(Resource.Id.Cancel);

            string CourseCode = Intent.GetStringExtra("CourseCode");

            SQLiteDB sq = new SQLiteDB();
            var lecture = sq.GetLecture(CourseCode);

            if (lecture != null)
            {
                Id.Text = Convert.ToString(lecture.Id);
                Code.Text = lecture.CourseCode;
                Course.Text = lecture.CourseCode;
                Teacher.Text = lecture.TId+"";
            }


            Update.Click += delegate
            {
                if (Course.Text != "" && Teacher.Text != "")
                {
                    lecture.CourseName = Course.Text;
                    lecture.TId = Convert.ToInt32(Teacher.Text);

                    sq.UpdateLecture(lecture);
                    Intent i = new Intent(this, typeof(DisplayActivity));
                    i.PutExtra("CourseCode", lecture.CourseCode);
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, " Course or Teacher failds are empty", ToastLength.Short).Show();
                }
            };

            Delete.Click += delegate
            {
                sq.DeleteLecture(lecture);
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);

            };

            Cancel.Click += delegate
            {
                Intent i = new Intent(this, typeof(DisplayActivity));
                i.PutExtra("CourseCode", lecture.CourseCode);
                StartActivity(i);
            };

        }
    }
}