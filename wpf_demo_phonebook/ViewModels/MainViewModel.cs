using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        public RelayCommand AddContactCommand { get; set; }
        #endregion

        //-----------------------------------------------------------------------Constructeurs


        public MainViewModel()
        {
            SearchContactCommand = new RelayCommand(SearchContact);   //Fait search by ID || name et met dans Contacts+Listview et SelectedContact+FicheGauche
            GetAllCommand = new RelayCommand(ShowAllContact);         //Fait get de tous les contact et met dans Contacts+Listview et SelectedContact+FicheGauche
            UpdateContactCommand = new RelayCommand(UpdateContact);   //Fait un enregistrement du SelectedContact de FicheGauche (update si flag=false) (insert si flag=true)
            DeleteContactCommand = new RelayCommand(DeleteContact);   //Fait une supression du SelectedContact de FicheGauche
            AddContactCommand = new RelayCommand(AddContact);         //Fait un newContact de FicheGauche avec flag=true

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


        //(ok) -->Methode qui fait soit un Update d'un contact dans la BD
        //(ok) -->Methoque qui fait soit un Insert d'un contact dans la BD
        private void UpdateContact(object parameter)
        {
            try
            {
                #region (ok) -->Partie qui fait le update si pas un newContact(Flag=false)
                if (!SelectedContact.Flag)
                {
                    int nbModif = PhoneBookBusiness.UpdateContact(SelectedContact);
                    Debug.WriteLine("Nb de contact modifier : " + nbModif);
                }
                #endregion

                #region (ok) -->Partie qui fait insert si c'est un newContact(Flag=true) 
                else if (SelectedContact.Flag)
                {
                    if (SelectedContact.FirstName == null)
                        SelectedContact.FirstName = "Empty";
                    if (SelectedContact.LastName == null)
                        SelectedContact.LastName = "Empty";
                    if (SelectedContact.Email == null)
                        SelectedContact.Email = "Empty";
                    if (SelectedContact.Phone == null)
                        SelectedContact.Phone = "Empty";
                    if (SelectedContact.Mobile == null)
                        SelectedContact.Mobile = "Empty";
                    SelectedContact.Flag = false;

                    int newGeneratedID = PhoneBookBusiness.AddContact(SelectedContact);
                    if (newGeneratedID > 0)
                    {
                        SelectedContact.ContactID = newGeneratedID;
                        Contacts.Add(SelectedContact);

                        SelectedContact = Contacts.Last<ContactModel>();
                    }
                }
                #endregion
            }
            catch
            {
                Debug.WriteLine("Impossible D'enregistrer");
            }
                
        }


        //(ok) -->Methode qui fait un Delete d'un contact dans la BD
        private void DeleteContact(Object Parameter)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
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





        //En essai******************************
        //() -->Methode qui ajoute un contact a la bd
        private void AddContact(Object parameter)
        {
            //Creation d'un new ContactModel
            ContactModel newContact = new ContactModel();
            newContact.Flag = true;
            

            SelectedContact = newContact;
        }

    }
}
