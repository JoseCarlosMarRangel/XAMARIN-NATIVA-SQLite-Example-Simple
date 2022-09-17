using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using SQLiteDB.Resources.Model;
using SQLiteDB.Resources.Helper;
using System.Collections.Generic;
using Person = SQLiteDB.Resources.Model.Person;
using System;

namespace SQLiteDB
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //! Componentes Globales
        ListView listviewdata;
        List<Person> listsource = new List<Person>();
        DataBase db;
        EditText edtName;
        Button btnAdd, btnUpd, btnDel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //!Instanciar la database
            db = new DataBase();
            //!Crear la database
            db.createDatabase();

            //! botones y componentes a usar
            listviewdata = FindViewById<ListView>(Resource.Id.listview1);
            edtName = FindViewById<EditText>(Resource.Id.editText1);
            btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnUpd = FindViewById<Button>(Resource.Id.btnUpd);
            btnDel = FindViewById<Button>(Resource.Id.btnDel);

            //!Cargar datos
            LoadData();

            //Todo Limpiar zona de Escritura

            //! Acciones
            btnAdd.Click += AddPerson;
            btnUpd.Click += EditPerson;
            btnDel.Click += DelPerson;

            //! Método para mostrar el número n de usuarios en la vista principal
            listviewdata.ItemClick += (s, e) => {
                for (int i = 0; i < listsource.Count; i++) 
                {
                    if (e.Position == i)
                    {
                        listviewdata.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Green);
                    }
                    else 
                    {
                        listviewdata.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    }
                    var txtName = e.View.FindViewById<TextView>(Resource.Id.textview2);
                    edtName.Tag = e.Id;
                }
            };
        }

        //! Añadir persona a la base de datos
        private void AddPerson(object sender, EventArgs e)
        {
            try {
                Person person = new Person()
                {
                    name = edtName.Text,
                };
                db.insertIntoTable(person);
                LoadData();
                edtName.Text = "";
            }
            catch(Exception ex) {
                edtName.Text = "No puedes agregar valores vacios";
            }
           
        }

        //! Editar persona de la base de datos
        private void EditPerson(object sender, EventArgs e)
        {
            try {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    name = edtName.Text
                };
                db.updateTable(person);
                LoadData();
                edtName.Text = "";
            }
            catch(Exception ex)
            {
                edtName.Text = "Selecciona primero que vas a editar";
            }
            
        }

        //! Borrar persona de la base de datos
        private void DelPerson(object sender, EventArgs e) {

            try 
            {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    name = edtName.Text
                };
                db.removeTable(person);
                LoadData();
            }
            catch(Exception ex){
                edtName.Text = "No puedes borrar nada";
            }
            

            
            
                
        }

        //! Recargar la data
        private void LoadData() {
            listsource = db.selectTable();
            var adapter = new ListViewAdapter(this, listsource);
            listviewdata.Adapter = adapter;
        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}