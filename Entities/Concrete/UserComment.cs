
using Core.Entities;
using System;

namespace Entities.Concrete;

public class UserComment:IEntity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public int UserId { get; set; }
    public int CommenterId { get; set; }

    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserComment()
    {
        CreatedAt = DateTime.Now;
    }
}
