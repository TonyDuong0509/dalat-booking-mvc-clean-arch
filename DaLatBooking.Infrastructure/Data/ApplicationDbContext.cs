using DaLatBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DaLatBooking.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                      new Villa
                      {
                          Id = 1,
                          Name = "Phòng Gỗ",
                          Description = "Phòng gỗ của chúng tôi là một kỳ quan đích thực của sự sáng tạo và tự nhiên. " +
                          "Tấm ván gỗ tự nhiên được sử dụng cho cả tường và sàn, tạo ra một không gian ấm áp và mộc mạc. " +
                          "Chiếc giường gỗ chắc chắn được bố trí giữa phòng với ga trải giường mềm mại, " +
                          "tạo điều kiện lý tưởng cho một giấc ngủ ngon lành",
                          ImageUrl = "https://placehold.co/600x400",
                          Occupancy = 4,
                          Price = 800,
                          Sqft = 500,
                      },
                      new Villa
                      {
                        Id = 2,
                        Name = "Phòng Đá",
                        Description = "Phòng đá của chúng tôi là một biểu tượng của sức mạnh và sự vững chãi của thiên nhiên. " +
                        "Tường và sàn được lát bằng các tấm đá tự nhiên, tạo ra một không gian mát mẻ và bền vững. " +
                        "Không gian rộng rãi và thoải mái với đồ nội thất đơn giản nhưng đầy tính thẩm mỹ",
                        ImageUrl = "https://placehold.co/600x401",
                        Occupancy = 4,
                        Price = 1200,
                        Sqft = 600,
                      },
                      new Villa
                      {
                        Id = 3,
                        Name = "Phòng Thường",
                        Description = "Phòng homestay bình thường của chúng tôi được thiết kế để mang lại sự tiện nghi và thoải mái cho du khách. " +
                        "Với không gian rộng rãi và ánh sáng tự nhiên, phòng homestay mang lại cảm giác ấm cúng và thân thiện",
                        ImageUrl = "https://placehold.co/600x402",
                        Occupancy = 2,
                        Price = 400,
                        Sqft = 300,
                      }
                );

            modelBuilder.Entity<VillaNumber>().HasData(
                    new VillaNumber
                    { 
                        Villa_Number = 101,
                        VillaId = 1,
                    },
                     new VillaNumber
                     {
                         Villa_Number = 102,
                         VillaId = 1,
                     },
                      new VillaNumber
                      {
                          Villa_Number = 103,
                          VillaId = 1,
                      },
                       new VillaNumber
                       {
                           Villa_Number = 104,
                           VillaId = 1,
                       },
                        new VillaNumber
                        {
                            Villa_Number = 201,
                            VillaId = 2,
                        },
                         new VillaNumber
                         {
                             Villa_Number = 202,
                             VillaId = 2,
                         },
                          new VillaNumber
                          {
                              Villa_Number = 203,
                              VillaId = 2,
                          },
                           new VillaNumber
                           {
                               Villa_Number = 301,
                               VillaId = 3,
                           },
                            new VillaNumber
                            {
                                Villa_Number = 302,
                                VillaId = 3,
                            },
                             new VillaNumber
                             {
                                 Villa_Number = 303,
                                 VillaId = 3,
                             }
                );
            modelBuilder.Entity<Amenity>().HasData(
          new Amenity
          {
              Id = 1,
              VillaId = 1,
              Name = "Private Pool"
          }, new Amenity
          {
              Id = 2,
              VillaId = 1,
              Name = "Microwave"
          }, new Amenity
          {
              Id = 3,
              VillaId = 1,
              Name = "Private Balcony"
          }, new Amenity
          {
              Id = 4,
              VillaId = 1,
              Name = "1 king bed and 1 sofa bed"
          },

          new Amenity
          {
              Id = 5,
              VillaId = 2,
              Name = "Private Plunge Pool"
          }, new Amenity
          {
              Id = 6,
              VillaId = 2,
              Name = "Microwave and Mini Refrigerator"
          }, new Amenity
          {
              Id = 7,
              VillaId = 2,
              Name = "Private Balcony"
          }, new Amenity
          {
              Id = 8,
              VillaId = 2,
              Name = "king bed or 2 double beds"
          },

          new Amenity
          {
              Id = 9,
              VillaId = 3,
              Name = "Private Pool"
          }, new Amenity
          {
              Id = 10,
              VillaId = 3,
              Name = "Jacuzzi"
          }, new Amenity
          {
              Id = 11,
              VillaId = 3,
              Name = "Private Balcony"
          });
        }
    }
}
