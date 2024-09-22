var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ResourcePlanner_Api>("resourceplanner-api");

await builder.Build().RunAsync();
