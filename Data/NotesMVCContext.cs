using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesMVC.Models;

namespace NotesMVC.Data
{
    public class NotesMVCContext : DbContext
    {
        public NotesMVCContext (DbContextOptions<NotesMVCContext> options)
            : base(options)
        {
        }

        public DbSet<NotesMVC.Models.Note> Note { get; set; } = default!;
    }
}
