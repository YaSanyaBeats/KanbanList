using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;

namespace KanbanList.Models
{
    public class KanbanTask : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public KanbanTask(string status = "todo")
        {
            this.Name = "Name";
            this.Description = "Description";
            this.ImagePath = "../../../Assets/note.png";
            this.Image = new Bitmap(ImagePath);
            this.Status = status;
        }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        private Bitmap image;
        public string ImagePath { get; set; }
        public Bitmap Image 
        {
            get => image;
            set
            {
                image = value;
                NotifyPropertyChanged();
            }
        }
    }
}
