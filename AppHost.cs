var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CineMoodAI_WebAPI>("cinemoodai-webapi");

builder.Build().Run();
