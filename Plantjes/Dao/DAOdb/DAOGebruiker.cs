﻿using Microsoft.EntityFrameworkCore;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace Plantjes.Dao.DAOdb
{
    public class DAOGebruiker : DAOGeneric
    {
        public DAOGebruiker() : base()
        {
            //ctor
        }

        public Gebruiker GetGebruikerWithEmail(string userEmail)
        {

            //var gebruiker = Context.Gebruikers.SingleOrDefault(g => g.Emailadres == userEmail);

            var gebruiker = context.Gebruikers.Include(g => g.Rol).SingleOrDefault(g => g.Emailadres == userEmail);

            return gebruiker;
        }


        public List<Gebruiker> getAllGebruikers()
        {
            var gebruiker = Context.Gebruikers.ToList();
            return gebruiker;
        }

        public void RegisterUser(string vivesNr, string firstName, string lastName, int rolid, string emailadres, string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                RolId = rolid,
                Emailadres = emailadres,
                HashPaswoord = passwordHashed
            };
            Context.Gebruikers.Add(gebruiker);
            Context.SaveChanges();
        }

        
        public bool CheckIfEmailAlreadyExists(string email)
        {
            bool result = false;
            if (GetGebruikerWithEmail(email) == null)
            {
                result = true;
            }

            return result;
        }
    }
}
