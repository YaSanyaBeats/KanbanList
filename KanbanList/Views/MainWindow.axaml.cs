using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using KanbanList.ViewModels;
using KanbanList.Models;
using Avalonia.Media.Imaging;

namespace KanbanList.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FindControl<MenuItem>("ExitButton").Click += delegate
            {
                Close();
            };

            this.FindControl<MenuItem>("NewButton").Click += delegate
            {
                var context = this.DataContext as MainWindowViewModel;
                context.TodoTasks.Clear();
                context.ProcessTasks.Clear();
                context.DoneTasks.Clear();
            };

            this.FindControl<MenuItem>("SaveButton").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.Save(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("LoadButton").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Search File",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);

                string[]? filePath = await taskPath;

                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;
                    context.Load(string.Join(@"\", filePath));
                }
            };
        }

        public async void AddImage(object sender, RoutedEventArgs e)
        {
            KanbanTask task = (KanbanTask)((Button)sender).DataContext;
            var taskPath = new OpenFileDialog()
            {
                Title = "Search File",
                Filters = null
            }.ShowAsync((Window)this.VisualRoot);

            string[]? filePath = await taskPath;

            if (filePath != null)
            {
                task.Image = new Bitmap(filePath[0]);
                task.ImagePath = filePath[0];
            }
        }
        private async void OpenAbout(object control, RoutedEventArgs arg)
        {
            await new About().ShowDialog((Window)this.VisualRoot);
        }
    }
}
