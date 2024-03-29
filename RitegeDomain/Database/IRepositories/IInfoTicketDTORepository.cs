﻿using RitegeDomain.DTO;

namespace RitegeDomain.Database.IRepositories;
public interface IInfoTicketDTORepository : IRepository<InfoTicketDTO>
{

    public Task<IEnumerable<InfoTicketDTO?>> GetAllByDatesAsync(DateTime dateStart, DateTime dateEnd,int idParking);




}