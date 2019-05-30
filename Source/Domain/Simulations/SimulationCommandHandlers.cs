/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading;
using Dolittle.Commands.Handling;
using Dolittle.TimeSeries.Modules;
using Dolittle.Logging;

namespace Domain.Simulations
{

    /// <summary>
    /// Represents the <see cref="ICanHandleCommands">command handlers</see> for working with simulations
    /// </summary>
    public class SimulationCommandHandlers : ICanHandleCommands
    {
        readonly ICommunicationClient _client;
        readonly Random _random;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance 
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> for messaging</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public SimulationCommandHandlers(ICommunicationClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
            _random = new Random();
            
        }

        /// <summary>
        /// Handles the <see cref="StartTagSimulation"/> command
        /// </summary>
        /// <param name="command">The <see cref="StartTagSimulation">command</see></param>
        public void Handle(StartTagSimulation command)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Repeat.Interval(TimeSpan.FromSeconds(1), () => {

                var dataPoint = new TagDataPoint<double>
                {
                    ControlSystem = command.ControlSystem,
                    Tag = command.Tag,
                    Value = _random.NextDouble()*100,
                    Timestamp = Timestamp.UtcNow
                };

                _logger.Information($"Sending event for system '{command.ControlSystem}' - tag '{command.Tag}' with value '{dataPoint.Value}'");

                _client.SendAsJson("tags", dataPoint);
            }, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Handles the <see cref="StartTimeSeriesSimulation"/> command
        /// </summary>
        /// <param name="command">The <see cref="StartTimeSeriesSimulation">command</see></param>
        public void Handle(StartTimeSeriesSimulation command)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Repeat.Interval(TimeSpan.FromSeconds(1), () => {

                var dataPoint = new DataPoint<double>
                {
                    TimeSeries = command.TimeSeries,
                    Value = _random.NextDouble()*100,
                    Timestamp = Timestamp.UtcNow
                };

                _logger.Information($"Sending event for TimeSeries '{command.TimeSeries}' with value '{dataPoint.Value}'");

                _client.SendAsJson("timeseries", dataPoint);
            }, cancellationTokenSource.Token);
        }

    }
}