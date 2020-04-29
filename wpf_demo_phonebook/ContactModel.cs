using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace wpf_demo_phonebook
{
    public class ContactModel : INotifyPropertyChanged
    {
        //---------------------------------------------------------------------------Variables

        #region -->Variable de contact
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        #endregion


        #region -->Variable Ajouter par JYD
        public bool Flag { get; set; }
        public string Info => $"{LastName}, {FirstName}";

        #endregion

        //---------------------------------------------------------------------------Constructeur

        public ContactModel()
        {
            Flag = false;
        }
       
        //---------------------------------------------------------------------------Methodes

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
