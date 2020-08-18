using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldiers.EF
{
    public class Soldier
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Gender { get; set; } = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string FullName
        {
            get { return SurName + " " + FirstName + " " + LastName; }
        }
        public string Search
        {
            get { return AccountNumber + " " + 
                         MilitaryRank + " " + 
                         FullName + " " +
                         RegistrationAddress + " " +
                         StudyPlace + " " +
                         WorkPlace + " " +
                         VOSnew + " " + 
                         VOSold + " " +
                         VOSzvit + " " +
                         Category + " " + 
                         YearMedical + " " + 
                         TeamNumber; }
        }
        public DateTime? BirthDate { get; set; }
        public string RegistrationAddress { get; set; }
        public string HouseAddress { get; set; }
        public string StudyPlace { get; set; }
        public string WorkPlace { get; set; }
        public bool MilitaryService { get; set; }
        public string YearServiceString { get; set; }
        public string VOSnew { get; set; }
        public string VOSold { get; set; }
        public string VOSzvit { get; set; }
        public string Category { get; set; }
        public string ProfileName { get; set; }
        public string MilitaryRank { get; set; }
        public DateTime? RankDate { get; set; }
        public DateTime? AcceptedDate { get; set; } 
        public DateTime? RemoveDate { get; set; }
        public bool OR1 { get; set; }
        public bool OR2 { get; set; }
        public bool ATO { get; set; }
        public bool UBD { get; set; }
        public string UBDName { get; set; }
        public int YearMedical { get; set; }
        public string TypeAccounting { get; set; }

        public bool AccountingTotal { get; set; }
        public bool AssignedTeam { get; set; }
        public string TeamNumber { get; set; }

        public bool AccountingOther { get; set; }
        public bool SubjectToConscription { get; set; }
        public bool Unsuitable { get; set; }
        public bool RightToDefer { get; set; }
        public string ReasonForDefer { get; set; }

        public string Color { get; set; } = "Black";        
        public bool Enabled { get; set; } = true;
    }
}
