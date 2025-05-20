using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSource.Models;

/// <summary>
/// Base model class for all entities.
/// This class contains common properties that are shared across all entities.
/// It is used to enforce a consistent structure and behavior for all entities.
/// The class includes properties for tracking the creation and modification of entities,
/// as well as properties for soft deletion.
/// </summary>
public abstract class BaseModel
{
    public virtual Guid Id { get; set; }
    
    public virtual Guid CreatedBy { get; set; }
    
    public virtual DateTime CreatedAt { get; set; }
    
    public virtual Guid ModifiedBy { get; set; }
    
    public virtual DateTime UpdatedAt { get; set; }

    public virtual bool IsDeleted { get; set; } = false;

    public virtual DateTimeOffset? DeletedUtc { get; set; } = null;

    public virtual Guid? DeletedBy { get; set; } = null;
}