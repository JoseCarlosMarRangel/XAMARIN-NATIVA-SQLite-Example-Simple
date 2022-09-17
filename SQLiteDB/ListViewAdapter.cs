using Android.App;
using Android.Widget;
using Android.Views;
using System.Collections.Generic;

namespace SQLiteDB.Resources.Model {

    public class ViewHolder : Java.Lang.Object 
    {
       
        public TextView textview1 { get;set;}
    }
    public class ListViewAdapter : BaseAdapter {
        private Activity activity;
        private List<Person> listperson;
        public ListViewAdapter(Activity activity, List<Person> listperson)
        { 
            this.activity = activity;
            this.listperson = listperson;
        }
        public override int Count {
            get { return listperson.Count; }
        }
        public override Java.Lang.Object GetItem(int position) 
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return listperson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view,parent,false);
            var textview1 = view.FindViewById<TextView>(Resource.Id.textview2);
            textview1.Text = listperson[position].name;
            return view;
        }
    }
}