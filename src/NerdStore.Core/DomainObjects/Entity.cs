﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
         
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();


        protected Entity()
        {
            Id = Guid.NewGuid();

        }

        public void AddEvent(Event myEvent)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(myEvent);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void CleanEvent()
        {
            _notifications?.Clear();
        }

        public override bool Equals(object? obj)
        {
            // compare two objects.
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);

        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            // prevent duplicated values * random number 907 + ID
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {

            return $"{GetType().Name} [Id={Id}]";

        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
         

    }
}
