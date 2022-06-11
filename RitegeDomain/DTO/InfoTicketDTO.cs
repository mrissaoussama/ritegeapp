using System;
using System.ComponentModel;

namespace RitegeDomain.DTO
{
    [Serializable]
    public enum TypeTicket { [Description("Stationnement")] TicketStationnement };
    [Serializable]
    public class InfoTicketDTO:IEntity
    {
        string borneEntree,codeTicket;string?  borneSortie;
        TypeTicket typeTicket;
        Decimal montantPaye;
        DateTime? dateHeureSortie; DateTime dateHeureEntree;

        public InfoTicketDTO() { }
     
        public string CodeTicket { get => codeTicket; set => codeTicket = value; }
        public string BorneEntree { get => borneEntree; set => borneEntree = value; }
        public string? BorneSortie { get => borneSortie; set => borneSortie = value; }
        public decimal MontantPaye { get => montantPaye; set => montantPaye = value; }
        public DateTime? DateHeureSortie { get => dateHeureSortie; set => dateHeureSortie = value; }
        public DateTime DateHeureEntree { get => dateHeureEntree; set => dateHeureEntree = value; }
        public TypeTicket TypeTicket { get => typeTicket; set => typeTicket = value; }
    }
}
