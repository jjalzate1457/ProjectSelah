using ProjectSelah.API;
using System;
using System.ComponentModel.DataAnnotations;
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

        int db_Order { get; set; }
        [Column(name: "Order")]
        public int Order
        {
            get { return db_Order; }
            set
            {
                db_Order = value;
                NotifyPropertyChanged();
            }
        }

        bool internal_IsSelected { get; set; }
        public bool IsSelected
        {
            get { return internal_IsSelected; }
            set
            {
                internal_IsSelected = value;
                NotifyPropertyChanged();
            }
        }

        public Model()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
