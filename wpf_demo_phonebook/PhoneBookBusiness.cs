using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace wpf_demo_phonebook
{
    static class PhoneBookBusiness
    {
        //---------------------------------------------------------------Variables

        private static PhonebookDAO dao = new PhonebookDAO();



        //---------------------------------------------------------------Methodes
        
        #region -->Methodes
        //(ok) -->Recherche qui retourne 0 a N contact
        public static ObservableCollection<ContactModel> GetContactByName(string _name)
        {
            ContactModel cm = null;
            ObservableCollection<ContactModel> OC_Contacts = new ObservableCollection<ContactModel>();

            DataTable dt = new DataTable();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    OC_Contacts.Add(cm);
                }
            }

            return OC_Contacts;
        }


        public static ContactModel GetContactByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }


        //(ok) -->Prendre tout les contacts de la BD
        public static ObservableCollection<ContactModel> GetAllContacts()
        {
            ContactModel cm = null;
            ObservableCollection<ContactModel> OC_Contacts = new ObservableCollection<ContactModel>();
            DataTable dt = new DataTable();

            dt = dao.GetAll();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    OC_Contacts.Add(cm);
                }
            }
            return OC_Contacts;
        }


        //(ok) -->Faire un update de toute info(sauf ContactId) sur 1 contact de la BD
        public static int UpdateContact(ContactModel _cm)
        {
            int nbModif = 0;
            DataTable dt = new DataTable();
            int _id = _cm.ContactID;

            nbModif = dao.Update(_cm, _id);

            return nbModif;
        }


        private static ContactModel RowToContactModel(DataRow row)
        {
            ContactModel cm = new ContactModel();

            cm.ContactID = Convert.ToInt32(row["ContactID"]);
            cm.FirstName = row["FirstName"].ToString();
            cm.LastName = row["LastName"].ToString();
            cm.Email = row["Email"].ToString();
            cm.Phone = row["Phone"].ToString();
            cm.Mobile = row["Mobile"].ToString();

            return cm;
        }


        //(ok) -->Methode qui fait un delete d'un contact
        public static int DeleteContact(ContactModel _cm)
        {
            int nbDel = 0; 
            int _id = _cm.ContactID;

            nbDel = dao.Delete(_cm, _id);

            return nbDel;
        }


        #endregion








        //***********en essai ****************
        //() -->Methode qui fait un Insert d'un contact
        public static int AddContact(ContactModel _cm)
        {
            int nbAdd = 0;
            

            nbAdd = dao.Insert(_cm);

            return nbAdd;
        }

    }
}
