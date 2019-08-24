using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamReader.Entities
{
  public class Book
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Encoding { get; set; }

    public DateTime LastAccessTime { get; set; }
    public long Seek { get; set; }
  }
}
