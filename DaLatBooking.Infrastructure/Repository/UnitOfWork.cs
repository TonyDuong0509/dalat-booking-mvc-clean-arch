﻿using DaLatBooking.Application.Common.Interfaces;
using DaLatBooking.Infrastructure.Data;

namespace DaLatBooking.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IVillaRepository Villa { get; private set; }

        public IVillaNumberRepository VillaNumber { get; private set; }

        public IAmenityRepository Amenity { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            Villa = new VillaRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
            Amenity = new AmenityRepository(_context);
            Booking = new BookingRepository(_context);
            User = new UserRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
