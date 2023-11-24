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
    /// Represents a level of occupancy.
    /// </summary>
    /// <value>Represents a level of occupancy.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaWebV4ModelsOccupancyLevel
    {
        /// <summary>
        /// Enum Low for value: low
        /// </summary>
        [EnumMember(Value = "low")]
        Low = 1,

        /// <summary>
        /// Enum Medium for value: medium
        /// </summary>
        [EnumMember(Value = "medium")]
        Medium = 2,

        /// <summary>
        /// Enum High for value: high
        /// </summary>
        [EnumMember(Value = "high")]
        High = 3,

        /// <summary>
        /// Enum Incomplete for value: incomplete
        /// </summary>
        [EnumMember(Value = "incomplete")]
        Incomplete = 4,

        /// <summary>
        /// Enum Missing for value: missing
        /// </summary>
        [EnumMember(Value = "missing")]
        Missing = 5,

        /// <summary>
        /// Enum Notpublictransport for value: notpublictransport
        /// </summary>
        [EnumMember(Value = "notpublictransport")]
        Notpublictransport = 6
    }

}