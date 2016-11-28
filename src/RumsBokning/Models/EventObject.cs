using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RumsBokning.Models
{
    public class EventObject
    {
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
        public string Description { get; set; }

        public EventObject(string Title, DateTime? Start, DateTime? End, string Description)
        {
            this.Title = Title;
            this.Start = Start;
            this.End = End;
            AllDay = false;
            this.Description = Description;
        }

        public EventObject()
        {

        }
    }
}
