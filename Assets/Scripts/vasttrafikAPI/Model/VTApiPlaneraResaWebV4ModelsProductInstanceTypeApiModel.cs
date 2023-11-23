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
    /// Specifies whether or not the product is dynamically based on the journey route.
    /// </summary>
    /// <value>Specifies whether or not the product is dynamically based on the journey route.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaWebV4ModelsProductInstanceTypeApiModel
    {
        /// <summary>
        /// Enum Static for value: static
        /// </summary>
        [EnumMember(Value = "static")]
        Static = 1,

        /// <summary>
        /// Enum Dynamic for value: dynamic
        /// </summary>
        [EnumMember(Value = "dynamic")]
        Dynamic = 2
    }

}
