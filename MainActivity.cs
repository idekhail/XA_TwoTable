using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;

using AndroidX.AppCompat.App;

namespace XA_SQLite_Lectures
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            var Code = FindViewById<EditText>(Resource.Id.Code);

            var Display = FindViewById<Button>(Resource.Id.Display);
            var AddCourse = FindViewById<Button>(Resource.Id.AddCouse);
            var Close = FindViewById<Button>(Resource.Id.Close);


            Display.Click += delegate
            {
                if (!string.IsNullOrEmpty(Code.Text))
                {
                    SQLiteDB sq = new SQLiteDB();
                    var lecture = sq.GetLecture(Code.Text);
                    if (lecture != null)
                    {
                        Intent i = new Intent(this, typeof(DisplayActivity));
                        i.PutExtra("CourseCode", Code.Text);
                        StartActivity(i);
                    }
                    else
                        Toast.MakeText(this, "Course Code is Empty", ToastLength.Long).Show();
                }
                else
                    Toast.MakeText(this, "lecture not phound!!!!", ToastLength.Long).Show();
            };

            AddCourse.Click += delegate
            {
                Intent i = new Intent(this, typeof(AddCourseActivity));
                StartActivity(i);
            };

            // Logout to Login Screen
            Close.Click += delegate
            {
                System.Environment.Exit(0);
            };

        }
    }
}