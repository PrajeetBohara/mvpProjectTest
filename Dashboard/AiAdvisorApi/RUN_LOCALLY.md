# Running Flask App Locally

## Quick Start

1. **Navigate to the Flask app directory:**
   ```powershell
   cd Dashboard\AiAdvisorApi
   ```

2. **Install Python dependencies (if not already installed):**
   ```powershell
   pip install -r requirements.txt
   ```

3. **Set OpenAI API Key (optional - for full functionality):**
   ```powershell
   $env:OPENAI_API_KEY="your-api-key-here"
   ```
   Or create a `.env` file in the `AiAdvisorApi` folder with:
   ```
   OPENAI_API_KEY=your-api-key-here
   ```

4. **Run the Flask app:**
   ```powershell
   python app.py
   ```

5. **Open in browser:**
   - Go to: `http://localhost:5000`
   - You should see the AI Advisor input page with the ENCS logo

## Testing

- The page should show the ENCS logo at the top
- Input field and Send button should be visible
- You can test sending messages (will work if OpenAI API key is set)
- Check the console for any errors

## Stopping the Server

Press `Ctrl+C` in the terminal to stop the Flask server.

