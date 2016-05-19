﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SqlBulkCopyExtensions.Test.Model.Configurations
{
    public class AccessLogConfiguration : EntityTypeConfiguration<AccessLog>
    {
        public AccessLogConfiguration()
        {
            ToTable("AccessLog");
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}