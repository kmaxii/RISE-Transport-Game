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
    /// Defines VT.ApiPlaneraResa.Core.Models.PositionTransportMode
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaCoreModelsPositionTransportMode
    {
        /// <summary>
        /// Enum Tram for value: tram
        /// </summary>
        [EnumMember(Value = "tram")]
        Tram = 1,

        /// <summary>
        /// Enum Bus for value: bus
        /// </summary>
        [EnumMember(Value = "bus")]
        Bus = 2,

        /// <summary>
        /// Enum Ferry for value: ferry
        /// </summary>
        [EnumMember(Value = "ferry")]
        Ferry = 3,

        /// <summary>
        /// Enum Train for value: train
        /// </summary>
        [EnumMember(Value = "train")]
        Train = 4,

        /// <summary>
        /// Enum Taxi for value: taxi
        /// </summary>
        [EnumMember(Value = "taxi")]
        Taxi = 5
    }

}