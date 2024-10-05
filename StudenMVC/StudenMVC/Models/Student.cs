using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; // Import IFormFile

namespace StudenMVC.Models;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; } // Change DateOnly to DateTime for compatibility

    // This will store the filename of the image in the database
    public string? Image { get; set; }

    // This property will not be saved to the database, it is just for receiving the file from the form
    [NotMapped]
    [DataType(DataType.Upload)]
    public IFormFile? ImageFile { get; set; } // Nullable, because it may not always be uploaded

    public virtual Class Class { get; set; } = null!;

    
}
