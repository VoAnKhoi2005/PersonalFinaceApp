﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceApp.Database;
using PersonalFinanceApp.Model;
using PersonalFinanceApp.Src.ViewModel.Stores;
using PersonalFinanceApp.ViewModel.Command;
using PersonalFinanceApp.ViewModel.Stores;

namespace PersonalFinanceApp.ViewModel.MainMenu;

public class GoalplanAddNewViewModel : BaseViewModel
{
    private readonly ModalNavigationStore _modalNavigationStore;
    private readonly IServiceProvider _serviceProvider;
    private readonly AccountStore _accountStore;
    private readonly GoalStore _goalStore;
    #region Properties
    //name
    public string NameGoal {
        get => _nameGoal;
        set {
            if (_nameGoal != value) {
                _nameGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _nameGoal;
    //target
    public string TargetGoal {
        get => _targetGoal;
        set {
            if (_targetGoal != value) {
                _targetGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _targetGoal;
    //current amount
    public string CurrentAmountGoal {
        get => _currentAmountGoal;
        set {
            if (_currentAmountGoal != value) {
                _currentAmountGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _currentAmountGoal;
    //resource
    public string ResourceGoal {
        get => _resourceGoal;
        set {
            if (_resourceGoal != value) {
                _resourceGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _resourceGoal;
    //reminder
    public string ReminderGoal {
        get => _reminderGoal;
        set {
            if (_reminderGoal != value) {
                _reminderGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _reminderGoal;
    //deadline
    public DateTime DeadlineGoal {
        get => _deadlineGoal;
        set {
            if (_deadlineGoal != value) {
                _deadlineGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private DateTime _deadlineGoal = DateTime.Now;

    //status
    public string StatusGoal {
        get => _statusGoal;
        set {
            if (_statusGoal != value) {
                _statusGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _statusGoal;
    //Decription
    public string DescriptionGoal {
        get => _descriptionGoal;
        set {
            if (_descriptionGoal != value) {
                _descriptionGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _descriptionGoal;
    //category
    public string CategoryGoal {
        get => _categoryGoal;
        set {
            if (_categoryGoal != value) {
                _categoryGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _categoryGoal;
    //category item source
    public ObservableCollection<string> _itemsGoal = new ObservableCollection<string> {};
    public ObservableCollection<string> ItemsGoal {
        get => _itemsGoal;
        set {
            if (_itemsGoal != value) {
                _itemsGoal = value;
                OnPropertyChanged();
            }
        }
    }
    public string SelectedItemGoal {
        get => _selectedItemGoal;
        set {
            if(value != null &&value.CompareTo("<New>") == 0) {
                CreateCategoryCommand.Execute(this);
                _selectedItemGoal = value;
                OnPropertyChanged();
            }
        }
    }
    private string _selectedItemGoal;
    #endregion
    public ICommand CreateCategoryCommand {  get; set; }
    public ICommand CancelNewGoalCommand { get; set; }
    public ICommand ConfirmNewGoalCommand { get; set; }
    public ICommand LoadDataAddNewGoalCommand { get; set; }

    public GoalplanAddNewViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _accountStore = serviceProvider.GetRequiredService<AccountStore>();
        _goalStore = serviceProvider.GetRequiredService<GoalStore>();
        _modalNavigationStore = serviceProvider.GetRequiredService<ModalNavigationStore>();

        CreateCategoryCommand = new NavigateModalCommand<GoalAddNewCategoryViewModel>(serviceProvider);

        LoadDataAddNewGoalCommand = new RelayCommand<object>(LoadItemSourceCategoryGoal);
        
        CancelNewGoalCommand = new RelayCommand<object>(CloseModal);
        ConfirmNewGoalCommand = new RelayCommand<object>(ConfirmNewGoal);
    }
    public void LoadItemSourceCategoryGoal(object parameter) {
        ItemsGoal.Clear();
        CategoryGoal = (_goalStore.NewCategory != null) ? _goalStore.NewCategory : "";
        var item = DBManager.GetAll<GoalCategory>();
        ItemsGoal.Add("<New>");
        foreach (var it in item) {
            ItemsGoal.Add(it.Name);
        }
    }
    private void CloseModal(object sender)
    {
        _modalNavigationStore.Close();
    }
    private void ConfirmNewGoal(object sender) {
        //add data to database
        //Goal
        Goal goal = new Goal() {
            Name = NameGoal,
            Target = long.Parse(TargetGoal),
            CurrentAmount = long.Parse(CurrentAmountGoal),
            Reminder = "Daily",
            Deadline = DeadlineGoal,
            Status = (long.Parse(TargetGoal) <= long.Parse(CurrentAmountGoal)) ? "Completed" : "Active",
            Resources = ResourceGoal,
            Description = DescriptionGoal,
            UserID = int.Parse(_accountStore.SharedUser[0]),
            CategoryName = CategoryGoal,

        };

        var item = DBManager.GetFirst<GoalCategory>(goalcategory => goalcategory.Name == CategoryGoal);

        DBManager.Insert(goal);

        _modalNavigationStore.Close();
    }
}