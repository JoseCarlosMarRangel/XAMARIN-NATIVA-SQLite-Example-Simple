using SQLite;
namespace SQLiteDB.Resources.Model {

    public class Person {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
    }
}