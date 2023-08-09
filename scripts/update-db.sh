#!/usr/bin/env bash
dotnet ef database update --project src/Infrastructure --startup-project src/WebApi --context ApplicationDbContext