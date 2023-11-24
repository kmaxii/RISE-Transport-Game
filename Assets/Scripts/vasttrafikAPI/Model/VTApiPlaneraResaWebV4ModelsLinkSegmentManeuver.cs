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
    /// Specifies a maneuver to be executed for a link segment.
    /// </summary>
    /// <value>Specifies a maneuver to be executed for a link segment.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VTApiPlaneraResaWebV4ModelsLinkSegmentManeuver
    {
        /// <summary>
        /// Enum None for value: none
        /// </summary>
        [EnumMember(Value = "none")]
        None = 1,

        /// <summary>
        /// Enum From for value: from
        /// </summary>
        [EnumMember(Value = "from")]
        From = 2,

        /// <summary>
        /// Enum To for value: to
        /// </summary>
        [EnumMember(Value = "to")]
        To = 3,

        /// <summary>
        /// Enum On for value: on
        /// </summary>
        [EnumMember(Value = "on")]
        On = 4,

        /// <summary>
        /// Enum Left for value: left
        /// </summary>
        [EnumMember(Value = "left")]
        Left = 5,

        /// <summary>
        /// Enum Right for value: right
        /// </summary>
        [EnumMember(Value = "right")]
        Right = 6,

        /// <summary>
        /// Enum Keepleft for value: keepleft
        /// </summary>
        [EnumMember(Value = "keepleft")]
        Keepleft = 7,

        /// <summary>
        /// Enum Keepright for value: keepright
        /// </summary>
        [EnumMember(Value = "keepright")]
        Keepright = 8,

        /// <summary>
        /// Enum Halfleft for value: halfleft
        /// </summary>
        [EnumMember(Value = "halfleft")]
        Halfleft = 9,

        /// <summary>
        /// Enum Halfright for value: halfright
        /// </summary>
        [EnumMember(Value = "halfright")]
        Halfright = 10,

        /// <summary>
        /// Enum Keephalfleft for value: keephalfleft
        /// </summary>
        [EnumMember(Value = "keephalfleft")]
        Keephalfleft = 11,

        /// <summary>
        /// Enum Keephalfright for value: keephalfright
        /// </summary>
        [EnumMember(Value = "keephalfright")]
        Keephalfright = 12,

        /// <summary>
        /// Enum Sharpleft for value: sharpleft
        /// </summary>
        [EnumMember(Value = "sharpleft")]
        Sharpleft = 13,

        /// <summary>
        /// Enum Sharpright for value: sharpright
        /// </summary>
        [EnumMember(Value = "sharpright")]
        Sharpright = 14,

        /// <summary>
        /// Enum Keepsharpleft for value: keepsharpleft
        /// </summary>
        [EnumMember(Value = "keepsharpleft")]
        Keepsharpleft = 15,

        /// <summary>
        /// Enum Keepsharpright for value: keepsharpright
        /// </summary>
        [EnumMember(Value = "keepsharpright")]
        Keepsharpright = 16,

        /// <summary>
        /// Enum Straight for value: straight
        /// </summary>
        [EnumMember(Value = "straight")]
        Straight = 17,

        /// <summary>
        /// Enum Uturn for value: uturn
        /// </summary>
        [EnumMember(Value = "uturn")]
        Uturn = 18,

        /// <summary>
        /// Enum Enter for value: enter
        /// </summary>
        [EnumMember(Value = "enter")]
        Enter = 19,

        /// <summary>
        /// Enum Leave for value: leave
        /// </summary>
        [EnumMember(Value = "leave")]
        Leave = 20,

        /// <summary>
        /// Enum Enterroundabout for value: enterroundabout
        /// </summary>
        [EnumMember(Value = "enterroundabout")]
        Enterroundabout = 21,

        /// <summary>
        /// Enum Stayinroundabout for value: stayinroundabout
        /// </summary>
        [EnumMember(Value = "stayinroundabout")]
        Stayinroundabout = 22,

        /// <summary>
        /// Enum Leaveroundabout for value: leaveroundabout
        /// </summary>
        [EnumMember(Value = "leaveroundabout")]
        Leaveroundabout = 23,

        /// <summary>
        /// Enum Enterferry for value: enterferry
        /// </summary>
        [EnumMember(Value = "enterferry")]
        Enterferry = 24,

        /// <summary>
        /// Enum Leaveferry for value: leaveferry
        /// </summary>
        [EnumMember(Value = "leaveferry")]
        Leaveferry = 25,

        /// <summary>
        /// Enum Changehighway for value: changehighway
        /// </summary>
        [EnumMember(Value = "changehighway")]
        Changehighway = 26,

        /// <summary>
        /// Enum Checkinferry for value: checkinferry
        /// </summary>
        [EnumMember(Value = "checkinferry")]
        Checkinferry = 27,

        /// <summary>
        /// Enum Checkoutferry for value: checkoutferry
        /// </summary>
        [EnumMember(Value = "checkoutferry")]
        Checkoutferry = 28,

        /// <summary>
        /// Enum Elevator for value: elevator
        /// </summary>
        [EnumMember(Value = "elevator")]
        Elevator = 29,

        /// <summary>
        /// Enum Elevatordown for value: elevatordown
        /// </summary>
        [EnumMember(Value = "elevatordown")]
        Elevatordown = 30,

        /// <summary>
        /// Enum Elevatorup for value: elevatorup
        /// </summary>
        [EnumMember(Value = "elevatorup")]
        Elevatorup = 31,

        /// <summary>
        /// Enum Escalator for value: escalator
        /// </summary>
        [EnumMember(Value = "escalator")]
        Escalator = 32,

        /// <summary>
        /// Enum Escalatordown for value: escalatordown
        /// </summary>
        [EnumMember(Value = "escalatordown")]
        Escalatordown = 33,

        /// <summary>
        /// Enum Escalatorup for value: escalatorup
        /// </summary>
        [EnumMember(Value = "escalatorup")]
        Escalatorup = 34,

        /// <summary>
        /// Enum Stairs for value: stairs
        /// </summary>
        [EnumMember(Value = "stairs")]
        Stairs = 35,

        /// <summary>
        /// Enum Stairsdown for value: stairsdown
        /// </summary>
        [EnumMember(Value = "stairsdown")]
        Stairsdown = 36,

        /// <summary>
        /// Enum Stairsup for value: stairsup
        /// </summary>
        [EnumMember(Value = "stairsup")]
        Stairsup = 37
    }

}