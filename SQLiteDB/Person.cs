using SQLite;
namespace SQLiteDB.Resources.Model {

    //! Estructura de la persona
    public class Person {

        //! Tiene clave primaria automática auto incrementable
        [PrimaryKey, AutoIncrement]
        //! Variable de la ID
        public int Id { get; set; }

        //! Variable del nombre
        public string name { get; set; }
    }
}