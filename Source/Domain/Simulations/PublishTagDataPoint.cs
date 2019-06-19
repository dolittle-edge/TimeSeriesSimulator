/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;
using Dolittle.TimeSeries.Modules;

namespace Domain.Simulations
{
    /// <summary>
    /// Represents the command for publishing a single data point
    /// </summary>
    public class PublishTagDataPoint : ICommand
    {
        /// <summary>
        /// The <see cref="Source"/> the data point is coming from
        /// </summary>
        public Source Source { get; set; }

        /// <summary>
        /// The <see cref="Tag"/> the data point is coming from
        /// </summary>
        public Tag Tag { get; set; }

        /// <summary>
        /// Gets or sets the value for the datapoint
        /// </summary>
        public double Value { get; set; }
    }
}
