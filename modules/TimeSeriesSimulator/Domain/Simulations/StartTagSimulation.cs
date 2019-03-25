/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Concepts;

namespace Domain.Simulations
{
    /// <summary>
    /// Represents the command that starts a simulation
    /// </summary>
    public class StartTagSimulation : ICommand
    {
        /// <summary>
        /// The <see cref="global::Concepts.System"/> the data point is coming from
        /// </summary>
        public global::Concepts.System System { get; set; }

        /// <summary>
        /// The <see cref="Tag"/> the data point is coming from
        /// </summary>
        public Tag Tag { get; set; }
    }
}