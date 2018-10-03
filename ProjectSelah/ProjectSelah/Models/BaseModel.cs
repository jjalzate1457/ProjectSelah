using ProjectSelah.API;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectSelah.Models
{
    public class Model : Notifyable
    {
        [Column(name: "id")]
        string RecordId { get; set; }

        public string Id
        {
            get { return RecordId; }
            set
            {
                RecordId = value;
                NotifyPropertyChanged();
            }
        }

        [Column(name: "name")]
        string db_Name { get; set; }
        public string Name
        {
            get { return db_Name; }
            set
            {
                db_Name = value;
                NotifyPropertyChanged();
            }
        }


        public Model()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
