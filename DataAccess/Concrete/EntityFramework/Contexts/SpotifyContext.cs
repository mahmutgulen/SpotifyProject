using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class SpotifyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"");

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<PlayListItem> PlayListItems { get; set; }
        public DbSet<SongRepository> SongRepository { get; set; }
        public DbSet<PlayListsIFollow> PlayListsIFollows { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Library> Library  { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Follow> Follows{ get; set; }


    }
}
