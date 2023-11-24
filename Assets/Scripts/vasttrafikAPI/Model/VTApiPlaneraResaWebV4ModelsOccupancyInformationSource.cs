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
    /// Represents the source from which the occupancy information originate.
    /// </summary>
    /// <value>Represents the source from which the occupancy information originate.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaWebV4ModelsOccupancyInformationSource
    {
        /// <summary>
        /// Enum Prediction for value: prediction
        /// </summary>
        [EnumMember(Value = "prediction")]
        Prediction = 1,

        /// <summary>
        /// Enum Realtime for value: realtime
        /// </summary>
        [EnumMember(Value = "realtime")]
        Realtime = 2
    }

}