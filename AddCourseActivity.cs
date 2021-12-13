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
    [Activity(Label = "AddCourseActivity")]
    public class AddCourseActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_addcourse);


            var Code = FindViewById<EditText>(Resource.Id.Code);
            var Course = FindViewById<EditText>(Resource.Id.Course);
            var Teacher = FindViewById<EditText>(Resource.Id.Teacher);

            var AddCouse = FindViewById<Button>(Resource.Id.AddCouse);
            var Cancel = FindViewById<Button>(Resource.Id.Cancel);

            AddCouse.Click += delegate
            {
                var sq = new SQLiteDB();
                if (Teacher.Text != "")
                {                  
                    var newTeacher = new SQLiteDB.Teachers()
                    {
                        Name = Teacher.Text,
                    };

                    sq.InsertTeacher(newTeacher);

                }
                else
                {
                    Toast.MakeText(this, " Code is found", ToastLength.Short).Show();
                }

                var teach = sq.GetTeacher(Teacher.Text);


                if (Code.Text != "" && Course.Text != "")
                {
                    var lecture = sq.GetLecture(Code.Text);

                if (lecture == null)
                {
                    var newLecture = new SQLiteDB.Lectures()
                    {
                        CourseCode = Code.Text,
                        CourseName = Course.Text,
                        TId = teach.Id
                    };                    

                    sq.InsertLecture(newLecture);
                    Intent i = new Intent(this, typeof(MainActivity));
                    StartActivity(i);
                }
                else
                {
                    Toast.MakeText(this, " Code is found", ToastLength.Short).Show();
                }
            }
                else
            {
                Toast.MakeText(this, " Code , Course or Teacher failds are empty", ToastLength.Short).Show();
            }
        
            };


            Cancel.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
            };

        }
    }
}