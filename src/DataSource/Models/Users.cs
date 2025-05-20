using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataSource.Models;

/// <summary>
/// Sample model class for users.
/// </summary>
[Table("Users")]
public class Users : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<UserActivity> Activities { get; set; } = [];
}