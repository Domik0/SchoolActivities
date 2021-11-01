//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolActivities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            this.Circles = new HashSet<Circle>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<double> Experience { get; set; }
        public Nullable<bool> AdministratorStatus { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string Name
        {
            get { return LastName + " " + FirstName[0] + "." + Patronymic[0] + "."; }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Circle> Circles { get; set; }
    }
}
