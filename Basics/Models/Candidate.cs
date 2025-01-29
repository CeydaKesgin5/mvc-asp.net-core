namespace Basics.Models
{
    public class Candidate
    {
        public String? Email {get; set;}=String.Empty;
        public String? FirstName {get; set;}=String.Empty;
        public String? LastName {get; set;}=String.Empty;
        public String? FullName=>$"{FirstName} {LastName?.ToUpper()}";
        public String? SelectedCourse {get; set;}=String.Empty;
        public int? Age {get; set;}
        public DateTime? ApplyAt {get; set;}

        public Candidate(){
            ApplyAt=DateTime.Now;
        }



    } 
}