var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ResourcePlanner_Api>("resourceplanner-api");

builder.Build().Run();
