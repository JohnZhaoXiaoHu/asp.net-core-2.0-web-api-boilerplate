using System;

namespace SharedSettings.Models
{
    public class FullCalendarEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public bool Editable { get; set; }
        public bool StartEditable { get; set; }
        public bool DurationEditable { get; set; }
        public bool ResourceEditable { get; set; }
        public string Rendering { get; set; }
        public bool Overlap { get; set; }
        public string Constraint { get; set; }
        // public dynamic Source { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
    }
}
