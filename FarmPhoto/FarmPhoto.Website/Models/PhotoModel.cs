using System;
using FarmPhoto.Domain;
using System.Collections.Generic;
using FarmPhoto.Website.Attributes;

namespace FarmPhoto.Website.Models
{
    public class PhotoModel
    {
        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public IList<string> Tags { get; set; }

        [LocalisedDisplayName("Tags")]
        public string TagString { get; set; }

        public string Description { get; set; }

        public Rating Rating { get; set; }

        public string SubmittedBy { get; set; }

        public bool Approved { get; set; }

        public string Height { get; set; }

        public string Width { get; set; }

        public string FileName { get; set; }

        public DateTime SubmittedOn { get; set; }
    }
}