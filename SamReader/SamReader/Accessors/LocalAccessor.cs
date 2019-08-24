using SamReader.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamReader
{
  public class LocalAccessor
  {
    public LocalAccessor(string dbPath)
    {
      _database = new SQLiteAsyncConnection(dbPath);
      _database.CreateTableAsync<Book>().Wait();
    }

    private readonly SQLiteAsyncConnection _database;

    public Task<List<Book>> GetBooksAsync()
    {
      return _database.Table<Book>().ToListAsync();
    }

    public Task<Book> GetBookAsync(int id)
    {
      return _database.Table<Book>()
                      .Where(i => i.Id == id)
                      .FirstOrDefaultAsync();
    }

    public Task<int> SaveBookAsync(Book book)
    {
      if (book.Id != 0)
      {
        return _database.UpdateAsync(book);
      }
      else
      {
        return _database.InsertAsync(book);
      }
    }

    public Task<int> DeleteBookAsync(Book book)
    {
      return _database.DeleteAsync(book);
    }
  }
}
