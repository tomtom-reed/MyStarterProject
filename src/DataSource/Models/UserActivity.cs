using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataSource.Models;

[Table("UserActivity")]
public class UserActivity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActivityType { get; set; }
    public DateTime ActivityDate { get; set; }
}