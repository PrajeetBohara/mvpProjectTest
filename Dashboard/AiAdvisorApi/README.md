# AI Advisor Simple API

A minimal ASP.NET Core API that stores transcripts in memory. No external services needed.

## Run Locally

```bash
cd Dashboard/AiAdvisorApi
dotnet run
```

The API will run on `http://localhost:5000` (or the port shown in console).

## Endpoints

- `POST /api/chat` - Append Q&A to a session
  ```json
  { "sessionId": "demo", "question": "...", "answer": "..." }
  ```

- `GET /api/transcript?sessionId=demo` - Get all messages for a session

- `POST /api/transcript/clear` - Clear a session
  ```json
  { "sessionId": "demo" }
  ```

## Deploy

You can deploy this to any hosting:
- Azure App Service
- AWS Elastic Beanstalk
- Railway / Render
- Your own server

Just update `AiAdvisorConfig.cs` in the MAUI app to point to your deployed URL.

