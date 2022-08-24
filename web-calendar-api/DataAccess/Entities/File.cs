using System;


namespace DataAccess.Entities
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateUploaded { get; set; }
        public int Size { get; set; }
        public Event Event { get; set; }
    }
}