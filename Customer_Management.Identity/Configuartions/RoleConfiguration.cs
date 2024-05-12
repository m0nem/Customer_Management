using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Identity.Configuartions
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
                (
                    new IdentityRole
                    {
                        Id = "BBAFFF6D-54B0-4922-9189-925032A8EFF0",
                        Name = "Customer",
                        NormalizedName = "CUSTOMER"
                    },
                   new IdentityRole
                   {
                       Id = "769503B1-AFCD-42D0-95C5-5EE76A5ACB6E",
                       Name = "Administrator",
                       NormalizedName = "Admin"
                   }

                );
        }
    }
}
