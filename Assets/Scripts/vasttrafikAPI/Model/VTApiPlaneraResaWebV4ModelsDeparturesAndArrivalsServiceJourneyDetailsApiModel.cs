/*
 * Planera Resa
 *
 * Sök och planera resor med Västtrafik
 *
 * The version of the OpenAPI document: v4
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// Information about a service journey.
    /// </summary>
    [DataContract(Name = "VT.ApiPlaneraResa.Web.V4.Models.DeparturesAndArrivals.ServiceJourneyDetailsApiModel")]
    public partial class VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel : IEquatable<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel" /> class.
        /// </summary>
        /// <param name="gid">16-digit Västtrafik service journey gid..</param>
        /// <param name="direction">A description of the direction..</param>
        /// <param name="line">line.</param>
        /// <param name="serviceJourneyCoordinates">The coordinates of the service journey..</param>
        /// <param name="callsOnServiceJourney">All calls on the service journey..</param>
        public VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel(string gid = default(string), string direction = default(string), VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsLineDetailsApiModel line = default(VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsLineDetailsApiModel), List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCoordinateApiModel> serviceJourneyCoordinates = default(List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCoordinateApiModel>), List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCallDetailsApiModel> callsOnServiceJourney = default(List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCallDetailsApiModel>))
        {
            this.Gid = gid;
            this.Direction = direction;
            this.Line = line;
            this.ServiceJourneyCoordinates = serviceJourneyCoordinates;
            this.CallsOnServiceJourney = callsOnServiceJourney;
        }

        /// <summary>
        /// 16-digit Västtrafik service journey gid.
        /// </summary>
        /// <value>16-digit Västtrafik service journey gid.</value>
        [DataMember(Name = "gid", EmitDefaultValue = true)]
        public string Gid { get; set; }

        /// <summary>
        /// A description of the direction.
        /// </summary>
        /// <value>A description of the direction.</value>
        [DataMember(Name = "direction", EmitDefaultValue = true)]
        public string Direction { get; set; }

        /// <summary>
        /// Gets or Sets Line
        /// </summary>
        [DataMember(Name = "line", EmitDefaultValue = false)]
        public VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsLineDetailsApiModel Line { get; set; }

        /// <summary>
        /// The coordinates of the service journey.
        /// </summary>
        /// <value>The coordinates of the service journey.</value>
        [DataMember(Name = "serviceJourneyCoordinates", EmitDefaultValue = true)]
        public List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCoordinateApiModel> ServiceJourneyCoordinates { get; set; }

        /// <summary>
        /// All calls on the service journey.
        /// </summary>
        /// <value>All calls on the service journey.</value>
        [DataMember(Name = "callsOnServiceJourney", EmitDefaultValue = true)]
        public List<VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsCallDetailsApiModel> CallsOnServiceJourney { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel {\n");
            sb.Append("  Gid: ").Append(Gid).Append("\n");
            sb.Append("  Direction: ").Append(Direction).Append("\n");
            sb.Append("  Line: ").Append(Line).Append("\n");
            sb.Append("  ServiceJourneyCoordinates: ").Append(ServiceJourneyCoordinates).Append("\n");
            sb.Append("  CallsOnServiceJourney: ").Append(CallsOnServiceJourney).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel);
        }

        /// <summary>
        /// Returns true if VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel instances are equal
        /// </summary>
        /// <param name="input">Instance of VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VTApiPlaneraResaWebV4ModelsDeparturesAndArrivalsServiceJourneyDetailsApiModel input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Gid == input.Gid ||
                    (this.Gid != null &&
                    this.Gid.Equals(input.Gid))
                ) && 
                (
                    this.Direction == input.Direction ||
                    (this.Direction != null &&
                    this.Direction.Equals(input.Direction))
                ) && 
                (
                    this.Line == input.Line ||
                    (this.Line != null &&
                    this.Line.Equals(input.Line))
                ) && 
                (
                    this.ServiceJourneyCoordinates == input.ServiceJourneyCoordinates ||
                    this.ServiceJourneyCoordinates != null &&
                    input.ServiceJourneyCoordinates != null &&
                    this.ServiceJourneyCoordinates.SequenceEqual(input.ServiceJourneyCoordinates)
                ) && 
                (
                    this.CallsOnServiceJourney == input.CallsOnServiceJourney ||
                    this.CallsOnServiceJourney != null &&
                    input.CallsOnServiceJourney != null &&
                    this.CallsOnServiceJourney.SequenceEqual(input.CallsOnServiceJourney)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Gid != null)
                {
                    hashCode = (hashCode * 59) + this.Gid.GetHashCode();
                }
                if (this.Direction != null)
                {
                    hashCode = (hashCode * 59) + this.Direction.GetHashCode();
                }
                if (this.Line != null)
                {
                    hashCode = (hashCode * 59) + this.Line.GetHashCode();
                }
                if (this.ServiceJourneyCoordinates != null)
                {
                    hashCode = (hashCode * 59) + this.ServiceJourneyCoordinates.GetHashCode();
                }
                if (this.CallsOnServiceJourney != null)
                {
                    hashCode = (hashCode * 59) + this.CallsOnServiceJourney.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}