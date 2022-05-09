using System;
using System.ComponentModel;

namespace RitegeDomain.DTO
{
    [Serializable]
    public enum TypeTicket { [Description("Stationnement")] TicketStationnement };
    [Serializable]
    public class InfoTicketDTO
    {
        string codeTicket, borneEntree;
        TypeTicket typeTicket;
        Decimal montantPaye;
        DateTime dateHeureSortie, dateHeureEntree;

        public InfoTicketDTO(string codeTickets, string borneEntree, decimal montantPaye, DateTime dateHeureSortie, DateTime dateHeureEntree)
        {
            CodeTicket = codeTickets;
            BorneEntree = borneEntree;
            MontantPaye = montantPaye;
            DateHeureSortie = dateHeureSortie;
            DateHeureEntree = dateHeureEntree;
        }
        public InfoTicketDTO() { }
        public InfoTicketDTO(string codeTickets, string borneEntree, decimal montantPaye, DateTime dateHeureSortie, DateTime dateHeureEntree, TypeTicket typeTicket)
        {
            CodeTicket = codeTickets;
            BorneEntree = borneEntree;
            MontantPaye = montantPaye;
            DateHeureSortie = dateHeureSortie;
            DateHeureEntree = dateHeureEntree;
            TypeTicket = typeTicket;
        }
        public string CodeTicket { get => codeTicket; set => codeTicket = value; }
        public string BorneEntree { get => borneEntree; set => borneEntree = value; }
        public decimal MontantPaye { get => montantPaye; set => montantPaye = value; }
        public DateTime DateHeureSortie { get => dateHeureSortie; set => dateHeureSortie = value; }
        public DateTime DateHeureEntree { get => dateHeureEntree; set => dateHeureEntree = value; }
        public TypeTicket TypeTicket { get => typeTicket; set => typeTicket = value; }
    }
}
