using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Identity.Configuartions
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string> 
                {
                    UserId= "F46E70D6-B925-4639-983C-18B2699042F2",
                    RoleId= "769503B1-AFCD-42D0-95C5-5EE76A5ACB6E"
                }, 
                new IdentityUserRole<string>
                {
                    UserId = "00368991-49FE-44A5-94F9-5ECAC762A420",
                    RoleId = "769503B1-AFCD-42D0-95C5-5EE76A5ACB6E"
                }


                );
        }
    }
}
