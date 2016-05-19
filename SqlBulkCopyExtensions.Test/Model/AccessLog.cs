using System;

namespace SqlBulkCopyExtensions.Test.Model
{
    public class AccessLog
    {
        //Original position
        //public virtual int Id { get; set; }
        //public virtual string Ip { get; set; }
        //public virtual DateTime Date { get; set; }
        //public virtual string FullName { get; set; }

        public virtual IpType IpType { get; set; }
        public virtual MachineType? MachineType { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Ip { get; set; }        
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }

    }

    public enum IpType
    {
        Lan = 1,
        Wan
    }

    public enum MachineType
    {
        PC = 1,
        Phone
    }
}