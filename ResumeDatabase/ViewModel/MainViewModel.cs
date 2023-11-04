using ResumeDatabase.Commands;
using ResumeDatabase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResumeDatabase.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ResumeViewModel> _resumes { get; private set; }
        public ObservableCollection<ResumeViewModel> Resumes
        {
            get { return _resumes; }
            set
            {
                if (_resumes != value)
                {
                    _resumes = value;
                    OnPropertyChanged(nameof(Resumes));
                }
            }
        }

        public ICommand _resetFieldsCommand { get; private set; }
        public ICommand ResetFieldsCommand
        {
            get => _resetFieldsCommand;
            set
            {
                if (_resetFieldsCommand != value)
                {
                    _resetFieldsCommand = value;
                    OnPropertyChanged(nameof(ResetFieldsCommand));
                }
            }
        }

        public ICommand _addSkillCommand { get; private set; }
        public ICommand AddSkillCommand
        {
            get => _addSkillCommand;
            set
            {
                if (_addSkillCommand != value)
                {
                    _addSkillCommand = value;
                    OnPropertyChanged(nameof(AddSkillCommand));
                }
            }
        }

        public ICommand _addResumeCommand { get; private set; }
        public ICommand AddResumeCommand
        {
            get { return _addResumeCommand; }
            set
            {
                if (_addResumeCommand != value)
                {
                    _addResumeCommand = value;
                    OnPropertyChanged(nameof(AddResumeCommand));
                }
            }
        }

        public ICommand _deleteResumeCommand { get; private set; }
        public ICommand DeleteResumeCommand
        {
            get
            {
                if (_deleteResumeCommand == null)
                {
                    _deleteResumeCommand = new DelegateCommand(
                        execute: (obj) =>
                        {
                            if (obj is ResumeViewModel ToDelete)
                            {
                                Resumes.Remove(ToDelete);
                            }
                        }, null);
                }
                return _deleteResumeCommand;
            }
        }

        public ICommand _viewResumeCommand { get; private set; }
        public ICommand ViewResumeCommand
        {
            get => _viewResumeCommand;
            set
            {
                if (_viewResumeCommand != value)
                {
                    _viewResumeCommand = value;
                    OnPropertyChanged(nameof(ViewResume));
                }
            }
        }
        private ResumeViewModel _selectedResume;
        public ResumeViewModel SelectedResume
        {
            get => _selectedResume;
            set
            {
                if (_selectedResume != value)
                {
                    _selectedResume = value;
                    OnPropertyChanged();
                }
            }
        }
        public ResumeViewModel _resume { get; private set; }
        public ResumeViewModel Resume
        {
            get => _resume;
            set
            {
                if (value != _resume)
                {
                    _resume = value;
                    OnPropertyChanged();
                }
            }
        }

        private ViewResume detailsWindow;
        public MainViewModel()
        {
            Resumes = new ObservableCollection<ResumeViewModel>();

            ViewResumeCommand = new DelegateCommand(ViewResume, CanViewResume);

            ResetFieldsCommand = new DelegateCommand(ResetFields, CanResetFields);
            AddSkillCommand = new DelegateCommand(AddSkill, CanAddSkill);
            AddResumeCommand = new DelegateCommand(AddResume, CanAddResume);
            Resume = new ResumeViewModel();
        }

        private void AddResume(object parameter)
        {
            if (Resume.Age <= 16)
            {
                MessageBox.Show("Для добавления резюме необходимо достичь 16-летнего возраста.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var resumeToAdd = Resume.Clone();
            Resumes.Add(resumeToAdd);
            ResetFields(parameter);
            Resume = new ResumeViewModel();
        }
        private bool CanAddResume(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Resume.FullName)
                && !string.IsNullOrWhiteSpace(Resume.Email)
                && Resume.Age > 0
                && !string.IsNullOrWhiteSpace(Resume.MaritalStatus)
                && !string.IsNullOrWhiteSpace(Resume.Address);
        }

        bool CanResetFields(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Resume.FullName) ||
           !string.IsNullOrWhiteSpace(Resume.Email) ||
            Resume.Age > 0 ||
           !string.IsNullOrWhiteSpace(Resume.MaritalStatus) ||
           !string.IsNullOrWhiteSpace(Resume.Address);
        }
        private void ResetFields(object parameter)
        {
            Resume.FullName = string.Empty;
            Resume.Age = null;
            Resume.MaritalStatus = string.Empty;
            Resume.Address = string.Empty;
            Resume.Email = string.Empty;
            Resume.Skills.Clear();
        }

        bool CanAddSkill(object parameter)
        {
            return !string.IsNullOrEmpty(Resume.NewSkill);
        }
        private void AddSkill(object parameter)
        {
            Resume.Skills.Add(Resume.NewSkill);
            Resume.NewSkill = string.Empty;
        }

        bool CanDeleteResume(object parameter)
        {
            return true;
        }
        private void DeleteResume(object parameter)
        {
            if (parameter is ResumeViewModel resumeToDelete)
            {
                Resumes.Remove(resumeToDelete);
            }
        }
        private bool CanViewResume(object parameter)
        {
            return SelectedResume != null;
        }


        private void ViewResume(object parameter)
        {
            var selectedResume = SelectedResume;

            detailsWindow = new ViewResume
            {
               DataContext = selectedResume
            };

            detailsWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
