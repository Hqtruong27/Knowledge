using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Common.Enums
{
    public enum UserErr
    {
        [Display(Name = "Users could not be found")] GETS404,
        [Display(Name = "User could not be found")] GET404,
        [Display(Name = "There was an error when creating, please contact admin")] POST500,
        [Display(Name = "Data is not complete")] PUT400,
        [Display(Name = "There was an error when updating, please contact admin")] PUT500,
        [Display(Name = "There was an error deleting this user")] DELETE406,
    }
}
