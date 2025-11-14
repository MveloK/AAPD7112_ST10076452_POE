

using System;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    public class ServiceRequestData
    {
        
        public int RequestID { get; set; }        
        public string Description { get; set; }
        public string Status { get; set; }       
        public DateTime SubmittedDate { get; set; }
        public int Severity { get; set; }         

        public ServiceRequestData(int id, string desc, string status, int severity = 1)
        {
            RequestID = id;
            Description = desc;
            Status = status;
            SubmittedDate = DateTime.Now; 
            Severity = severity;
        }

        public override string ToString()
        {
            string displayDesc = Description.Length > 40
                                 ? Description.Substring(0, 40) + "..."
                                 : Description;

            return $"ID: {RequestID} | Status: {Status} | Severity: {Severity} | Description: {displayDesc}";
        }
    }
}