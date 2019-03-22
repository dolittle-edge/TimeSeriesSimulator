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
        readonly IClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="PublishingCommandHandlers"/>
        /// </summary>
        /// <param name="client"><see cref="IClient"/> to use for messaging</param>
        public PublishingCommandHandlers(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Handles publishing of a single data point
        /// </summary>
        /// <param name="command"><see cref="PublishDataPoint"/> command to handle</param>
        public void Handle(PublishDataPoint command)
        {
            var dataPoint = new DataPoint
            {
                System = command.System,
                Tag = command.Tag,
                Value = command.Value,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            _client.SendEventAsJson("events", dataPoint);
        }
    }
}
