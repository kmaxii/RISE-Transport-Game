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
    /// Information about a ticket suggestion.
    /// </summary>
    [DataContract(Name = "VT.ApiPlaneraResa.Web.V4.Models.JourneyDetails.TicketSuggestionApiModel")]
    public partial class VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel : IEquatable<VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel>, IValidatableObject
    {

        /// <summary>
        /// Gets or Sets TravellerCategory
        /// </summary>
        [DataMember(Name = "travellerCategory", EmitDefaultValue = false)]
        public VTApiPlaneraResaCoreModelsTravellerCategory? TravellerCategory { get; set; }

        /// <summary>
        /// Gets or Sets TimeLimitation
        /// </summary>
        [DataMember(Name = "timeLimitation", EmitDefaultValue = false)]
        public VTApiPlaneraResaCoreModelsTimeLimitation? TimeLimitation { get; set; }

        /// <summary>
        /// Gets or Sets ProductInstanceType
        /// </summary>
        [DataMember(Name = "productInstanceType", EmitDefaultValue = false)]
        public VTApiPlaneraResaWebV4ModelsProductInstanceTypeApiModel? ProductInstanceType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel" /> class.
        /// </summary>
        /// <param name="productId">The product id..</param>
        /// <param name="productName">The product name..</param>
        /// <param name="productType">The product type..</param>
        /// <param name="travellerCategory">travellerCategory.</param>
        /// <param name="priceInSek">The product price in SEK..</param>
        /// <param name="timeValidity">timeValidity.</param>
        /// <param name="timeLimitation">timeLimitation.</param>
        /// <param name="saleChannels">A list of the channels that sell the product..</param>
        /// <param name="validZones">A list of the valid zones for the ticket..</param>
        /// <param name="productInstanceType">productInstanceType.</param>
        /// <param name="punchConfiguration">punchConfiguration.</param>
        /// <param name="offerSpecification">Used to get ticket offer..</param>
        public VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel(int productId = default(int), string productName = default(string), int productType = default(int), VTApiPlaneraResaCoreModelsTravellerCategory? travellerCategory = default(VTApiPlaneraResaCoreModelsTravellerCategory?), double priceInSek = default(double), VTApiPlaneraResaWebV4ModelsJourneyDetailsTimeValidityApiModel timeValidity = default(VTApiPlaneraResaWebV4ModelsJourneyDetailsTimeValidityApiModel), VTApiPlaneraResaCoreModelsTimeLimitation? timeLimitation = default(VTApiPlaneraResaCoreModelsTimeLimitation?), List<VTApiPlaneraResaWebV4ModelsJourneyDetailsChannelApiModel> saleChannels = default(List<VTApiPlaneraResaWebV4ModelsJourneyDetailsChannelApiModel>), List<VTApiPlaneraResaWebV4ModelsJourneyDetailsZoneApiModel> validZones = default(List<VTApiPlaneraResaWebV4ModelsJourneyDetailsZoneApiModel>), VTApiPlaneraResaWebV4ModelsProductInstanceTypeApiModel? productInstanceType = default(VTApiPlaneraResaWebV4ModelsProductInstanceTypeApiModel?), VTApiPlaneraResaWebV4ModelsJourneyDetailsPunchConfigurationApiModel punchConfiguration = default(VTApiPlaneraResaWebV4ModelsJourneyDetailsPunchConfigurationApiModel), string offerSpecification = default(string))
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.ProductType = productType;
            this.TravellerCategory = travellerCategory;
            this.PriceInSek = priceInSek;
            this.TimeValidity = timeValidity;
            this.TimeLimitation = timeLimitation;
            this.SaleChannels = saleChannels;
            this.ValidZones = validZones;
            this.ProductInstanceType = productInstanceType;
            this.PunchConfiguration = punchConfiguration;
            this.OfferSpecification = offerSpecification;
        }

        /// <summary>
        /// The product id.
        /// </summary>
        /// <value>The product id.</value>
        [DataMember(Name = "productId", EmitDefaultValue = false)]
        public int ProductId { get; set; }

        /// <summary>
        /// The product name.
        /// </summary>
        /// <value>The product name.</value>
        [DataMember(Name = "productName", EmitDefaultValue = true)]
        public string ProductName { get; set; }

        /// <summary>
        /// The product type.
        /// </summary>
        /// <value>The product type.</value>
        [DataMember(Name = "productType", EmitDefaultValue = false)]
        public int ProductType { get; set; }

        /// <summary>
        /// The product price in SEK.
        /// </summary>
        /// <value>The product price in SEK.</value>
        [DataMember(Name = "priceInSek", EmitDefaultValue = false)]
        public double PriceInSek { get; set; }

        /// <summary>
        /// Gets or Sets TimeValidity
        /// </summary>
        [DataMember(Name = "timeValidity", EmitDefaultValue = false)]
        public VTApiPlaneraResaWebV4ModelsJourneyDetailsTimeValidityApiModel TimeValidity { get; set; }

        /// <summary>
        /// A list of the channels that sell the product.
        /// </summary>
        /// <value>A list of the channels that sell the product.</value>
        [DataMember(Name = "saleChannels", EmitDefaultValue = true)]
        public List<VTApiPlaneraResaWebV4ModelsJourneyDetailsChannelApiModel> SaleChannels { get; set; }

        /// <summary>
        /// A list of the valid zones for the ticket.
        /// </summary>
        /// <value>A list of the valid zones for the ticket.</value>
        [DataMember(Name = "validZones", EmitDefaultValue = true)]
        public List<VTApiPlaneraResaWebV4ModelsJourneyDetailsZoneApiModel> ValidZones { get; set; }

        /// <summary>
        /// Gets or Sets PunchConfiguration
        /// </summary>
        [DataMember(Name = "punchConfiguration", EmitDefaultValue = false)]
        public VTApiPlaneraResaWebV4ModelsJourneyDetailsPunchConfigurationApiModel PunchConfiguration { get; set; }

        /// <summary>
        /// Used to get ticket offer.
        /// </summary>
        /// <value>Used to get ticket offer.</value>
        [DataMember(Name = "offerSpecification", EmitDefaultValue = true)]
        public string OfferSpecification { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel {\n");
            sb.Append("  ProductId: ").Append(ProductId).Append("\n");
            sb.Append("  ProductName: ").Append(ProductName).Append("\n");
            sb.Append("  ProductType: ").Append(ProductType).Append("\n");
            sb.Append("  TravellerCategory: ").Append(TravellerCategory).Append("\n");
            sb.Append("  PriceInSek: ").Append(PriceInSek).Append("\n");
            sb.Append("  TimeValidity: ").Append(TimeValidity).Append("\n");
            sb.Append("  TimeLimitation: ").Append(TimeLimitation).Append("\n");
            sb.Append("  SaleChannels: ").Append(SaleChannels).Append("\n");
            sb.Append("  ValidZones: ").Append(ValidZones).Append("\n");
            sb.Append("  ProductInstanceType: ").Append(ProductInstanceType).Append("\n");
            sb.Append("  PunchConfiguration: ").Append(PunchConfiguration).Append("\n");
            sb.Append("  OfferSpecification: ").Append(OfferSpecification).Append("\n");
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
            return this.Equals(input as VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel);
        }

        /// <summary>
        /// Returns true if VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel instances are equal
        /// </summary>
        /// <param name="input">Instance of VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VTApiPlaneraResaWebV4ModelsJourneyDetailsTicketSuggestionApiModel input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.ProductId == input.ProductId ||
                    this.ProductId.Equals(input.ProductId)
                ) && 
                (
                    this.ProductName == input.ProductName ||
                    (this.ProductName != null &&
                    this.ProductName.Equals(input.ProductName))
                ) && 
                (
                    this.ProductType == input.ProductType ||
                    this.ProductType.Equals(input.ProductType)
                ) && 
                (
                    this.TravellerCategory == input.TravellerCategory ||
                    this.TravellerCategory.Equals(input.TravellerCategory)
                ) && 
                (
                    this.PriceInSek == input.PriceInSek ||
                    this.PriceInSek.Equals(input.PriceInSek)
                ) && 
                (
                    this.TimeValidity == input.TimeValidity ||
                    (this.TimeValidity != null &&
                    this.TimeValidity.Equals(input.TimeValidity))
                ) && 
                (
                    this.TimeLimitation == input.TimeLimitation ||
                    this.TimeLimitation.Equals(input.TimeLimitation)
                ) && 
                (
                    this.SaleChannels == input.SaleChannels ||
                    this.SaleChannels != null &&
                    input.SaleChannels != null &&
                    this.SaleChannels.SequenceEqual(input.SaleChannels)
                ) && 
                (
                    this.ValidZones == input.ValidZones ||
                    this.ValidZones != null &&
                    input.ValidZones != null &&
                    this.ValidZones.SequenceEqual(input.ValidZones)
                ) && 
                (
                    this.ProductInstanceType == input.ProductInstanceType ||
                    this.ProductInstanceType.Equals(input.ProductInstanceType)
                ) && 
                (
                    this.PunchConfiguration == input.PunchConfiguration ||
                    (this.PunchConfiguration != null &&
                    this.PunchConfiguration.Equals(input.PunchConfiguration))
                ) && 
                (
                    this.OfferSpecification == input.OfferSpecification ||
                    (this.OfferSpecification != null &&
                    this.OfferSpecification.Equals(input.OfferSpecification))
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
                hashCode = (hashCode * 59) + this.ProductId.GetHashCode();
                if (this.ProductName != null)
                {
                    hashCode = (hashCode * 59) + this.ProductName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ProductType.GetHashCode();
                hashCode = (hashCode * 59) + this.TravellerCategory.GetHashCode();
                hashCode = (hashCode * 59) + this.PriceInSek.GetHashCode();
                if (this.TimeValidity != null)
                {
                    hashCode = (hashCode * 59) + this.TimeValidity.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TimeLimitation.GetHashCode();
                if (this.SaleChannels != null)
                {
                    hashCode = (hashCode * 59) + this.SaleChannels.GetHashCode();
                }
                if (this.ValidZones != null)
                {
                    hashCode = (hashCode * 59) + this.ValidZones.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ProductInstanceType.GetHashCode();
                if (this.PunchConfiguration != null)
                {
                    hashCode = (hashCode * 59) + this.PunchConfiguration.GetHashCode();
                }
                if (this.OfferSpecification != null)
                {
                    hashCode = (hashCode * 59) + this.OfferSpecification.GetHashCode();
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
