﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using SqlBulkCopyExtensions.Test.Model;

namespace SqlBulkCopyExtensions.Test
{
    public static class TestHelper
    {
        public static ModelContext GetDbContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString;
            return new ModelContext(connectionString);
        }

        public static IEnumerable<AccessLog> GenerateAccessLogList(int numberOfItems)
        {
            var list = new Collection<AccessLog>();
            for (var number = 1; number <= numberOfItems; number++)
            {
                list.Add(new AccessLog()
                {
                    Id = number,
                    Ip = "127.0.0.1",
                    Date = GetRandomDateTime(),
                    FullName = "Full name",
                    IpType = IpType.Lan,
                    MachineType = number % 2 == 0 ? (MachineType?)MachineType.PC : null
                });
            }
            return list;
        }

        private static DateTime GetRandomDateTime()
        {
            var start = new DateTime(2010, 1, 1);
            var gen = new Random();
            var range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
