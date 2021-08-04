using FoccoEmFrente.Kanban.Application.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FoccoEmFrente.Kanban.Application.Entities
{
    public class Activity : Entity, IAggregateRoot
    {        
        public Activity() { }

        public string Title { get; set; }

        public ActivityStatus Status { get; set; }

        public Guid UserId { get; set; }

        public int Order { get; set; }
    }
}
