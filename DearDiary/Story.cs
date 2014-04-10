using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace DearDiary
{
    [Table]
    public class Story
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity")]
        public int idStory { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Created { get; set; }

        [Column]
        public string Feeling { get; set; }

        [Column]
        public string TextStory { get; set; }
    }

    public class StoryContext : DataContext
    {
        public Table<Story> diary;
        public StoryContext(string connectionstring) : base(connectionstring) { }
    }
}
