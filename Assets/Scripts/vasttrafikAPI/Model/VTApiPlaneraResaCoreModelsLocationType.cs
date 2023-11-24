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
    /// Defines VT.ApiPlaneraResa.Core.Models.LocationType
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaCoreModelsLocationType
    {
        /// <summary>
        /// Enum Unknown for value: unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Enum Stoparea for value: stoparea
        /// </summary>
        [EnumMember(Value = "stoparea")]
        Stoparea,

        /// <summary>
        /// Enum Stoppoint for value: stoppoint
        /// </summary>
        [EnumMember(Value = "stoppoint")]
        Stoppoint,

        /// <summary>
        /// Enum Address for value: address
        /// </summary>
        [EnumMember(Value = "address")]
        Address,

        /// <summary>
        /// Enum Pointofinterest for value: pointofinterest
        /// </summary>
        [EnumMember(Value = "pointofinterest")]
        Pointofinterest,

        /// <summary>
        /// Enum Metastation for value: metastation
        /// </summary>
        [EnumMember(Value = "metastation")]
        Metastation
    }

}