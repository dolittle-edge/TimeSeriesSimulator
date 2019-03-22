/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading;
using Dolittle.Commands.Handling;
using Dolittle.Edge.Modules;
using Dolittle.Logging;

namespace Domain.Simulations
{

    /// <summary>
    /// Represents the <see cref="ICanHandleCommands">command handlers</see> for working with simulations
    /// </summary>
    public class SimulationCommandHandlers : ICanHandleCommands
    {
        readonly IClient _client;
        readonly Random _random;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        /// <param name="client"><see cref="IClient"/> for messaging</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public SimulationCommandHandlers(IClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
            _random = new Random();
            
        }

        /// <summary>
        /// Handles the <see cref="StartSimulation"/> command
        /// </summary>
        /// <param name="command">The <see cref="StartSimulation">command</see></param>
        public void Handle(StartSimulation command)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Repeat.Interval(TimeSpan.FromSeconds(1), () => {

                var dataPoint = new DataPoint
                {
                    System = command.System,
                    Tag = command.Tag,
                    Value = _random.NextDouble()*100,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                _logger.Information($"Sending event for system '{command.System}' - tag '{command.Tag}' with value '{dataPoint.Value}'");

                _client.SendEventAsJson("events", dataPoint);
            }, cancellationTokenSource.Token);
        }
    }
}