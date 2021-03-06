﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirstEntityFramework.Models
{
    public class CodeFirstEntityFrameworkContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CodeFirstEntityFrameworkContext() : base("name=CodeFirstEntityFrameworkContext")
        {
            Database.SetInitializer<CodeFirstEntityFrameworkContext>(new DropCreateDatabaseIfModelChanges<CodeFirstEntityFrameworkContext>());
        }

        public System.Data.Entity.DbSet<CodeFirstEntityFramework.Models.School> Schools { get; set; }

        public System.Data.Entity.DbSet<CodeFirstEntityFramework.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<CodeFirstEntityFramework.Models.SchoolClass> SchoolClasses { get; set; }

        public System.Data.Entity.DbSet<CodeFirstEntityFramework.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<CodeFirstEntityFramework.Models.Student> Students { get; set; }
    }
}
