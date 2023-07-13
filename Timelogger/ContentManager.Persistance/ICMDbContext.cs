using System;
using ContentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Persistance
{
    public interface ICMDbContext
    {

        DbSet<ContentManager.Domain.Entities.Application> Application { get; set; }
        DbSet<Models> Models { get; set; }
        DbSet<Users> Users { get; set; }
    }
}

