using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma_Project.Entity_lib.Models;

namespace Diploma_Project.Entity_lib
{
    public static class BusinessLogic
    {
        static public EntitiesContext DB { get; set; }
        #region Client
        static public void RegisterClient(Client client)
        {
            DB.Clients.Add(client);
            DB.SaveChanges();
        }
        static public void RegisterClient(string password, string fullName, string email, string numberPhone)
        {
            Client client = new Client()
            {
                FullName = fullName,
                Email = email,
                Password=password,
                NumberPhone = numberPhone,               
            };
            RegisterClient(client);
        }
        static public void EditClient(Client client)
        {
            DB.Clients.Update(client);
            DB.SaveChanges();
        }
        static public void EditClient(int clientId, string login, string password, string fullName, string email, string numberPhone)
        {
            Client client=  DB.Clients.Find(clientId);
            client.Login = login;
            client.Password = password;
            client.FullName = fullName;
            client.Email = email;
            client.NumberPhone = numberPhone;
            EditClient(client);
        }
        #endregion

        #region Manager
        static public void RegisterManager(Manager Manager)
        {
            DB.Managers.Add(Manager);
            DB.SaveChanges();
        }
        static public void RegisterManager(string login, string password, string fullName, string numberPhone)
        {
            Manager Manager = new Manager()
            {
                FullName = fullName,
                Login = login,
                Password = password,
                NumberPhone = numberPhone,
            };
            RegisterManager(Manager);
        }
        static public void EditManager(Manager Manager)
        {
            DB.Managers.Update(Manager);
            DB.SaveChanges();
        }
        static public void EditManager(int ManagerId, string login, string password, string fullName, string numberPhone)
        {
            Manager Manager = DB.Managers.Find(ManagerId);
            Manager.Login = login;
            Manager.Password = password;
            Manager.FullName = fullName;
            Manager.NumberPhone = numberPhone;
            EditManager(Manager);
        }
        #endregion
        #region Order

        #endregion
        #region Point

        #endregion
        #region Product

        #endregion
        #region ProductCategory

        #endregion
        #region Seller

        #endregion
        #region ShopCart

        #endregion
        #region Status

        #endregion
        #region Users
        static public User AuthUser(string login,string password)
        {
           return DB.Users.FirstOrDefault(u => u.Password == password && u.Login == login);
        }
        #endregion
        #region Operations

        #endregion
        #region TypeOperations

        #endregion

    }
}
