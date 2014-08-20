using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFBalda
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public void Insert(Client client)
        {
            RowMapper<Client> rowMapper = RowMapper;
            ActiveRecord<Client> dao = new ActiveRecord<Client>("User", rowMapper);
           dao.Insert(client);
        }

        private Client RowMapper(OleDbDataReader reader)
        {
            Client user = new Client();
            user.Nickname = reader["Nickname"] as string;
            user.FirstName = reader["Name"] as string;
            user.SecondName = reader["Sername"] as string;
            return user;
        }

        public Client Select(string id)
        {
            RowMapper<Client> rowMapper = RowMapper;
            ActiveRecord<Client> dao = new ActiveRecord<Client>("User", rowMapper);
            return dao.Select(id);
        }
    }
}
