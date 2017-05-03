using System.Collections.Generic;
using System.Threading.Tasks;
using ManDown.Models;
using SQLite;

namespace ManDown.Database
{
    public class ContactsDb
    {
        readonly SQLiteAsyncConnection database;

        public ContactsDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetItemsAsync()
        {
            return database.Table<Person>().ToListAsync();
        }

        public Task<Person> GetItemAsync(ContactType type)
        {
            return database.Table<Person>().Where(i => i.ContactType == type).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Person item)
        {
            if (item.ContactType != ContactType.Emergency || item.ContactType != ContactType.Patient)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Person item)
        {
            return database.DeleteAsync(item);
        }
    }
}
