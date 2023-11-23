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
    /// The different kinds of detailed information that could be included in a get arrival details request.
    /// </summary>
    /// <value>The different kinds of detailed information that could be included in a get arrival details request.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaWebV4ModelsArrivalDetailsIncludeType
    {
        /// <summary>
        /// Enum Servicejourneycalls for value: servicejourneycalls
        /// </summary>
        [EnumMember(Value = "servicejourneycalls")]
        Servicejourneycalls = 1,

        /// <summary>
        /// Enum Servicejourneycoordinates for value: servicejourneycoordinates
        /// </summary>
        [EnumMember(Value = "servicejourneycoordinates")]
        Servicejourneycoordinates = 2
    }

}
