namespace Hospital
{
    using System;
    using System.Data.Entity.Validation;
    using Hospital.Models;
    public class Program
    {
        public static void Main()
        {
            try
            {
                var context = new HospitalContext();

                using (context)
                {
                    //context.Database.Initialize(true);

                    Patient patient = new Patient
                    {
                        FirstName = "Marrika",
                        LastName = "Obstova",
                        Adress = "Luvov most No.1",
                        Email = "marrika@abv.bg",
                        DateOfBirth = new DateTime(1990, 05, 07)
                    };

                    Doctor doctor = new Doctor
                    {
                        Name = "Doctor Frankenstein",
                        Specialty = "Mad scientist"
                    };

                    Visitation visitation = new Visitation
                    {
                        Doctor = doctor,
                        Comments = "Mnoo zle",
                        Patient = patient,
                        Date = DateTime.Now
                    };

                    patient.Visitations.Add(visitation);
                    doctor.Visitations.Add(visitation);

                    Diagnose diagnose = new Diagnose
                    {
                        Name = "HIV",
                        Comments = "Ot mangalite na luvov most",
                        Patient = patient
                    };

                    patient.Diagnoses.Add(diagnose);

                    Medicament medicament = new Medicament
                    {
                        Name = "Paracetamol",
                        Patient = patient
                    };

                    patient.Medicaments.Add(medicament);

                    context.Patients.Add(patient);
                    context.Doctors.Add(doctor);
                    context.Diagnoses.Add(diagnose);
                    context.Visitations.Add(visitation);
                    context.Medicaments.Add(medicament);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult dbEntityValidationResult in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        Console.WriteLine(dbValidationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
