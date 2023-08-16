﻿using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IBookingRepository   : IGenericRepository<Booking>
{
    Task<List<Booking>> GetAllBookingByCustomerId(Guid customerId);
    Task<List<Booking>> GetAllBooking();
}