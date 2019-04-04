/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using Dolittle.Commands.Handling;
using Dolittle.Edge.Modules;

namespace Domain.Simulations
{

    /// <summary>
    /// Represents the <see cref="ICanHandleCommands">command handlers</see> for publishing of data points
    /// </summary>
    public class PublishingCommandHandlers : ICanHandleCommands
    {
        readonly ICommunicationClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="PublishingCommandHandlers"/>
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> to use for messaging</param>
        public PublishingCommandHandlers(ICommunicationClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Handles publishing of a single data point
        /// </summary>
        /// <param name="command"><see cref="PublishTagDataPoint"/> command to handle</param>
        public void Handle(PublishTagDataPoint command)
        {
            var dataPoint = new TagDataPoint<double>
            {
                System = command.System,
                Tag = command.Tag,
                Value = command.Value,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            _client.SendAsJson("tags", dataPoint);
        }

        /// <summary>
        /// Handles publishing of a single data point
        /// </summary>
        /// <param name="command"><see cref="PublishTimeSeriesDataPoint"/> command to handle</param>
        public void Handle(PublishTimeSeriesDataPoint command)
        {
            var dataPoint = new DataPoint<double>
            {
                TimeSeries = command.TimeSeries,
                Value = command.Value,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            _client.SendAsJson("timeseries", dataPoint);
        }
    }
}
