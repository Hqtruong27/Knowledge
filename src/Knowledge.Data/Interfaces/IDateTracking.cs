using System;

namespace Knowledge.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreateDate { get; set; }
        DateTime? LastUpdated { get; set; }
    }
}
