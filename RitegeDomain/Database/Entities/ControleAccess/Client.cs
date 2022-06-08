using System;
using System.Collections.Generic;
using System.Text;

namespace RitegeDomain.Database.Entities.ControleAccess
{
    public class Client:IEntity
    {
        public int IdClient { get; set; }
        public string Email { get; set; }
        public string MotDePasse{get;set;}
        public int IdSociete { get; set; }
    }
}
