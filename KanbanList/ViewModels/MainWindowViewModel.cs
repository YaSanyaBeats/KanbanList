using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanList.Models;
using ReactiveUI;
using System.Reactive;
using Avalonia.Media.Imaging;
using System.IO;

namespace KanbanList.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            AddTodoTask = ReactiveCommand.Create((string status) => addTodoTask(status));
            DeleteTodoTask = ReactiveCommand.Create((KanbanTask kanbanTask) => deleteTodoTask(kanbanTask));
        }
        private ObservableCollection<KanbanTask> todoTasks = new ObservableCollection<KanbanTask>();
        public ObservableCollection<KanbanTask> TodoTasks
        {
            get => todoTasks;
            set
            {
                this.RaiseAndSetIfChanged(ref todoTasks, value);
            }
        }
        private ObservableCollection<KanbanTask> processTasks = new ObservableCollection<KanbanTask>();
        public ObservableCollection<KanbanTask> ProcessTasks
        {
            get => processTasks;
            set
            {
                this.RaiseAndSetIfChanged(ref processTasks, value);
            }
        }
        private ObservableCollection<KanbanTask> doneTasks = new ObservableCollection<KanbanTask>();
        public ObservableCollection<KanbanTask> DoneTasks
        {
            get => doneTasks;
            set
            {
                this.RaiseAndSetIfChanged(ref doneTasks, value);
            }
        }
        public ReactiveCommand<string, Unit> AddTodoTask { get; }
        private void addTodoTask(string Status)
        {
            if (Status == "todo")
            {
                TodoTasks.Add(new KanbanTask("todo"));
            }
            else if(Status == "process")
            {
                ProcessTasks.Add(new KanbanTask("process"));
            }
            else if(Status == "done")
            {
                DoneTasks.Add(new KanbanTask("done"));
            }
        }
        public ReactiveCommand<KanbanTask, Unit> DeleteTodoTask { get; }
        private void deleteTodoTask(KanbanTask kanbanTask)
        {
            if (kanbanTask.Status == "todo")
            {
                TodoTasks.Remove(kanbanTask);
            }
            else if (kanbanTask.Status == "process")
            {
                ProcessTasks.Remove(kanbanTask);
            }
            else if (kanbanTask.Status == "done")
            {
                DoneTasks.Remove(kanbanTask);
            }
        }
        public void Save(string path)
        {
            File.WriteAllText(path, "");
            List<string> fileData = new List<string>();
            foreach (KanbanTask task in TodoTasks)
            {
                fileData.Add(task.Name);
                fileData.Add(task.Description);
                fileData.Add(task.Status);
                fileData.Add(task.ImagePath);
            }
            foreach (KanbanTask task in ProcessTasks)
            {
                fileData.Add(task.Name);
                fileData.Add(task.Description);
                fileData.Add(task.Status);
                fileData.Add(task.ImagePath);
            }
            foreach (KanbanTask task in DoneTasks)
            {
                fileData.Add(task.Name);
                fileData.Add(task.Description);
                fileData.Add(task.Status);
                fileData.Add(task.ImagePath);
            }
            File.WriteAllLines(path, fileData);
        }
        public void Load(string path)
        {
            ObservableCollection<KanbanTask> currentTodoTasks = new ObservableCollection<KanbanTask>();
            ObservableCollection<KanbanTask> currentProcessTasks = new ObservableCollection<KanbanTask>();
            ObservableCollection<KanbanTask> currentDoneTasks = new ObservableCollection<KanbanTask>();
            StreamReader file = new StreamReader(path);
            try
            {
                TodoTasks.Clear();
                ProcessTasks.Clear();
                DoneTasks.Clear();

                while (!file.EndOfStream)
                {
                    KanbanTask currentTask = new KanbanTask();
                    currentTask.Name = file.ReadLine();
                    currentTask.Description = file.ReadLine();
                    currentTask.Status = file.ReadLine();
                    currentTask.ImagePath = file.ReadLine();
                    currentTask.Image = new Bitmap(currentTask.ImagePath);
                    if(currentTask.Status == "todo")
                    {
                        currentTodoTasks.Add(currentTask);
                    }
                    else if(currentTask.Status == "process")
                    {
                        currentProcessTasks.Add(currentTask);
                    }
                    else if(currentTask.Status == "done")
                    {
                        currentDoneTasks.Add(currentTask);
                    }
                }
                file.Close();

                TodoTasks = currentTodoTasks;
                ProcessTasks = currentProcessTasks;
                DoneTasks = currentDoneTasks;
            }
            catch
            {
                file.Close();
            }
        }
    }
}
