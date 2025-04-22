import { useState, useEffect } from 'react';
import axios from 'axios';

export default function CommentSection({ postId }) {
    const [comments, setComments] = useState([]);
    const [content, setContent] = useState('');

    const fetchComments = async () => {
        const res = await axios.get(`/api/posts/${postId}/comments`);
        setComments(res.data);
    };

    const submitComment = async (e) => {
        e.preventDefault();
        await axios.post(`/api/posts/${postId}/comments`, { content });
        setContent('');
        fetchComments();
    };

    useEffect(() => { fetchComments(); }, [postId]);

    return (
        <div className="comment-section">
            <form onSubmit={submitComment}>
                <textarea
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    placeholder="Add a comment..."
                    required
                />
                <button type="submit">Post Comment</button>
            </form>

            <div className="comments-list">
                {comments.map(comment => (
                    <div key={comment.id} className="comment">
                        <strong>{comment.authorName}</strong>
                        <p>{comment.content}</p>
                        <small>{new Date(comment.createdAt).toLocaleString()}</small>
                    </div>
                ))}
            </div>
        </div>
    );
}