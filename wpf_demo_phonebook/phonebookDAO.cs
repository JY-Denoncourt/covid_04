using System.Data;
using System.Data.SqlClient;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        //-----------------------------------------------------------------------Variables

        private DbConnection conn;

        //-----------------------------------------------------------------------Constructeurs

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        //-----------------------------------------------------------------------Methodes

        #region -->Methodes
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }


        //(ok) -->Requete pour avoir tout contact
        public DataTable GetAll()
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] ";

            return conn.ExecuteSelectQuery(_query, null);
        }


        //(ok) -->Requete pour update de table Contacts
        public int Update(ContactModel cm, int _id)
        {
            string _query =
                $"UPDATE Contacts " +
                $"SET FirstName = '{cm.FirstName}­', " +
                    $"LastName = '{cm.LastName}', " +
                    $"Email = '{cm.Email}', " +
                    $"Phone = '{cm.Phone}', " +
                    $"Mobile = '{cm.Mobile}' " +
                $"WHERE ContactId = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteUpdateQuery(_query, parameters);
        }


        //(ok) -->Requete qui Delete un contact de table Contacts
        public int Delete(ContactModel cm, int _id)
        {
            string _query =
                $"Delete " +
                $"FROM [Contacts] " +
                $"WHERE ContactId = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteDeleteQuery(_query, parameters);
        }


        //(ok) -->Requete qui Insert un newContact dans la Table Contact
        public int Insert(ContactModel cm)
        {
            // 
            string _query =
                $"Insert " +
                $"Into [Contacts] (FirstName, LastName, Email, Phone, Mobile) " +
                $"OUTPUT INSERTED.ContactID " +
                $"Values ('{cm.FirstName}­', '{cm.LastName}', '{cm.Email}', '{cm.Phone}', '{cm.Mobile}')";

            return conn.ExecuteInsertQuery(_query, null);
        }
    }
    #endregion

}
