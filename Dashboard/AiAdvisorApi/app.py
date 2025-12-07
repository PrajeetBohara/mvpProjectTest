from flask import Flask, request, jsonify, send_from_directory
from flask_cors import CORS
import os
from openai import OpenAI
from datetime import datetime
from collections import defaultdict

app = Flask(__name__, static_folder='wwwroot', static_url_path='')
CORS(app)

# OpenAI client
openai_key = os.getenv('OPENAI_API_KEY')
openai_client = OpenAI(api_key=openai_key) if openai_key else None

# In-memory transcript storage
transcripts = defaultdict(list)

# Degree catalog links
CATALOG_LINKS = [
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59137&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59280&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx",
    "https://catalog.mcneese.edu/preview_program.php?catoid=97&poid=59024&_gl=1*1mrh98w*_gcl_au*MTk4MjAwMzc3MS4xNzYwMTUwNjgx"
]

# Serve HTML page
@app.route('/')
def index():
    try:
        return send_from_directory('wwwroot', 'index.html')
    except Exception as e:
        return f"Error loading page: {str(e)}", 500

# Chat endpoint
@app.route('/api/chat', methods=['POST'])
def chat():
    data = request.json
    session_id = data.get('sessionId', 'demo')
    question = data.get('question', '').strip()
    
    if not question:
        return jsonify({'error': 'Question required'}), 400
    
    # Add user message
    transcripts[session_id].append({
        'role': 'user',
        'content': question,
        'timestamp': datetime.utcnow().isoformat()
    })
    
    # Get AI answer
    answer = "I'm sorry, but the OpenAI API key is not configured."
    
    if openai_client:
        try:
            # Simple prompt (can add RAG later if needed)
            messages = [
                {"role": "system", "content": "You are an academic advisor for McNeese State University. Provide helpful degree plan advice. Always cite sources and warn to verify with an official advisor."},
                {"role": "user", "content": question}
            ]
            
            # Add conversation history
            for msg in transcripts[session_id][-4:]:
                if msg['role'] in ['user', 'assistant']:
                    messages.append({"role": msg['role'], "content": msg['content']})
            
            response = openai_client.chat.completions.create(
                model="gpt-4o-mini",
                messages=messages
            )
            answer = response.choices[0].message.content
        except Exception as e:
            answer = f"Error: {str(e)}"
    
    # Add assistant message
    transcripts[session_id].append({
        'role': 'assistant',
        'content': answer,
        'timestamp': datetime.utcnow().isoformat()
    })
    
    return jsonify({'answer': answer, 'count': len(transcripts[session_id])})

# Get transcript
@app.route('/api/transcript')
def get_transcript():
    session_id = request.args.get('sessionId', 'demo')
    return jsonify(transcripts.get(session_id, []))

# Clear transcript
@app.route('/api/transcript/clear', methods=['POST'])
def clear_transcript():
    session_id = request.json.get('sessionId', 'demo') if request.json else 'demo'
    if session_id in transcripts:
        del transcripts[session_id]
    return jsonify({'ok': True, 'sessionId': session_id})

if __name__ == '__main__':
    port = int(os.getenv('PORT', 5000))
    app.run(host='0.0.0.0', port=port)

