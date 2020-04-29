using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using wpf_demo_phonebook.ViewModels.Commands;

namespace wpf_demo_phonebook.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        //-----------------------------------------------------------------------Variables

        #region -->Variables
        private ContactModel selectedContact;

        private ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();

        private string criteria;

        #endregion

        //-----------------------------------------------------------------------Définition

        #region -->Definitions
        public ObservableCollection<ContactModel> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged();
            }
        }

        public ContactModel SelectedContact
        {
            get => selectedContact;
            set { 
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public string Criteria
        {
            get { return criteria; }
            set { 
                criteria = value;
                OnPropertyChanged();
            }
        }


        //***************ICommand-RelayCommand
        public RelayCommand SearchContactCommand { get; set; }
        public RelayCommand GetAllCommand { get; set; }
        public RelayCommand UpdateContactCommand { get; set; }
        public RelayCommand DeleteContactCommand { get; set; }
        #endregion

        //-----------------------------------------------------------------------Constructeurs


        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);
            GetAllCommand = new RelayCommand(ShowAllContact);
            UpdateContactCommand = new RelayCommand(UpdateContact);
            DeleteContactCommand = new RelayCommand(DeleteContact);

            SelectedContact = PhoneBookBusiness.GetContactByID(1);
            ShowAllContact(null);
        }

        //-----------------------------------------------------------------------Methodes

        #region -->Methodes

        //(ok) -->Methode qui fait un seach de contact par id ou name
        private void SearchContact(object parameter)
        {
            string input = parameter as string;
            int output;
            string searchMethod;
            if (!Int32.TryParse(input, out output))
                searchMethod = "name";
            else
                searchMethod = "id";


            switch (searchMethod)
            {
                case "id":
                    Contacts.Clear();
                    SelectedContact = PhoneBookBusiness.GetContactByID(output);
              
                    if (SelectedContact != null)
                        Contacts.Add(SelectedContact);
                    
                        
                    break;
                case "name":
                    Contacts = PhoneBookBusiness.GetContactByName(input);
                    if (Contacts.Count > 0)
                        SelectedContact = Contacts[0];
                    break;
                default:
                    MessageBox.Show("Unkonwn search method");
                    break;
            }

            
        }


        //(ok) -->Methode qui fait un ramasse tout les contact de la BD
        private void ShowAllContact(Object parameter)
        {
            Contacts = PhoneBookBusiness.GetAllContacts();
        }


        //(ok) -->Methode qui fait un Update d'un contact dans la BD
        private void UpdateContact(object parameter)
       {
            int nbModif = PhoneBookBusiness.UpdateContact(SelectedContact);
            Debug.WriteLine("Nb de contact modifier : " + nbModif);
       }


        //(ok) -->Methode qui fait un Delete d'un contact dans la BD
        private void DeleteContact(Object Parameter)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Debug.WriteLine("Efface");
                int nbDel = PhoneBookBusiness.DeleteContact(SelectedContact);
                Debug.WriteLine("Nb de contact modifier : " + nbDel);
                if(nbDel > 0)
                {
                    Contacts.Remove(SelectedContact);
                }
            }
            else
            {
                Debug.WriteLine("Efface pas");
            }
        }

        #endregion


    }
}
