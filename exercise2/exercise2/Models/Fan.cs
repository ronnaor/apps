using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exercise2.Models
{
    public class Fan
    {
        static int count = 0;

        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Must be only chars")]
        [Display(Name = "First Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[\p{L}]+$", ErrorMessage = "Must be only chars")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [EnumDataType(typeof(Gender))]
        [Display(Name = "Gender:")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date:")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Seniority is required")]
        [DataType(DataType.Duration)]
        [Range(0, int.MaxValue, ErrorMessage = "Value must be positive")]
        [Display(Name = "Seniority:")]
        public int Seniority { get; set; }


        public Fan()
        {
            count++;
            ID = count;
        }

        public void copy(Fan fan)
        {
            Name = fan.Name;
            LastName = fan.LastName;
            Gender = fan.Gender;
            BirthDate = fan.BirthDate;
            Seniority = fan.Seniority;
        }

    }

    public enum Gender
    {
        Male,
        Female
    }
}