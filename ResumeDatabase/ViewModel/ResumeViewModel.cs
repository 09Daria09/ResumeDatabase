using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ResumeDatabase.Model;
using ResumeDatabase.Commands;
using System.Windows.Input;

namespace ResumeDatabase.ViewModel
{
    public class ResumeViewModel : INotifyPropertyChanged
    {
        private Resume _resume;

        public ResumeViewModel()
        {
            _resume = new Resume();
            Skills = new ObservableCollection<string>(_resume.Skills ?? new List<string>());
            DeleteSkillCommand = new DelegateCommand(DeleteSkillExecute, DeleteSkillCanExecute);
        }
        public string FullName
        {
            get => _resume.FullName;
            set
            {
                if (_resume.FullName != value)
                {
                    _resume.FullName = value;
                    OnPropertyChanged();
                }
            }
        }
        public int? Age
        {
            get => _resume.Age;
            set
            {
                if (_resume.Age != value)
                {
                    _resume.Age = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MaritalStatus
        {
            get => _resume.MaritalStatus;
            set
            {
                if (_resume.MaritalStatus != value)
                {
                    _resume.MaritalStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Address
        {
            get => _resume.Address;
            set
            {
                if (_resume.Address != value)
                {
                    _resume.Address = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _resume.Email;
            set
            {
                if (_resume.Email != value)
                {
                    _resume.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newSkill;
        public string NewSkill
        {
            get { return _newSkill; }
            set
            {
                if (_newSkill != value)
                {
                    _newSkill = value;
                    OnPropertyChanged(nameof(NewSkill));
                }
            }
        }

        private string _selectedSkill;
        public string SelectedSkill
        {
            get => _selectedSkill;
            set
            {
                if (_selectedSkill != value)
                {
                    _selectedSkill = value;
                    OnPropertyChanged(nameof(SelectedSkill));

                    NewSkill = _selectedSkill;
                    OnPropertyChanged(nameof(NewSkill));
                }
            }
        }
        public ObservableCollection<string> Skills { get; set; }
        public ICommand _deleteSkillCommand { get; private set; }
        public ICommand DeleteSkillCommand {
            get => _deleteSkillCommand;
            set
            {
                if( _deleteSkillCommand != value)
                {
                    _deleteSkillCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool DeleteSkillCanExecute(object parameter)
        {
            return parameter != null;
        }

        private void DeleteSkillExecute(object parameter)
        {
            var skill = parameter as string;
            if (!string.IsNullOrEmpty(skill) && Skills.Contains(skill))
            {
                Skills.Remove(skill);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ResumeViewModel Clone()
        {
            return new ResumeViewModel
            {
                FullName = this.FullName,
                Email = this.Email,
                Age = this.Age,
                MaritalStatus = this.MaritalStatus,
                Address = this.Address,
                Skills = new ObservableCollection<string>(this.Skills)
            };
        }

    }
}

