using System;

namespace Knowledge.Backend.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreateDate { get; set; }
        DateTime? LastUpdated { get; set; }
    }
}
