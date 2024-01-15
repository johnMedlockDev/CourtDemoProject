#!/bin/bash
set -e

# Run database migrations
dotnet ef database update -- --connection-string