using App.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using wpf_demo_phonebook.ViewModels;

namespace wpf_demo_phonebook
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //-------------------------------------------------------------Variables

        #region -->Variables
        private ContactModel selectedContact;

        private ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();

        //--------------Definitions Variables

        public ContactModel SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        #endregion

        //-------------------------------------------------------------Constructeurs

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        //-------------------------------------------------------------Méthodes

        #region -->Methodes 
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //*************************************

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }



        #endregion
    }
}
