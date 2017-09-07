using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

namespace Cgsc.Doet.AtisWebServiceLibrary.MessageClasses
{
    /// <summary>
    /// Used to represent a json message used for sending updates to an enrollment.
    /// </summary>
    [DataContract]
    public class EnrollmentUpdates : AtisResponseMessage
    {
        [DataMember(Name = "enrollments")]
        public List<EnrollmentUpdate> EnrollmentUpdateList { get; set; }
    }

    [DataContract]
    public class EnrollmentUpdate : IAtisMessage
    {
        public int StatusUpdateID { get; set; }
        [DataMember(Name = "cls", IsRequired=false, EmitDefaultValue=false)]
        public string ClassId { get; set; }
        [DataMember(Name = "crs", IsRequired=false, EmitDefaultValue=false)]
        public string CourseId { get; set; }
        [DataMember(Name = "fy", IsRequired=false, EmitDefaultValue=false)]
        public string SchoolYear { get; set; }
        [DataMember(Name = "phase", IsRequired=false, EmitDefaultValue=false)]
        public string Phase { get; set; }
        [DataMember(Name = "sch", IsRequired=false, EmitDefaultValue=false)]
        public string School { get; set; }
        [DataMember(Name = "stuId", IsRequired=false, EmitDefaultValue=false)]
        public string EDIPI { get; set; }
        [DataMember(Name = "statusDate", IsRequired=false, EmitDefaultValue=false)]
        public string StatusDate { get; set; }
        [DataMember(Name = "statusCode", IsRequired=false, EmitDefaultValue=false)]
        public string StatusCode { get; set; }
        [DataMember(Name = "statusType", IsRequired=false, EmitDefaultValue=false)]
        public string StatusType { get; set; }
        [DataMember(Name = "reasonCode", IsRequired=false, EmitDefaultValue=false)]
        public string ReasonCode { get; set; }
        [DataMember(Name = "qs", IsRequired=false, EmitDefaultValue=false)]
        public string QuotaStatus {get; set; }
        [DataMember(Name = "inpDate", IsRequired=false, EmitDefaultValue=false)]
        public string InputDate {get; set;}
        [DataMember(Name = "trackingId", IsRequired = false, EmitDefaultValue = false)]
        public string TrackingID { get; set; }
    }

    [DataContract]
    public class Enrollment : IAtisMessage
    {
        [DataMember(Name = "id", IsRequired=false, EmitDefaultValue=false)]
        public int Id { get; set; }
        [DataMember(Name = "edipi", IsRequired=false, EmitDefaultValue=false)]
        public string EDIPI { get; set; }
        [DataMember(Name = "title", IsRequired=false, EmitDefaultValue=false)]
        public string Title { get; set; }
        [DataMember(Name = "fy", IsRequired=false, EmitDefaultValue=false)]
        public string SchoolYear { get; set; }
        [DataMember(Name = "sch", IsRequired=false, EmitDefaultValue=false)]
        public string School { get; set; }
        [DataMember(Name = "crs", IsRequired=false, EmitDefaultValue=false)]
        public string CourseId { get; set; }
        [DataMember(Name = "phase", IsRequired=false, EmitDefaultValue=false)]
        public string Phase { get; set; }
        [DataMember(Name = "cls", IsRequired=false, EmitDefaultValue=false)]
        public string ClassId { get; set; }
        [DataMember(Name = "resStat", IsRequired=false, EmitDefaultValue=false)]
        public string ReservationStatus { get; set; }
        [DataMember(Name = "resDate", IsRequired=false, EmitDefaultValue=false)]
        public string ReservationDate { get; set; }
        [DataMember(Name = "reasonRs", IsRequired=false, EmitDefaultValue=false)]
        public string reasonRs { get; set; }
        [DataMember(Name = "lastModified", IsRequired=false, EmitDefaultValue=false)]
        public string LastModified { get; set; }
        [DataMember(Name = "isActive", IsRequired=false, EmitDefaultValue=false)]
        public bool IsActive { get; set; }
        [DataMember(Name = "links", IsRequired=false, EmitDefaultValue=false)]
        public Links Links { get; set; }
    }
}
