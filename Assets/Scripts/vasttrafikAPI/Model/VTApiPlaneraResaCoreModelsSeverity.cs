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
    /// Defines VT.ApiPlaneraResa.Core.Models.Severity
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaCoreModelsSeverity
    {
        /// <summary>
        /// Enum Unknown for value: unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Enum Low for value: low
        /// </summary>
        [EnumMember(Value = "low")]
        Low,

        /// <summary>
        /// Enum Normal for value: normal
        /// </summary>
        [EnumMember(Value = "normal")]
        Normal,

        /// <summary>
        /// Enum High for value: high
        /// </summary>
        [EnumMember(Value = "high")]
        High
    }

}