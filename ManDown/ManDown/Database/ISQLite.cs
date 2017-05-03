using SQLite;

namespace ManDown.Database
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
