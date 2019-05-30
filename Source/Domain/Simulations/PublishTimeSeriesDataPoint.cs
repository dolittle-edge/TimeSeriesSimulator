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
    public class PublishTimeSeriesDataPoint : ICommand
    {
        /// <summary>
        /// The <see cref="TimeSeries"/> the data point is coming from
        /// </summary>
        public TimeSeries TimeSeries { get; set; }

        /// <summary>
        /// Gets or sets the value for the datapoint
        /// </summary>
        public double Value { get; set; }
    }
}