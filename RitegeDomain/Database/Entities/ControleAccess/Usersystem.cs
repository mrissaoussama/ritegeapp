// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RitegeDomain.Database.Entities.ControleAccess
{
    // usersystem
    /// <summary>
    /// controleaccessdb.usersystem
    /// </summary>
    public class Usersystem : IEntity
    {
        public short UserCode { get; set; } // userCode (Primary key)
        public string FirstName { get; set; } // firstName (length: 25)
        public string LastName { get; set; } // lastName (length: 25)
        public string Icn { get; set; } // icn (length: 8)
        public string Tel1 { get; set; } // tel1 (length: 25)
        public string Tel2 { get; set; } // tel2 (length: 25)
        public string Email { get; set; } // email (length: 55)
        public string Departement { get; set; } // departement (length: 25)
        public bool Active { get; set; } // active
        public string AccessMode { get; set; } // accessMode (length: 25)
        public bool? SiteAccessCard { get; set; } // siteAccessCard
        public string NumAccessCard { get; set; } // numAccessCard (length: 11)
        public DateTime? StartValidateDate { get; set; } // startValidateDate
        public DateTime? EndValidateDate { get; set; } // endValidateDate
        public string Picture { get; set; } // picture (length: 150)
        public short? UserApplication { get; set; } // userApplication
        public bool Sync { get; set; } // sync
        public string Fonction { get; set; } // Fonction (length: 25)
        public string Alias { get; set; } // Alias (length: 16)
        public string Adresse { get; set; } // Adresse (length: 50)
        public string PinCode { get; set; } // PinCode (length: 4)
        public int? ControllerSyncStatus { get; set; } // ControllerSyncStatus
        public short? CodeProfil { get; set; } // CodeProfil
        public bool? ActiveDate { get; set; } // ActiveDate
    }

}
// </auto-generated>
